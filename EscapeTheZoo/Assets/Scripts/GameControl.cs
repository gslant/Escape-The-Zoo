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

    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;

    public static bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

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