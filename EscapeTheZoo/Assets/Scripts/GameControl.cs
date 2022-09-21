using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DataManager;

public class GameControl : MonoBehaviour
{
    // HUD Panel Objects
    public GameObject HUDGoldAmountText;
    public GameObject HUDPlayerNameText;
    public Button HUDCards;

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

        SetPlayerObjectLists();
        SetSelectedPlayers();
        LoadPlayerDataToPlayerObjects();

        player1.GetComponent<PlayerMovement>().myTurn = false;
        player2.GetComponent<PlayerMovement>().myTurn = false;

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
            gameOver = true;
        }

        if (player2.GetComponent<PlayerMovement>().waypointIndex ==
            player2.GetComponent<PlayerMovement>().waypoints.Length)
        {
            gameOver = true;
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
                break;

            case 2:
                player2.GetComponent<PlayerMovement>().myTurn = true;
                break;
        }
    }

    public void updatePlayerHUD(Player player)
    {
        HUDPlayerNameText.GetComponent<TextMeshProUGUI>().text = player.getName();
        //HUDGoldAmountText.GetComponent<TextMeshProUGUI>().text = player.getGameBalance();
    }
}