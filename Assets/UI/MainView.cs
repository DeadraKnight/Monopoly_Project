using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public sealed class MainView : View
{
    [SerializeField]
    private Button purchaseTileButton;

    [SerializeField]
    private Button endTurnButton;

    [SerializeField]
    private Button showOwnedTilesButton;
    
    [SerializeField] 
    private TMP_Text balanceText;

    public GameObject popupAlert;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private TMP_Text playerList;

    [SerializeField]
    public LobbyView lobbyView;

    public bool IsTurn { get; set; }

    void Update()
    {
        if (GameManager.Instance.Players.Count > 0)
        {
            string playerList = "<b><size=40>\n\nPlayers: </size></b>"; // Bigger and bold text with extra new lines

            List<string> usedUsernames = new List<string>(); // Keep track of used usernames

            foreach (Player player in GameManager.Instance.Players)
            {
                string username = player.username;

                if (string.IsNullOrEmpty(username) || usedUsernames.Contains(username))
                {
                    // Generate a new random name until it is unique
                    do
                    {
                        username = RandomNameGenerator.GenerateRandomName();
                    } while (usedUsernames.Contains(username));

                    player.username = username; // Assign the generated name to the player
                    usedUsernames.Add(username); // Add the username to the list of used usernames
                }

                if (player.IsTurn) // Check if it is the current player's turn
                {
                    playerList += "\n\t\t<color=black><size=14><pos=-1.8em>" + username + "</size></color>"; // Black text color, smaller size, and moved a little bit to the left for the current player
                }
                else
                {
                    playerList += "\n\n\t\t\t\t\t\t<size=14><pos=-1.6em>" + username + "</size>"; // Default text color, smaller size, and moved a little bit to the left for other players
                }
            }

            this.playerList.text = playerList;
        }
    }

    public override void Initialize()
    {
        purchaseTileButton.onClick.AddListener(() =>
        {
            int pawnPosition = Player.Instance.controlledPawn.currentPosition;
            Tile tile = Board.Instance.Tiles[pawnPosition];

            if (Player.Instance.controlledPawn.controllingPlayer.hasRolledDiceThisTurn == false)
            {
                popupAlert.GetComponent<PopupAlert>().ShowAlert("You must roll first before purchasing a tile. ");
            }
            else if (!tile.IsOwnable)
            {
                popupAlert.GetComponent<PopupAlert>().ShowAlert("Tile is not a purchasable tile.");
            }
            else if (tile.isOwned)
            {
                popupAlert.GetComponent<PopupAlert>().ShowAlert($"Tile is already owned by {tile.owningPlayer.username}");
            } 
            else
            {
                GetComponent<DiceRollerUI>().ClearResults();
                OpenPurchasePanel openPurchasePanel = GetComponent<OpenPurchasePanel>();
                openPurchasePanel.OpenPanel();
            }
        });

        endTurnButton.onClick.AddListener(() =>
        {
            if (Player.Instance.hasRolledDiceThisTurn)
            {
                GetComponent<DiceRollerUI>().ClearResults();
                Player.Instance.hasRolledDiceThisTurn = false;
                Player.Instance.controlledPawn.IsEnding();
            }
            else
            {
                popupAlert.GetComponent<PopupAlert>().ShowAlert("You must roll the dice before ending your turn.");
            }
        });

        showOwnedTilesButton.onClick.AddListener(() =>
        {
            GetComponent<DiceRollerUI>().ClearResults();
            MyTilesPanel myTilesPanel = GetComponent<MyTilesPanel>();
            myTilesPanel.OpenPanel();
        });

        exitButton.onClick.AddListener(Application.Quit);
        base.Initialize();
    }
}