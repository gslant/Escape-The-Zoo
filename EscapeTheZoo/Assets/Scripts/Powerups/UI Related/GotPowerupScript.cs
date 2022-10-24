using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class GotPowerupScript : MonoBehaviour
{
    public static GotPowerupScript Instance { get; private set; }


    private TextMeshProUGUI textMesh, infoText;
    private Button okbutton;
    public static Sprite image;
    public static String text = "";
    public static String infoString = "";


    private void Awake()
    {
        Instance = this;
        Hide();
        textMesh = transform.Find("PowerupTitle").GetComponent<TextMeshProUGUI>();
        infoText = transform.Find("DescText").GetComponent<TextMeshProUGUI>();
        okbutton = transform.Find("okButton").GetComponent<Button>();
        image = transform.Find("Sprite").GetComponent<Sprite>();

        PowerupPopUp(text, infoString, () => {
            Debug.Log("First Button");
        });
    }

    public void PowerupPopUp(String popupText, String textInfo, Action Button)
    {
        textMesh.text = popupText;
        infoText.text = textInfo;
        okbutton.onClick.AddListener(() => {
            Hide();
            Button();
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
