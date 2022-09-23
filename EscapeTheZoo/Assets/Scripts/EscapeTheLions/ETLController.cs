using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ETLController : MonoBehaviour
{
    public PlayerController p1;
    public PlayerController p2;

    [SerializeField]
    private GameObject GameOverCanvas;
    [SerializeField]
    private Button goBackButton;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    public List<GameObject> boardObjects = new List<GameObject>();

    private void Start()
    {
        GameOverCanvas.SetActive(false);
        goBackButton.onClick.AddListener(delegate { goBack(); });
    }

    public void getBoardObjects(List<GameObject> objects)
    {
        Debug.Log("minigame recieved: " + objects);
        this.boardObjects = objects;
        foreach(GameObject o in boardObjects)
        {
            o.SetActive(false);
        }
    }

    public void PlayerDies(string name)
    {
        GameOverCanvas.SetActive(true);
        if (name == "Player 1")
        {
            Debug.Log("player 1 died");
            gameOverText.SetText("player 1 has died, player 2 wins!");
            //p1.SetActive(false);
        }
        else if(name == "Player 2")
        {
            Debug.Log("player 2 died");
            gameOverText.SetText("player 2 has died, player 1 wins!");
        }
        else
        {
            Debug.Log(name);
        }

        //show game over screen/winner recieving coins?
        //notify scenemanager that the minigame is complete
        //notify gamemanager that the minigame is complete
        //transition back to board
    }

    private void goBack()
    {
        Debug.Log("Returning to board");
        GameControl con = FindObjectOfType<GameControl>();
        con.reloadObjs();
        SceneLoader.unloadScene("EscapeTheLions");
    }
}
