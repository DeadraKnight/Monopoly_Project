using TMPro;
using UnityEngine;

public class PopupAlert : MonoBehaviour
{
    public TMP_Text messageText;
    public CanvasGroup popupPanel;
    
    //shows panel and shows a message
    public void ShowAlert(string message)
    {
        messageText.text = message;
        popupPanel.alpha = 1;
        popupPanel.blocksRaycasts = true;
    }

    //hides panel
    public void CloseAlert()
    {
        popupPanel.alpha = 0;
        popupPanel.blocksRaycasts = false;
    }
}
