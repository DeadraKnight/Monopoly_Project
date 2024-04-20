using UnityEngine;
using UnityEngine.UI;

public sealed class MainView : View
{
    [SerializeField]
    private Button purchaseTileButton;

    [SerializeField]
    private Button endTurnButton;

    [SerializeField]
    private Button showOwnedTilesButton;

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

        showOwnedTilesButton.onClick.AddListener(() =>
        {
            // Method to show the owned tiles
            //ShowOwnedTiles();
        });

        base.Initialize();
    }
}