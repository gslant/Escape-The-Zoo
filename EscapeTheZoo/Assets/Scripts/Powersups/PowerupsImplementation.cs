using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsImplementation : MonoBehaviour
{

    public static List<Player> listofplayers = MinigameLoadPlayers.GetListOfPlayersPlaying();

    public static Player player1, player2;

    public static int up = 3;
    public static int down = 5;

    void Start()
    {
        player1 = listofplayers[0];
        player2 = listofplayers[1];
        GameControl.player1.GetComponent<PlayerMovement>().myTurn = false;
        GameControl.player2.GetComponent<PlayerMovement>().myTurn = false;
    }

    // player is move up by 3 spaces(tiles)
    public static void moveup(Player player)
    {
        if(player == player1)
        {
            Dice.RollTheDice();
        }

        if (player == player2)
        {
            Dice.RollTheDice();
        }
        Debug.Log("Function 1");


    }
    // player is move down by 5 spaces(tiles)
    public static void movedown(Player player)
    {
        GameControl.MovePlayer(-5);
        Debug.Log("Function 2");
    }
    // player instantly gain additional 8 coins
    public static void gainCoins(Player player)
    {
        player.changeGameBalanceByAmount(8);
        Debug.Log("Function 4");
    }
    // player instantly loses 10 coins
    public static void loseCoins(Player player)
    {
        player.changeGameBalanceByAmount(-10);
        Debug.Log("Function 5");
    }

    public static void GetPowerup(Player player)
    {
        int num = Random.Range(1, 5);


        switch (num)
        {
            case 1:
                
                moveup(player);
                break;
            case 2:
                movedown(player);
                break;
            case 3:
                gainCoins(player);
                break;
            case 4:
                loseCoins(player);
                break;
        }

    }

}
