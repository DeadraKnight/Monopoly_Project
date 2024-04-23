using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class UI_InputWindow : MonoBehaviour
{
    private Button_UI OkBtn;
    private Button_UI CancelBtn;
    private TextMeshProUGUI TitleText;
    private TMP_InputField InputField;

    private void Awake()
    {

        OkBtn = transform.Find("OkBtn").GetComponent<Button_UI>();
        CancelBtn = transform.Find("CancelBtn").GetComponent<Button_UI>();
        TitleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        InputField = transform.Find("InputField").GetComponent<TMP_InputField>();

        Hide();
    }

    public void Show(string TitleString, string InputString)
    {
        gameObject.SetActive(true);

        TitleText.text = TitleString;
        InputField.text = InputString;

        //OkBtn.ClickFunc = () => {
        //    Hide();
        //    onOk(InputField.text);
        //};

    }

    //private void onOk(string text)
    //{
    //    throw new NotImplementedException();
    //}

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    

}
