using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsImplementation : MonoBehaviour
{

    public static int up = 3;
    public static int down = -5;

    public GameControl control;

    // player is move up by 3 spaces(tiles)
    public static void moveup(Player player)
    {

        if (string.Equals(player.getName(), GameControl.listOfPlayersPlaying[0].getName()))
        {
            GameControl.diceSideThrown = up;
            GameControl.MovePlayer(1);
        }

        if (string.Equals(player.getName(), GameControl.listOfPlayersPlaying[1].getName()))
        {
            GameControl.diceSideThrown = up;
            GameControl.MovePlayer(2);
        }

        Debug.Log("Function 1");

    }
    // player is move down by 5 spaces(tiles)
    public static void movedown(Player player)
    {
        if (string.Equals(player.getName(), GameControl.listOfPlayersPlaying[0].getName()))
        {
            GameControl.diceSideThrown = down;
            GameControl.MovePlayer(1);
        }

        if (string.Equals(player.getName(), GameControl.listOfPlayersPlaying[1].getName()))
        {
            GameControl.diceSideThrown = down;
            GameControl.MovePlayer(2);
        }

        Debug.Log("Function 2");
    }
    // player gain additional 8 coins
    public static void gainCoins(Player player)
    {
        player.changeGameBalanceByAmount(8);
        Debug.Log("Function 4");
    }
    // player loses 10 coins
    public static void loseCoins(Player player)
    {
        player.changeGameBalanceByAmount(-10);
        Debug.Log("Function 5");
    }

    // player can select which minigame they want to play
    public void minigameSelect()
    {
        SelectMiniGameScript.Instance.Show();
        SelectMiniGameScript.Instance.showPopUp(SelectMiniGameScript.text, SelectMiniGameScript.infoString, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(GameControl.minigames[0]);
            Debug.Log("First Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(GameControl.minigames[3]);
            Debug.Log("Second Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(GameControl.minigames[1]);
            Debug.Log("Thirdth Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(GameControl.minigames[2]);
            Debug.Log("Fourth Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(GameControl.minigames[4]);
            Debug.Log("Fivth Button");
        });
        Debug.Log("Function 5");
    }

    public static void GetPowerup(Player player)
    {
        int num = Random.Range(2, 3);
        
        
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
            case 5:
                minigameSelect(player);
                break;
        }
        



    }
}
