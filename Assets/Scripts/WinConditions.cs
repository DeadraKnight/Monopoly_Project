using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditions : MonoBehaviour
{
    public GameObject Winner_View;

    void Update()
    {
        if (IsWinner())
        {
            ShowWinner_View();
        }
    }

    bool IsWinner()
    {
        return GameManager.Instance.Players.Count == 1;
    }

    void ShowWinner_View()
    {
        Winner_View.SetActive(true);
    }
}
