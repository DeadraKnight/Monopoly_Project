using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting.Tugboat;
using JetBrains.Annotations;
using System.Linq;
using UnityEngine;

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: SyncObject]
    public SyncList<Player> Players { get; } = new SyncList<Player>();

    [field: SerializeField]
    [field: SyncVar]
    public bool CanStart { get; private set; }

    [field: SerializeField]
    [field: SyncVar]
    public bool DidStart { get; private set; }

    [field: SerializeField]
    [field: SyncVar]
    public int Turn { get; private set; }

    public int consecutiveZeroBalanceTurns = 0;

    [field: SerializeField]
    public GameObject Loser_View;

    [field: SerializeField]
    public GameObject Winner_View;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!IsServer) return;

        CanStart = Players.All(player => player.IsReady);

        Debug.Log($"There are {Players.Count} players in the game");
    }

    [Server]
    public void StartGame()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].StartGame();
        }

        DidStart = true;

        BeginTurn();
    }

    [Server]
    public void StopGame()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].StopGame();
        }

        DidStart = false;
    }

    [Server]
    public void BeginTurn()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].BeginTurn();
        }
    }

    /// <summary>
    /// Increment the turn count by 1 and if it reaches zero start over.
    /// </summary>
    [Server]
    public void EndTurn()
    {
        Turn = (Turn + 1) % Players.Count;

        if (Players[Turn].Balance <= 0)
        {
            consecutiveZeroBalanceTurns++;
        }
        else
        {
            consecutiveZeroBalanceTurns = 0;
        }

        Debug.Log("Current consecutive zero balance turns: " + consecutiveZeroBalanceTurns);

        if (consecutiveZeroBalanceTurns >= 3)
        {
            Debug.Log("Condition to show Loser_View is met");
            Loser_View.SetActive(true);
            StartCoroutine(KickPlayerAfterDelay(10f));
        }

        // Check if there is only one player left in the game
        if (Players.Count == 1)
        {
            // Activate the Winner_View if there is only one player left
            Winner_View.SetActive(true);
        }

        BeginTurn();
    }

    [Server]
    public void ChanceCard()
    {
        //switch statement to determine the outcome of the chance card
        switch (Random.Range(1, 5))
        {
            case 1:
                // Player Add 200 to the player's balance
                Players[Turn].Balance += 200;
                Debug.Log("Player has received 200");
                break;
            case 2:
                // Subtract 200 from the player's balance
                Players[Turn].Balance += 500;
                Debug.Log("Player has recevied 500");
                break;
            case 3:
                // Move the pawn to the "Jail" waypoint
                Players[Turn].controlledPawn.transform.position = Players[Turn].controlledPawn.jailWaypoint.transform.position;

                // Fix Pawn Position for pathing
                Players[Turn].controlledPawn.currentPosition = 10;
                Debug.Log("Player has been sent to jail");

                // Set the isInJail flag to true
                GameManager.Instance.Players[Turn].isInJail = true;
                break;
            case 4:
                
                break;
            case 5:
                //Player goes to free parking!
                Players[Turn].controlledPawn.transform.position = Players[Turn].controlledPawn.freeParkingWaypoint.transform.position;
                Player.Instance.controlledPawn.currentPosition = 20;
                Debug.Log("Player has been sent to free parking");
                break;
        } 
    }
}
