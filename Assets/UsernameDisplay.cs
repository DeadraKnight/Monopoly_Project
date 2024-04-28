using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameDisplay : MonoBehaviour
{
    public TextMeshProUGUI usernamesText;

    void Start()
    {
        //UpdateUsernamesText();
    }

    void UpdateUsernamesText()
    {
        //usernamesText.text = "";

        foreach (string username in UsernameManager.usernames)
        {
            usernamesText.text += username + "\n";
        }
    }
}
