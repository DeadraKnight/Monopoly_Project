using UnityEngine;
using UnityEngine.UI;

public sealed class MainView : View
{
    [SerializeField]
    private Button purchaseTileButton;

    [SerializeField]
    private Button moveForwardButton;

    [SerializeField] 
    private Button moveBackwardButton;

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

        moveForwardButton.onClick.AddListener(() => Player.Instance.controlledPawn.ServerMove(1));

        moveBackwardButton.onClick.AddListener(() => Player.Instance.controlledPawn.ServerMove(-1));

        base.Initialize();
    }
}
