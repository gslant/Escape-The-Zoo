using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
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

            //MINIGAME CODE: I know this is not effecince will improve on this later
            if (player1.GetComponent<PlayerMovement>().waypointIndex == 6 || player1.GetComponent<PlayerMovement>().waypointIndex == 17 || player1.GetComponent<PlayerMovement>().waypointIndex == 24 || player1.GetComponent<PlayerMovement>().waypointIndex == 38
            || player1.GetComponent<PlayerMovement>().waypointIndex == 47 || player1.GetComponent<PlayerMovement>().waypointIndex == 62 || player1.GetComponent<PlayerMovement>().waypointIndex == 82)
            {
                Debug.Log("MiniGame Time!!!!!");
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

            //MINIGAME CODE: I know this is not effecince will improve on this later
            if (player2.GetComponent<PlayerMovement>().waypointIndex == 6 || player2.GetComponent<PlayerMovement>().waypointIndex == 17 || player2.GetComponent<PlayerMovement>().waypointIndex == 24 || player2.GetComponent<PlayerMovement>().waypointIndex == 38
            || player2.GetComponent<PlayerMovement>().waypointIndex == 47 || player2.GetComponent<PlayerMovement>().waypointIndex == 62 || player2.GetComponent<PlayerMovement>().waypointIndex == 82)
            {
                Debug.Log("MiniGame Time!!!!!");
            }

            //WILDCARD CODE: I know this is not effecince will improve on this later
            if (player2.GetComponent<PlayerMovement>().waypointIndex == 11 || player2.GetComponent<PlayerMovement>().waypointIndex == 16 || player2.GetComponent<PlayerMovement>().waypointIndex == 33 || player2.GetComponent<PlayerMovement>().waypointIndex == 45
            || player2.GetComponent<PlayerMovement>().waypointIndex == 57 || player2.GetComponent<PlayerMovement>().waypointIndex == 65 || player2.GetComponent<PlayerMovement>().waypointIndex == 69 || player2.GetComponent<PlayerMovement>().waypointIndex == 77)
            {
                Debug.Log("You found a (nameofthepowerup)!!!");
            }

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
}