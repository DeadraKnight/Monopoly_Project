using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseConditions : MonoBehaviour
{
    public GameObject Loser_View;

    bool hasLost = false;

    void Start()
    {
        ShowLoserView();

        StartCoroutine(KickPlayerAfterDelay(10f));
    }

    public void ShowLoserView()
    {
        Player currentPlayer = GameManager.Instance.Players[GameManager.Instance.Turn];

        if (currentPlayer.Balance <= 0)
        {
            Loser_View.SetActive(true);
            hasLost = true; 
        }
    }

    public IEnumerator KickPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (hasLost)
        {
            KickPlayer();
        }
    }

    public void KickPlayer()
    {
        Player currentPlayer = GameManager.Instance.Players[GameManager.Instance.Turn];
        
        GameManager.Instance.Players.Remove(currentPlayer);

        currentPlayer.gameObject.SetActive(false); 

        Debug.Log($"Player {currentPlayer} has been kicked from the game");
    }
}
