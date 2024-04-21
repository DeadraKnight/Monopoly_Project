using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{

    public GameObject winnerView;
    public GameObject loserView;

    public void ShowWinnerView()
    {
        winnerView.SetActive(true);
        loserView.SetActive(false);
    }

    public void ShowLoserView()
    {
        winnerView.SetActive(false);
        loserView.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
