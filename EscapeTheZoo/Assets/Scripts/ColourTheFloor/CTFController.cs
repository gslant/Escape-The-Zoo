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
    private List<GameObject> squareList;

    public KeyCode testKey;
    float powerUpStartTime;

    public TextMeshProUGUI timerText;
    private float currentTime;
    private float startingTime = 60f;
    // Start is called before the first frame update
    void Start()
    {

        currentTime = startingTime;
        timerText.SetText(startingTime.ToString());
        GameOverCanvas.SetActive(false);
        for (float j = -8.5f; j < 9f; j += 0.5f)
        {
            for (float i = -4.5f; i <= 3.5f; i += 0.5f)
            {
                squareList.Add(Instantiate(squarePrefab, new Vector3(j, i, 0), Quaternion.identity));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1f * Time.deltaTime;
        timerText.SetText(((int) currentTime).ToString());
    }
}
