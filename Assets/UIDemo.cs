using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishNet.Object.Synchronizing;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI output;
    public TMP_InputField userName;

    public void ButtonDemo()
    {
        string username = userName.text;

        // Generate a unique identifier using the username and timestamp
        string uniqueId = $"{username}_{DateTime.Now.Ticks}";

        // Save the unique identifier and the corresponding username using PlayerPrefs
        PlayerPrefs.SetString(uniqueId, username);
        PlayerPrefs.Save();

        UsernameManager.AddUsername(uniqueId);
        output.text = username;
        output.text = $"Welcome, {username}!\n<size=20>\nPlease click Ready and then Start</size>";
    }
}
