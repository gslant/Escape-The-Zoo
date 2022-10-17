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
    public BoxCollider2D colorCollider;

    public KeyCode testKey;
    float powerUpStartTime;
    // Start is called before the first frame update
    void Start()
    {
        GameOverCanvas.SetActive(false);
        for (float j = -8.5f; j < 9f; j += 0.5f)
        {
            for (float i = -4.5f; i <= 4.5f; i += 0.5f)
            {
                Instantiate(squarePrefab, new Vector3(j, i, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(testKey))
        {
            powerUpStartTime = Time.time;
            Debug.Log("BIG");
            Vector3 initSize = colorCollider.size;
            Debug.Log(initSize);
            colorCollider.size = new Vector3(5,5,0);
        }

        if(Time.time > powerUpStartTime + 5f)
        {
            colorCollider.size = new Vector3(0.1f, 0.1f, 0);
        }
    }
}
