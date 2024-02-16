using UnityEngine;
using UnityEngine.UI;

public sealed class MainView : View
{
    [SerializeField]
    private Button purchaseTileButton;

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

        base.Initialize();
    }
}