using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    void Update()
    {
        if (GameManager.Instance.Turn >= 0 && GameManager.Instance.Turn < GameManager.Instance.Players.Count)
        {
            string currentPlayerBalance = GameManager.Instance.Players[GameManager.Instance.Turn].Balance.ToString();
            balanceText.text = $"Balance: {currentPlayerBalance}";
        }
    }

    public override void Initialize()
    {
        purchaseTileButton.onClick.AddListener(() =>
        {
            int pawnPosition = Player.Instance.controlledPawn.currentPosition;
            Tile tile = Board.Instance.Tiles[pawnPosition];

            if (Player.Instance.hasRolledDiceThisTurn == false)
            {
                popupAlert.GetComponent<PopupAlert>().ShowAlert("You must roll first before purchasing a tile. ");
            }
            else if (!tile.IsOwnable)
            {
                popupAlert.GetComponent<PopupAlert>().ShowAlert("Tile is not a purchasable tile.");
            }
            else if (tile.IsOwned)
            {
                popupAlert.GetComponent<PopupAlert>().ShowAlert("Tile is already owned by another player.");
            } 
            else
            {
                OpenPurchasePanel openPurchasePanel = GetComponent<OpenPurchasePanel>();
                openPurchasePanel.OpenPanel();
                //add purchase code to OpenPurchasePanel.PurchaseTile()
            }
        });

        endTurnButton.onClick.AddListener(() =>
        { 
            GetComponent<DiceRollerUI>().ClearResults();
            Player.Instance.hasRolledDiceThisTurn = false;
            Player.Instance.controlledPawn.IsEnding();
        });

        base.Initialize();
    }
}