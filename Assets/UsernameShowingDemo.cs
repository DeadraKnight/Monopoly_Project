using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameShowingDemo : MonoBehaviour
{

    public TextMeshProUGUI output;
    public TMP_InputField userName;


    public void ButtonDemoMainView()
    {
        output.text = userName.text;
    }

    //public string playerName = Player.Instance.controlledPawn.controllingPlayer.username;

    //public void Update()
    //{
    //    playerName = userName.text;
    //    output.text = playerName;

    //}
}
   

