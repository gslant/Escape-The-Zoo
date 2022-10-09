using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;
using static Player;

public class PowerupsImplementation : MonoBehaviour
{

    int playercoinearned = Player.getGameBalance();


    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveup()
    {
        Debug.Log("Function 1");
    }

    public void movedown()
    {
        Debug.Log("Function 2");
    }

    public int gainCoins(int playeramount)
    {
        return playeramount + 5;
    }

    public void GetPowerup()
    {
        int num = Random.Range(1, 4);

        switch (num)
        {
            case 1:
                if(num == 1)
                moveup();
                break;
            case 2:
                if(num == 2)
                movedown();
                break;
            case 3:
                gainCoins(playercoinearned);
                break;
        }
    }

}
