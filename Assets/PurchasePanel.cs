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
        purchaseButton.onClick.RemoveListener(PurchaseTile);
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
        PurchasePanel.GetComponent<Image>().sprite = tile.panelSprite;
    }
    
    public void PurchaseTile()
    {
        int pawnPosition = Player.Instance.controlledPawn.currentPosition;

        if (Board.Instance.Tiles[pawnPosition].owningPlayer == null)
        {
            Board.Instance.ServerSetTileOwner(pawnPosition, Player.Instance.controlledPawn.controllingPlayer);
        }
    }
}