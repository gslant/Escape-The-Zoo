using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsImplementation : MonoBehaviour
{

    public List<Player> listofplayers = MinigameLoadPlayers.GetListOfPlayersPlaying();

    public Player player;



    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void moveup()
    {
        Debug.Log("Function 1");
    }

    public static void movedown()
    {
        Debug.Log("Function 2");
    }

    public static int gainCoins(int playerBalance)
    {
        int newplayerBalnce = playerBalance + 8;
        return newplayerBalnce;
    }

    public static int loseCoins(int playerBalance)
    {
        int newplayerBalnce = playerBalance - 10;
        return newplayerBalnce;
    }

    public static void GetPowerup(Player player)
    {
        int num = Random.Range(3, 5);


        switch (num)
        {
            case 1:
                moveup();
                break;
            case 2:
                movedown();
                break;
            case 3:
                gainCoins(player.getGameBalance());
                break;
            case 4:
                loseCoins(player.getGameBalance());
                break;
        }

    }

}
