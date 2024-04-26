using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDemo : MonoBehaviour
{
    public TextMeshProUGUI output;
    public TMP_InputField userName;
    public GameObject UI_InputWindow; // Reference to the UI_InputWindow object

    //public void Hide()
    //{
    //    UI_InputWindow.SetActive(false);
    //}

    public void ButtonDemo()
    {
        string welcomeText = "Welcome, " + userName.text + "!";
        string instructionText = "<align=left><size=16>Please Ready Up</size>\n<size=16>and then click Start!</size></align>";
        output.text = welcomeText + "\n" + instructionText;
        //Hide();
    }
}