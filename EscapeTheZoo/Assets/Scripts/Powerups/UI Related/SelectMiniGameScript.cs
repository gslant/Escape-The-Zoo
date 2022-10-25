using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMiniGameScript : MonoBehaviour
{
    public static SelectMiniGameScript Instance { get; private set; }

    public GameControl control;
    private TextMeshProUGUI textMesh, miniGameOptionText;
    private Button button1, button2, button3, button4, button5;
    public static String text = "Click on the button to play the minigame now:";
    public static String infoString = "1 - Escape The Lion\n2 - Catch The Banana\n3 - Don't Wake The Human\n4 - Stay On The Iceberg\n5 - Colour The Floor";

    private void Awake()
    {
        Instance = this;
        Hide();
        textMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        miniGameOptionText = transform.Find("TextInfo").GetComponent<TextMeshProUGUI>();
        button1 = transform.Find("1Button").GetComponent<Button>();
        button2 = transform.Find("2Button").GetComponent<Button>();
        button3 = transform.Find("3Button").GetComponent<Button>();
        button4 = transform.Find("4Button").GetComponent<Button>();
        button5 = transform.Find("5Button").GetComponent<Button>();

    }

    public void showPopUp(String popupText, String textInfo, Action firstButton, Action secondButton, Action thirdthButton, Action fourthButton, Action fivthButton)
    {
        textMesh.text = popupText;
        miniGameOptionText.text = textInfo;

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();
        button5.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() =>
        {
            firstButton();
            Hide();
        });
        button2.onClick.AddListener(() => {
            secondButton();
            Hide();
        });
        button3.onClick.AddListener(() => {
            thirdthButton();
            Hide();
        });
        button4.onClick.AddListener(() => {
            fourthButton();
            Hide();
        });
        button5.onClick.AddListener(() => { 
            fivthButton();
            Hide();
        });
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}