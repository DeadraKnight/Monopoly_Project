using UnityEngine;
using UnityEngine.UI;

public sealed class MainView : View
{
    [SerializeField]
    private Button purchaseTileButton;

    [SerializeField]
    private Button rollDiceButton;

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

        rollDiceButton.onClick.AddListener(() => {
            int diceRoll1 = Random.Range(1, 7);
            int diceRoll2 = Random.Range(1, 7);
            int total = diceRoll1 + diceRoll2;

            Player.Instance.controlledPawn.Move(total);
        });

        base.Initialize();
    }
}
