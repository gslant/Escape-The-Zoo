using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SOTIController : MonoBehaviour
{
    public CharacterController2D p1;
    public CharacterController2D p2;

    [SerializeField]
    private GameObject GameOverCanvas;
    [SerializeField]
    private Button goBackButton;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    public List<GameObject> boardObjects = new List<GameObject>();

    // Constants for the amount of coins earned
    private const int WINNING_COINS = 6;
    private const int LOSING_COINS = 2;

    // Start is called before the first frame update
    void Start()
    {
        GameOverCanvas.SetActive(false);
        goBackButton.onClick.AddListener(delegate { goBack(); });
    }

    //Defunct
    public void getBoardObjects(List<GameObject> objects)
    {
        this.boardObjects = objects;
        foreach (GameObject o in boardObjects)
        {
            o.SetActive(false);
        }
    }

    //This function is called by a player controller when that player collides with the lion
    public void PlayerDies(string name)
    {
        int winIndex = -1;
        int loseIndex = -1;
        Debug.Log(name + " recieved by SOTICon");

        GameOverCanvas.SetActive(true);
        if (name == "Player1")
        {
            Debug.Log("WINdex = 1");
            gameOverText.SetText(MinigameLoadPlayers.GetListOfPlayersPlaying()[0].getName() + " has died, " + MinigameLoadPlayers.GetListOfPlayersPlaying()[1].getName() + " wins!");
            winIndex = 1;
            loseIndex = 0;
        }
        else if (name == "Player2")
        {
            Debug.Log("WINdex = 0");
            gameOverText.SetText(MinigameLoadPlayers.GetListOfPlayersPlaying()[1].getName() + " has died, " + MinigameLoadPlayers.GetListOfPlayersPlaying()[0].getName() + " wins!");
            winIndex = 0;
            loseIndex = 1;
        }

        EarnCoins(winIndex, loseIndex);
    }

    private void EarnCoins(int winIndex, int loseIndex)
    {
        Debug.Log("winIndex = " + winIndex);
        Debug.Log("loseIndex = " + loseIndex);
        Debug.Log(MinigameLoadPlayers.GetListOfPlayersPlaying());
        MinigameLoadPlayers.GetListOfPlayersPlaying()[winIndex].changeGameBalanceByAmount(WINNING_COINS);
        MinigameLoadPlayers.GetListOfPlayersPlaying()[loseIndex].changeGameBalanceByAmount(LOSING_COINS);
    }

    private void goBack()
    {
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("StayOnTheIceberg");
    }
}