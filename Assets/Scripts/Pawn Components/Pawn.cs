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

    // An array of sprites to store the different icons
    [SerializeField]
    private Sprite[] sprites;

    private bool _isMoving;

    // Sound effect for moving the pawn
    public AudioClip moveSound;

    // AusioSource component for playing sound effect
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

        // Add an AudioSource component to the game object and set it up
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
                // Move the pawn to the "Jail" waypoint
                transform.position = jailWaypoint.transform.position;

                // Set currentWP to 10 to fix the pathing.
                currentPosition = 10;

                // Set the isInJail flag to true
                Player.Instance.isInJail = true;
            }
        });

        tween.Play();
    }
}
