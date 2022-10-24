using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsImplementation : MonoBehaviour
{
    public List<PowerupItem> items = new List<PowerupItem>();
    public static int up = 3;
    public static int down = -5;

    public GameControl control;
    public SelectMiniGameScript selectMini;
    public GotPowerupScript popup;

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
    public void minigameSelect(Player player)
    {

        SelectMiniGameScript.text = player.getName() + " please select a minigame to play now:"; 

        SelectMiniGameScript.Instance.Show();
        SelectMiniGameScript.Instance.showPopUp(SelectMiniGameScript.text, SelectMiniGameScript.infoString, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[0]);
            Debug.Log("First Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[3]);
            Debug.Log("Second Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[1]);
            Debug.Log("Thirdth Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[2]);
            Debug.Log("Fourth Button");
        }, () => {
            control.unreloadObjs();
            SceneLoader.LoadMinigameAdditive(control.minigames[4]);
            Debug.Log("Fivth Button");
        });
        Debug.Log("Function 5");
    }

    public void GetPowerup(Player player)
    {
        int num = Random.Range(1, 6);
        
        
        switch (5)
        {
            case 1:
                GotPowerupScript.text = player.getName() + "\nYou got "+ items[0].Title + " powerup";
                GotPowerupScript.infoString = items[0].desc;
                GotPowerupScript.image = items[0].sprite;
                GotPowerupScript.Instance.Show();
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.image, GotPowerupScript.infoString, () => {
                    GotPowerupScript.Instance.Hide();
                    moveup(player);
                });
                break;
            case 2:
                GotPowerupScript.text = player.getName() + "\nYou got " + items[1].Title + " powerup";
                GotPowerupScript.infoString = items[1].desc;
                GotPowerupScript.image = items[1].sprite;
                GotPowerupScript.Instance.Show();
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.image, GotPowerupScript.infoString, () => {
                    GotPowerupScript.Instance.Hide();
                    movedown(player);
                });
                break;
            case 3:
                GotPowerupScript.text = player.getName() + "\nYou got " + items[2].Title + " powerup";
                GotPowerupScript.infoString = items[2].desc;
                GotPowerupScript.image = items[3].sprite;
                GotPowerupScript.Instance.Show();
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.image, GotPowerupScript.infoString, () => {
                    GotPowerupScript.Instance.Hide();
                    gainCoins(player);
                });
                break;
            case 4:
                GotPowerupScript.text = player.getName() + "\nYou got " + items[3].Title + " powerup";
                GotPowerupScript.infoString = items[3].desc;
                GotPowerupScript.image = items[3].sprite;
                GotPowerupScript.Instance.Show();
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.image, GotPowerupScript.infoString, () => {
                    GotPowerupScript.Instance.Hide();
                    loseCoins(player);
                });
                break;
            case 5:
                
                GotPowerupScript.text = player.getName() + "\nYou got " + items[4].Title + " powerup";
                GotPowerupScript.infoString = items[4].desc;
                GotPowerupScript.image = items[4].sprite;
                GotPowerupScript.Instance.Show();
                GotPowerupScript.Instance.PowerupPopUp(GotPowerupScript.text, GotPowerupScript.image, GotPowerupScript.infoString, () => {
                    GotPowerupScript.Instance.Hide();
                    minigameSelect(player);
                });
                break;
        }

    }
}
