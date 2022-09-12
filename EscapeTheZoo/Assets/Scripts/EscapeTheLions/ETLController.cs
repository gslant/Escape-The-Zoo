using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETLController : MonoBehaviour
{
    public PlayerController p1;
    public Player2Controller p2;
    //public Player2Controller p2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDies(PlayerInterface p)
    {
        if (p == p1)
        {
            Debug.Log("player 1 died");
            p1.SetActive(false);
        }
        else
        {
            Debug.Log("player 2 died");
        }

        //}
        //Debug.Log("Player " + Player + "dies");
        //Debug.Log("Player " + )
    }
}
