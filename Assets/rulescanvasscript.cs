using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rulescanvasscript : MonoBehaviour
{
    [SerializeField] CanvasGroup RulesPanel;
    // Start is called before the first frame update
    public void OpenPanel()
    {
        RulesPanel.alpha = 1;
        RulesPanel.blocksRaycasts = true;
    }

    // Update is called once per frame
    public void ClosePanel()
    {
        RulesPanel.alpha = 0;
        RulesPanel.blocksRaycasts = false;
    }
}
