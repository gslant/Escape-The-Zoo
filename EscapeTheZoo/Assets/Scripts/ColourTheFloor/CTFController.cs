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

    public List<GameObject> squares = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameOverCanvas.SetActive(false);
        SpawnSquares(squarePrefab);
    }

    public void SpawnSquares(GameObject toSpawn)
    {
        for (float j = -8.5f; j < 9f; j += 0.5f)
        {
            for (float i = -4.5f; i <= 4.5f; i += 0.5f)
            {
                squares.Add(Instantiate(toSpawn, new Vector3(j, i, 0), Quaternion.identity));
            }
        }
        Debug.Log(squares.Count);
    }

    // Update is called once per frame
    void Update()
    {
        increaseSize(colorCollider);
    }

    public void increaseSize(BoxCollider2D PCollider)
    {
        if (Input.GetKeyDown(testKey))
        {
            powerUpStartTime = Time.time;
            Debug.Log("BIG");
            Vector3 initSize = PCollider.size;
            Debug.Log(initSize);
            PCollider.size = new Vector3(5, 5, 0);
        }

        if (Time.time > powerUpStartTime + 5f)
        {
            PCollider.size = new Vector3(0.1f, 0.1f, 0);
        }
    }
}
