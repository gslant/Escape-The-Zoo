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

    // List of minigames
    private List<string> minigames = new List<string>();

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
    private int currentPlayerIndex; // This will determine which profile to load for the HUD

    public GameObject mainCam, eventSys, grid, hud, p1, p2, dice;

    // Constants
    private int FINISH_FIRST_REWARD = 10; // The reward for finishing the game first

    // Use this for initialization
    void Start()
    {
        // Adds all the minigames to the list
        minigames.Add("EscapeTheLions");
        minigames.Add("Don'tWakeTheHumans");
        minigames.Add("StayOnTheIceberg");
        //minigames.Add("CatchTheBananas");

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        HUDGoldAmountText = GameObject.Find("GoldAmount");
        HUDPlayerNameText = GameObject.Find("PlayerName");

        SetPlayerObjectLists();
        SetSelectedPlayers();
        LoadPlayerDataToPlayerObjects();

        player1.GetComponent<PlayerMovement>().myTurn = false;
        player2.GetComponent<PlayerMovement>().myTurn = false;

        // Sets up the HUD and hides the win screen. Then initialises a DataManager object and attaches a listener to the "Quit to Menu" button
        winCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
        updatePlayerHUD(listOfPlayersPlaying[0]);
        dataManager = GetComponent<DataManager>();
        finishGameButton.onClick.AddListener(delegate { finishGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        // While the game isn't over
        if (!gameOver)
        {
            if (player1.GetComponent<PlayerMovement>().waypointIndex >
                player1StartWaypoint + diceSideThrown)
            {
                player1.GetComponent<PlayerMovement>().myTurn = false;
                player1StartWaypoint = player1.GetComponent<PlayerMovement>().waypointIndex - 1;
                currentPlayerIndex = 1;

                //MINIGAME CODE: I know this is not effecince will improve on this later
                if (player1.GetComponent<PlayerMovement>().waypointIndex == 6 || player1.GetComponent<PlayerMovement>().waypointIndex == 17 || player1.GetComponent<PlayerMovement>().waypointIndex == 24 || player1.GetComponent<PlayerMovement>().waypointIndex == 38
                || player1.GetComponent<PlayerMovement>().waypointIndex == 47 || player1.GetComponent<PlayerMovement>().waypointIndex == 62 || player1.GetComponent<PlayerMovement>().waypointIndex == 82)
                {
                    Debug.Log("MiniGame Time!!!!!");
                    mainCam.SetActive(false);
                    hud.SetActive(false);
                    grid.SetActive(false);
                    dice.SetActive(false);
                    p1.SetActive(false);
                    p2.SetActive(false);
                    eventSys.SetActive(false);
                    SceneLoader.LoadMinigameAdditive(minigames[Random.Range(0, minigames.Count)]);
                }

                //WILDCARD CODE: I know this is not effecince will improve on this later
                if (player1.GetComponent<PlayerMovement>().waypointIndex == 11 || player1.GetComponent<PlayerMovement>().waypointIndex == 16 || player1.GetComponent<PlayerMovement>().waypointIndex == 33 || player1.GetComponent<PlayerMovement>().waypointIndex == 45
                || player1.GetComponent<PlayerMovement>().waypointIndex == 57 || player1.GetComponent<PlayerMovement>().waypointIndex == 65 || player1.GetComponent<PlayerMovement>().waypointIndex == 69 || player1.GetComponent<PlayerMovement>().waypointIndex == 77)
                {
                    Debug.Log("You found a (nameofthepowerup)!!!");
                }
            }

            if (player2.GetComponent<PlayerMovement>().waypointIndex >
                player2StartWaypoint + diceSideThrown)
            {
                player2.GetComponent<PlayerMovement>().myTurn = false;
                player2StartWaypoint = player2.GetComponent<PlayerMovement>().waypointIndex - 1;
                currentPlayerIndex = 0;

                //MINIGAME CODE: I know this is not effecince will improve on this later
                if (player2.GetComponent<PlayerMovement>().waypointIndex == 6 || player2.GetComponent<PlayerMovement>().waypointIndex == 17 || player2.GetComponent<PlayerMovement>().waypointIndex == 24 || player2.GetComponent<PlayerMovement>().waypointIndex == 38
                || player2.GetComponent<PlayerMovement>().waypointIndex == 47 || player2.GetComponent<PlayerMovement>().waypointIndex == 62 || player2.GetComponent<PlayerMovement>().waypointIndex == 82)
                {
                    Debug.Log("MiniGame Time!!!!!");
                    mainCam.SetActive(false);
                    hud.SetActive(false);
                    grid.SetActive(false);
                    dice.SetActive(false);
                    p1.SetActive(false);
                    p2.SetActive(false);
                    eventSys.SetActive(false);
                    SceneLoader.LoadMinigameAdditive(minigames[Random.Range(0, minigames.Count)]);

                }

                //WILDCARD CODE: I know this is not effecince will improve on this later
                if (player2.GetComponent<PlayerMovement>().waypointIndex == 11 || player2.GetComponent<PlayerMovement>().waypointIndex == 16 || player2.GetComponent<PlayerMovement>().waypointIndex == 33 || player2.GetComponent<PlayerMovement>().waypointIndex == 45
                || player2.GetComponent<PlayerMovement>().waypointIndex == 57 || player2.GetComponent<PlayerMovement>().waypointIndex == 65 || player2.GetComponent<PlayerMovement>().waypointIndex == 69 || player2.GetComponent<PlayerMovement>().waypointIndex == 77)
                {
                    Debug.Log("You found a (nameofthepowerup)!!!");
                }
            }

            GameControl.updatePlayerHUD(GameControl.listOfPlayersPlaying[currentPlayerIndex]);

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
            if (gameOver)
            {
                winCanvas.SetActive(true);
                HUDCanvas.SetActive(false);
                winCanvasText.GetComponent<TextMeshProUGUI>().text = GameControl.listOfPlayersPlaying[winner].getName() + " Wins!\n" + GameControl.listOfPlayersPlaying[winner].getName() + " earns an extra " + FINISH_FIRST_REWARD + " coins!";
                GameControl.listOfPlayersPlaying[winner].changeGameBalanceByAmount(FINISH_FIRST_REWARD);

                for (int i = 0; i < listOfPlayersPlaying.Count; i++)
                {
                    GameControl.listOfPlayersPlaying[i].addGameBalanceToTotalBalance();
                    dataManager.SaveData(listOfPlayersPlaying[i]);
                }
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
            playerObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = "<size=60%>(Player " + (i + 1) + ")</size>\n" + listOfPlayersPlaying[i].getName();
            playerAccessoryObjects[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cosmetic Sprites/" + listOfPlayersPlaying[i].getAccessory());
        }
    }

    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove)
        {
            case 1:
                player1.GetComponent<PlayerMovement>().myTurn = true;
                break;

            case 2:
                player2.GetComponent<PlayerMovement>().myTurn = true;
                break;
        }
    }

    // Updates the HUD with information taken from the given "Player" object
    public static void updatePlayerHUD(Player player)
    {
        HUDPlayerNameText.GetComponent<TextMeshProUGUI>().text = player.getName();
        HUDGoldAmountText.GetComponent<TextMeshProUGUI>().text = player.getGameBalance() + "";
    }

    // Does this when the "Quit to Menu" button is pressed
    private void finishGame()
    {
        Debug.Log("Quit to menu button clicked");
        SceneLoader.LoadScene("MainScene");
    }

    public void reloadObjs()
    {
        mainCam.SetActive(true);
        hud.SetActive(true);
        grid.SetActive(true);
        dice.SetActive(true);
        p1.SetActive(true);
        p2.SetActive(true);
        eventSys.SetActive(true);
    }
}