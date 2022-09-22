using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DataManager;

public class GameControl : MonoBehaviour
{
    // HUD Canvas Objects and Win Canvas Objects
    private static GameObject HUDGoldAmountText;
    private static GameObject HUDPlayerNameText;
    private static Button HUDCards;
    public GameObject HUDCanvas;
    public GameObject winCanvas;
    public GameObject winCanvasText;
    public Button finishGameButton;

    // DataManager to save players
    private DataManager dataManager;

    // Who the winner of the game is
    int winner;

    private static GameObject player1, player2;
    [SerializeField] private GameObject player1Accessory, player2Accessory;

    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;

    public static bool gameOver = false;

    // Loading player variables
    public static List<Player> listOfPlayersPlaying; // This will load in the selected players from the lobby
    private List<GameObject> playerObjects;
    private List<GameObject> playerAccessoryObjects;

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        HUDGoldAmountText = GameObject.Find("GoldAmount");
        HUDPlayerNameText = GameObject.Find("PlayerName");

        SetPlayerObjectLists();
        SetSelectedPlayers();
        LoadPlayerDataToPlayerObjects();

        player1.GetComponent<PlayerMovement>().myTurn = false;
        player2.GetComponent<PlayerMovement>().myTurn = false;

        winCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
        updatePlayerHUD(listOfPlayersPlaying[0]);
        dataManager = GetComponent<DataManager>();
        finishGameButton.onClick.AddListener(delegate { finishGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<PlayerMovement>().waypointIndex >
            player1StartWaypoint + diceSideThrown)
        {
            player1.GetComponent<PlayerMovement>().myTurn = false;
            player1StartWaypoint = player1.GetComponent<PlayerMovement>().waypointIndex - 1;
        }

        if (player2.GetComponent<PlayerMovement>().waypointIndex >
            player2StartWaypoint + diceSideThrown)
        {
            player2.GetComponent<PlayerMovement>().myTurn = false;
            player2StartWaypoint = player2.GetComponent<PlayerMovement>().waypointIndex - 1;
        }

        if (player1.GetComponent<PlayerMovement>().waypointIndex ==
            player1.GetComponent<PlayerMovement>().waypoints.Length)
        {
            player1.GetComponent<PlayerMovement>().waypointIndex = player1.GetComponent<PlayerMovement>().waypoints.Length - 1;
            gameOver = true;
            winner = 0;
        }

        if (player2.GetComponent<PlayerMovement>().waypointIndex ==
            player2.GetComponent<PlayerMovement>().waypoints.Length)
        {
            player2.GetComponent<PlayerMovement>().waypointIndex = player2.GetComponent<PlayerMovement>().waypoints.Length - 1;
            gameOver = true;
            winner = 1;
        }

        // If game is over, do this
        if (gameOver == true)
        {
            winCanvas.SetActive(true);
            HUDCanvas.SetActive(false);
            winCanvasText.GetComponent<TextMeshProUGUI>().text = GameControl.listOfPlayersPlaying[winner].getName() + " Wins!";
            
            for (int i = 0; i < listOfPlayersPlaying.Count; i++)
            {
                GameControl.listOfPlayersPlaying[i].addGameBalanceToTotalBalance();
                dataManager.SaveData(listOfPlayersPlaying[i]);
            }
        }
    }

    private void SetPlayerObjectLists()
    {
        // Not the most efficient way for now...
        playerObjects = new List<GameObject>() { player1, player2 };
        playerAccessoryObjects = new List<GameObject>() { player1Accessory, player2Accessory };
    }

    public void SetSelectedPlayers()
    {
        listOfPlayersPlaying = LobbyManager.getListOfPlayersPlaying();
    }

    public void LoadPlayerDataToPlayerObjects()
    {
        for (int i = 0; i < listOfPlayersPlaying.Count; i++)
        {
            playerObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Character Face Sprites/" + listOfPlayersPlaying[i].getAnimal());
            playerAccessoryObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/" + listOfPlayersPlaying[i].getAccessory());
        }
    }

    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove)
        {
            case 1:
                player1.GetComponent<PlayerMovement>().myTurn = true;
                GameControl.updatePlayerHUD(GameControl.listOfPlayersPlaying[1]);
                break;

            case 2:
                player2.GetComponent<PlayerMovement>().myTurn = true;
                GameControl.updatePlayerHUD(GameControl.listOfPlayersPlaying[0]);
                break;
        }
    }

    public static void updatePlayerHUD(Player player)
    {
        HUDPlayerNameText.GetComponent<TextMeshProUGUI>().text = player.getName();
        HUDGoldAmountText.GetComponent<TextMeshProUGUI>().text = player.getGameBalance() + "";
    }

    private void finishGame()
    {
        SceneLoader.LoadScene("MainScene");
    }
}