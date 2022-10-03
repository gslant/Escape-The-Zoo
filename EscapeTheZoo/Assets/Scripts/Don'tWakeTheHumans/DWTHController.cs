using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DWTHController : MonoBehaviour
{
    public DWTHPlayerController p1;
    public DWTHPlayerController p2;

    public GameObject GameOverCanvas;
    public Button goBackButton;
    public TextMeshProUGUI gameOverText;
    public List<GameObject> boardObjects = new List<GameObject>();

    // Constants for the amount of coins earned
    private const int WINNING_COINS = 6;
    private const int LOSING_COINS = 2;

    void Start()
    {
        GameOverCanvas.SetActive(false);
        goBackButton.onClick.AddListener(delegate { goBack(); });
    }


    void Update()
    {

    }

    //This function is called by a player controller when that player is spotted by a human
    public void PlayerSpotted(string name)
    {
        
    }

    // Add coins to players based on ranking
    private void EarnCoins(int winIndex, int loseIndex)
    {
        MinigameLoadPlayers.GetListOfPlayersPlaying()[winIndex].changeGameBalanceByAmount(WINNING_COINS);
        MinigameLoadPlayers.GetListOfPlayersPlaying()[loseIndex].changeGameBalanceByAmount(LOSING_COINS);
    }

    // Functionality for the "goBackButton"
    private void goBack()
    {
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("Don'tWakeTheHumans");
    }
}
