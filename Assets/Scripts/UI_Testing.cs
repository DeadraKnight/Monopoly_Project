using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Search;
using UnityEngine;

public class UI_Testing : MonoBehaviour
{

    [SerializeField] private UI_InputWindow InputWindow;

    private void Start()
    {
        transform.Find("SubmitUsernameBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            InputWindow.Show("Test Title", "abcd");
        };
    }
}
