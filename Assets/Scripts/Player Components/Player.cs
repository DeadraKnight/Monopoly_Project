using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class Player : NetworkBehaviour
{
    public static Player Instance { get; private set; }

    [SyncVar]
    public string username;

    [SyncVar]
    public int balance = 1500; // Players start with 1500 dollars

    [field: SyncVar]
    public bool IsReady
    {
        get;

        [ServerRpc(RequireOwnership = false)]
        set;
    }

    [SerializeField]
    private Pawn pawnPrefab;

    [SyncVar]
    public Pawn controlledPawn;

    public bool hasRolledDiceThisTurn = false;

    public bool isInJail = false;

    public List<Tile> ownedTiles = new List<Tile>();

    public override void OnStartServer()
    {
        base.OnStartServer();

        GameManager.Instance.Players.Add(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        GameManager.Instance.Players.Remove(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        Instance = this;

        ViewManager.Instance.Initialize();
    }

    [Server]
    public void StartGame()
    {
        int playerIndex = GameManager.Instance.Players.IndexOf(this);

        Transform spawnPoint = Board.Instance.Tiles[0].PawnPositions[playerIndex];

        Pawn pawnInstance = Instantiate(pawnPrefab, spawnPoint.position, Quaternion.identity);

        controlledPawn = pawnInstance;

        controlledPawn.controllingPlayer = this;

        Spawn(pawnInstance.gameObject, Owner);
    }

    [Server]
    public void StopGame()
    {
        if (controlledPawn != null) controlledPawn.Despawn();
    }

    /// <summary>
    /// If current turn corresponds with the current player then, they can play.
    /// </summary>
    [Server]
    public void BeginTurn()
    {
        TargetBegin(Owner, GameManager.Instance.Turn == GameManager.Instance.Players.IndexOf(this));
    }

    [TargetRpc]
    private void TargetBegin(NetworkConnection networkConnection, bool canPlay)
    {
        if (canPlay)
        {
            ViewManager.Instance.Show<MainView>();
        }
        else
        {
            ViewManager.Instance.Show<WaitView>();
        }
    }
}