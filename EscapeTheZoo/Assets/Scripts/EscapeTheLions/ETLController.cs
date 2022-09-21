using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETLController : MonoBehaviour
{
    public PlayerController p1;
    public PlayerController p2;

    public void PlayerDies(string name)
    {
        if (name == "player1")
        {
            Debug.Log("player 1 died");
            //p1.SetActive(false);
        }
        else
        {
            Debug.Log("player 2 died");
        }

        //show game over screen/winner recieving coins?
        //notify scenemanager that the minigame is complete
        //notify gamemanager that the minigame is complete
        //transition back to board
    }
}
