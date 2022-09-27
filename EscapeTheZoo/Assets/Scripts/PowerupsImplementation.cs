using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsImplementation : MonoBehaviour
{
    private static GameObject player1, player2;

    private List<Gameobject> playerObject;



    private const int moveback = 5;
    private const int getcoins = 3;
    


    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void gain3coins()
    {
        player1
    }

    public void lose3coins()
    {

    }
}
