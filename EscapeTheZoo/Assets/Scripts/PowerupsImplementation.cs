using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsImplementation : MonoBehaviour
{

    public List<Player> listofplayers = MinigameLoadPlayers.GetListOfPlayersPlaying();

    public Player player;

    // player is move up by 3 spaces(tiles)
    public static void moveup()
    {
        Debug.Log("Function 1");
    }
    // player is move down by 5 spaces(tiles)
    public static void movedown()
    {
        Debug.Log("Function 2");
    }
    // player instantly gain additional 8 coins
    public static void gainCoins(Player player)
    {
        player.changeGameBalanceByAmount(8);
        Debug.Log("Function 3");
    }
    // player instantly loses 10 coins
    public static void loseCoins(Player player)
    {
        player.changeGameBalanceByAmount(-10);
        Debug.Log("Function 4");
    }

    public static void GetPowerup(Player player)
    {
        int num = Random.Range(1, 5);


        switch (num)
        {
            case 1:
                moveup();
                break;
            case 2:
                movedown();
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
