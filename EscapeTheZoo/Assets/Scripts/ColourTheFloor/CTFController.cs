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

    public GameObject grid;
    public GameObject squarePrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameOverCanvas.SetActive(false);
        for (int j = -7; j < 8; j += 2)
        {
            for (int i = -3; i <= 4; i += 2)
            {
                Instantiate(squarePrefab, new Vector3(j, i, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
