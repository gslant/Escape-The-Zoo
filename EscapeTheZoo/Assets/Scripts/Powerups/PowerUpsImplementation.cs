using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUpsImplementation : MonoBehaviour
{
    public List<PowerupItem> items = new List<PowerupItem>();

    public static int up = 3;
    public static int down = -6;

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


    }
    // player is move down by 6 spaces(tiles)
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

    }
    // player gain additional 5 coins
    public static void gainCoins(Player player)
    {
        player.changeGameBalanceByAmount(5);
    }
    // player loses 10 coins
    public static void loseCoins(Player player)
    {
        player.changeGameBalanceByAmount(-10);
    }

    // player can select which minigame they want to play
    public void minigameSelect(Player player)
    {

        SelectMiniGameScript.text = player.getName() + " please select a minigame to play now:"; 

        SelectMiniGameScript.Instance.Show();
        SelectMiniGameScript.Instance.showPopUp(SelectMiniGameScript.text, SelectMiniGameScript.infoString, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[0]);
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[3]);
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[1]);
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[2]);
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[4]);
        });

    }

    public void GetPowerup(Player player)
    {
        int num = Random.Range(1, 5);

        switch (num)
        {
            case 1:
                GotPowerupScript.text = player.getName() + "\nYou got "+ items[0].Title + " powerup";
                GotPowerupScript.infoString = items[0].desc;
                GotPowerupScript.Instance.Show();
                moveup(player);
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.infoString, () =>{
                    control.dice.SetActive(true);

                });
                break;
            case 2:
                GotPowerupScript.text = player.getName() + "\nYou got " + items[1].Title + " powerup";
                GotPowerupScript.infoString = items[1].desc;
                GotPowerupScript.Instance.Show();
                movedown(player);
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.infoString, () =>{
                    control.dice.SetActive(true);

                });
                break;
            case 3:
                GotPowerupScript.text = player.getName() + "\nYou got " + items[2].Title + " powerup";
                GotPowerupScript.infoString = items[2].desc;
                GotPowerupScript.Instance.Show();
                gainCoins(player);
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.infoString, () => {
                    control.dice.SetActive(true);

                });
                break;
            case 4:
                GotPowerupScript.text = player.getName() + "\nYou got " + items[3].Title + " powerup";
                GotPowerupScript.infoString = items[3].desc;
                GotPowerupScript.Instance.Show();
                loseCoins(player);
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.infoString, () => {
                    control.dice.SetActive(true);

                });
                break;
        }

    }
}
