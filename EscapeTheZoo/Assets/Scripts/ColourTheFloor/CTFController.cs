using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CTFController : MonoBehaviour
{
    public GameObject GameOverCanvas;
    public Button goBackButton;
    public TextMeshProUGUI gameOverText;

    public CTFPlayerController player1;
    public CTFPlayerController player2;
    // Start is called before the first frame update
    void Start()
    {
        GameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
