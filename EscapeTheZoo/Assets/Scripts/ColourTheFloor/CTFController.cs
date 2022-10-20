using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CTFController : MonoBehaviour
{
    // Sounds
    public AudioSource CTFMusic;
    public AudioSource playerOneBeepAudio;
    public AudioSource playerTwoBeepAudio;

    public GameObject GameOverCanvas;
    public TextMeshProUGUI gameOverText;
    public GameObject startCanvas;

    public GameObject player1;
    public GameObject player2;

    public GameObject grid;
    public GameObject squarePrefab;
    private List<GameObject> squareList;

    public KeyCode testKey;
    float powerUpStartTime;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI redtext;
    public TextMeshProUGUI greentext;
    private float currentTime;
    private float startingTime = 60f;

    public int greenNumber;
    public int redNumber;

    // Constants for the amount of coins earned
    private const int WINNING_COINS = 6;
    private const int LOSING_COINS = 2;

    int winIndex;
    int loseIndex;
    // Start is called before the first frame update
    void Start()
    {
        greenNumber = 0;
        redNumber = 0;
        GameOverCanvas.SetActive(false);
        squareList = new List<GameObject>();
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

        // Unmute all sounds on startup
        CTFMusic.mute = false;
        playerOneBeepAudio.mute = false;
        playerTwoBeepAudio.mute = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCanvas.activeInHierarchy)
        {
            player1.SetActive(true);
            player2.SetActive(true);
        }
        currentTime -= 1f * Time.deltaTime;
        timerText.SetText(((int) currentTime).ToString());
        greentext.SetText(greenNumber.ToString());
        redtext.SetText(redNumber.ToString());
        if(currentTime <= 0f)
        {
            if(greenNumber > redNumber)
            {
                GameOver("Player 1");
            }
            else if(redNumber > greenNumber)
            {
                GameOver("Player 2");
            }
        }
    }

    public void GameOver(string name)
    {
        // Mute the music when the game is over
        CTFMusic.mute = true;
        playerOneBeepAudio.mute = true;
        playerTwoBeepAudio.mute = true;

        GameOverCanvas.SetActive(true);
        if (name == "Player 1")
        {
            gameOverText.SetText(MinigameLoadPlayers.GetListOfPlayersPlaying()[1].getName() + " wins!");

            winIndex = 1;
            loseIndex = 0;
        }
        else if (name == "Player 2")
        {
            gameOverText.SetText(MinigameLoadPlayers.GetListOfPlayersPlaying()[0].getName() + " wins!");
            winIndex = 0;
            loseIndex = 1;
        }
        StartCoroutine(GameOverPause());
    }

    private void EarnCoins(int winIndex, int loseIndex)
    {
        MinigameLoadPlayers.GetListOfPlayersPlaying()[winIndex].changeGameBalanceByAmount(WINNING_COINS);
        MinigameLoadPlayers.GetListOfPlayersPlaying()[loseIndex].changeGameBalanceByAmount(LOSING_COINS);
    }

    IEnumerator GameOverPause()
    {
        // Pause game for 2 seconds
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        EarnCoins(winIndex, loseIndex);

        GoBackToGameBoard();
    }


    private void GoBackToGameBoard()
    {
        foreach (GameObject sqr in squareList)
        {
            Destroy(sqr);
        }
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("ColourTheFloor");
    }
}
