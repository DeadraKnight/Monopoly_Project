using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OpenPurchasePanel : MonoBehaviour
{
    [SerializeField] CanvasGroup PurchasePanel;
    [SerializeField] Button exitButton;
    [SerializeField] Button purchaseButton;
    [SerializeField] TMP_Text tileName, tilePrice, tileRent;
    

    public void OpenPanel()
    {
        //TODO: assign text from tile
        
        RenderTileSprite();
        purchaseButton.onClick.AddListener(PurchaseTile);
        PurchasePanel.blocksRaycasts = true;
        PurchasePanel.alpha = 1;
    }

    public void ClosePanel()
    {
        PurchasePanel.blocksRaycasts = false;
        PurchasePanel.alpha = 0;
    }

    public void RenderTileSprite()
    {
        Tile tile = Board.Instance.Tiles[Player.Instance.controlledPawn.currentPosition];
        PurchasePanel.GetComponent<Image>().sprite = tile.sprite;
    }
    
    public void PurchaseTile()
    {
        int tileIndex = Player.Instance.controlledPawn.currentPosition;
        Player player = Player.Instance;

        // Check if the player has enough money to buy the tile
        if (player.Balance < Board.Instance.Tiles[tileIndex].cost)
        {
            return;
        }
        Board.Instance.ServerSetTileOwner(tileIndex, player);
    }
}