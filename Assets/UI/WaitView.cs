using TMPro;
using UnityEngine;
public sealed class WaitView : View
{
    [SerializeField]
    TMP_Text waiting_text;



    private void Update()
    {
        if (!IsInitialized) return;
        int curTurnNum = GameManager.Instance.Turn;
        string curPlayerName = GameManager.Instance.Players[curTurnNum].username;
        if (string.IsNullOrEmpty(curPlayerName))
        {
        waiting_text.text = $"Waiting on Player {curTurnNum} to finish their turn.";
        }
        else
        {
            waiting_text.text = $"Waiting on {curPlayerName} to finish their turn.";
        }
    }
}
