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
    
    void Update()
    {
        string currentPlayerBalance = GameManager.Instance.Players[GameManager.Instance.Turn].Balance.ToString();
        balanceText.text = $"Balance: {currentPlayerBalance}";
    }

    public override void Initialize()
    {
        purchaseTileButton.onClick.AddListener(() =>
        {
            int pawnPositioin = Player.Instance.controlledPawn.currentPosition;

            if (Board.Instance.Tiles[pawnPositioin].owningPlayer == null)
            {
                Board.Instance.ServerSetTileOwner(pawnPositioin, Player.Instance);
            }
        });

        endTurnButton.onClick.AddListener(() =>
        {
            Player.Instance.hasRolledDiceThisTurn = false;
            Player.Instance.controlledPawn.IsEnding();
        });

        base.Initialize();
    }
}