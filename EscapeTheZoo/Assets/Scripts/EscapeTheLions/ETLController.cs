using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETLController : MonoBehaviour
{
    public PlayerController p1;
    public PlayerController p2;
    //public Player2Controller p2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }
}
