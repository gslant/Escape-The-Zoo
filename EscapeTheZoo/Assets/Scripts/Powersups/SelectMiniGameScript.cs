using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMiniGameScript : MonoBehaviour
{
    public static SelectMiniGameScript Instance { get; private set; }


    private TextMeshProUGUI textMesh, miniGameOptionText;
    private Button button1, button2, button3, button4;
    public static String text = "Which minigame would you like to play now:";
    public static String infoString = "1 - Escape The Lion\n2 - Don't Wake The Human\n3 - Stay On The Iceberg\n4 - Catch The Banana";


    private void Awake()
    {
        Hide();
        Instance = this;


        textMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        miniGameOptionText = transform.Find("TextInfo").GetComponent<TextMeshProUGUI>();
        button1 = transform.Find("1Buttons").GetComponent<Button>();
        button2 = transform.Find("2Buttons").GetComponent<Button>();
        button3 = transform.Find("3Buttons").GetComponent<Button>();
        button4 = transform.Find("4Buttons").GetComponent<Button>();

        showPopUp(text, infoString, () => {
            Debug.Log("First Button");
        }, () => {
            Debug.Log("Second Button");
         }, () => {
             Debug.Log("Thirdth Button");
         }, () => {
             Debug.Log("Fourth Button");
         });
    }

    public void showPopUp(String popupText, String textInfo, Action firstButton, Action secondButton, Action thirdthButton, Action fourthButton)
    {
        textMesh.text = popupText;
        miniGameOptionText.text = textInfo;
        button1.onClick.AddListener(() => {
            Hide();
            firstButton();
        });
        button2.onClick.AddListener(() => {
            Hide();
            secondButton();
        });
        button3.onClick.AddListener(() => {
            Hide();
            thirdthButton();
        });
        button4.onClick.AddListener(() => {
            Hide();
            fourthButton();
        });

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
