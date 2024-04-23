using TMPro;
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
        
        //TODO: Assign sprite from tile 
        
        PurchasePanel.blocksRaycasts = true;
        PurchasePanel.alpha = 1;
    }

    public void ClosePanel()
    {
        PurchasePanel.blocksRaycasts = false;
        PurchasePanel.alpha = 0;
    }
}