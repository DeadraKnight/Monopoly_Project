using DG.Tweening;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Linq;
using UnityEngine;

public sealed class Pawn : NetworkBehaviour
{
    [SyncVar]
    public Player controllingPlayer;

    [SyncVar]
    public int currentPosition;

    public GameObject jailWaypoint;
    public GameObject freeParkingWaypoint;

    // An array of sprites to store the different icons
    [SerializeField]
    private Sprite[] sprites;

    private bool _isMoving;

    // Sound for moving the pawn
    public AudioClip moveSound;

    // AudioSource component for playing sound effect
    private AudioSource audioSource;

    /// <summary>
    /// This will set the players avatar in order of the Index of players.
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();

        GetComponent<SpriteRenderer>().sprite = sprites[GameManager.Instance.Players.IndexOf(controllingPlayer)];
    }

    private void Start()
    {
        // Find the jail waypoint in the scene by its name
        jailWaypoint = GameObject.Find("Jail_Position");
        freeParkingWaypoint = GameObject.Find("Free_Parking_Position");

        // Add AudioSource component to the game object
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = moveSound;
    }

    /// <summary>
    /// This was made to let the server know a turn is ending.
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    public void IsEnding()
    {
        GameManager.Instance.EndTurn();
    }

    /// <summary>
    /// Method to move the pawn on the board
    /// </summary>
    /// <param name="steps"></param>
    [ServerRpc(RequireOwnership = false)]
    public void Move(int steps)
    {
        if (_isMoving) return;

        _isMoving = true;

        // Play the move sound effect
        audioSource.volume = 0.5f;
        audioSource.Play();

        Tile[] tiles = Board.Instance.Slice(currentPosition, (currentPosition + steps) % Board.Instance.Tiles.Length);

        int controllingPlayerIndex = GameManager.Instance.Players.IndexOf(controllingPlayer);

        Vector3[] path = tiles.Select(tile => tile.PawnPositions[controllingPlayerIndex].position).ToArray();

        Tween tween = transform.DOPath(path, 2.0f);

        tween.OnComplete(() =>
        {
            _isMoving = false;

            currentPosition = (currentPosition + steps) % Board.Instance.Tiles.Length;

            // Check if the new position is the "Go To Jail" tile
            if (currentPosition == 30)
            {
                GoToJail();
            }
            //if statement to check if the player has looped around the board
            if (currentPosition < steps)
            {
                PassGo();
            }
            // Check if current tile is a ChanceTile
            if (Board.Instance.Tiles[currentPosition].ChanceTile)
            {
                // Call the ChanceCard method
                GameManager.Instance.ChanceCard();
            }
            if (Board.Instance.Tiles[currentPosition].isOwned == true)
            {
                PayRent();    
            }
            if (Board.Instance.Tiles[currentPosition].TaxTile)
            {
                LandOnTaxTile();
            }
            if (currentPosition == 20)
            {
                LandOnFreeParking();
            }
        });

        tween.Play();
    }
    #region Board Actions

    /// <summary>
    /// Method to send the player to the "Jail" waypoint
    /// </summary>
    public void GoToJail()
    {
        // Move the pawn to the "Jail" waypoint
        transform.position = jailWaypoint.transform.position;

        // Set currentWP to 10 to fix the pathing.
        currentPosition = 10;

        // Set the isInJail flag to true
        controllingPlayer.isInJail = true;
    }

    /// <summary>
    /// Method to pass "Go" and collect $200
    /// </summary>
    public void PassGo()
    {
        controllingPlayer.Balance += 200;
    }

    /// <summary>
    /// Method to pay rent to the owning player
    /// </summary>
    public void PayRent()
    {
        if (Board.Instance.Tiles[currentPosition].owningPlayer != controllingPlayer)
        {
            int rent = Board.Instance.Tiles[currentPosition].rent; // Get the rent amount of the tile

            controllingPlayer.Balance -= rent; // Deduct rent from player balance

            Board.Instance.Tiles[currentPosition].owningPlayer.Balance += rent; // Add rent to the owning player's balance

            Debug.Log($"Player {controllingPlayer.OwnerId} paid ${rent} in rent to Player {Board.Instance.Tiles[currentPosition].owningPlayer.OwnerId}!");
        }
        else
        {
            // Do nothing
        }
    }

    /// <summary>
    /// Method to pay tax when landing on a tax tile
    /// </summary>
    public void LandOnTaxTile()
    {
        int taxAmount = Board.Instance.Tiles[currentPosition].TaxCost;

        // Deduct tax from player balance
        controllingPlayer.Balance -= taxAmount;

        // Add tax to the tax pile
        Board.Instance.TaxPile += taxAmount;

        // (Optional) Inform the player about the tax payment
        Debug.Log($"Player {controllingPlayer.username} paid ${taxAmount} in tax!");
    }

    /// <summary>
    /// Method to collect the money from the tax pile when landing on Free Parking
    /// </summary>
    public void LandOnFreeParking()
    {
        controllingPlayer.Balance += Board.Instance.TaxPile;
        Board.Instance.TaxPile = 0;
    }
    #endregion
}
