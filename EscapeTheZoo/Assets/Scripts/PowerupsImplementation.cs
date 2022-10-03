using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameControl;
using static Player;

public class PowerupsImplementation : MonoBehaviour
{

    [SerializeField] public Player p1, p2;


    //public static listOfPlayersPlaying;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SettingPlayers();

    }

    public void SettingPlayers()
    {
        p1 = listOfPlayersPlaying[0];
        p2 = listOfPlayersPlaying[1];
    }

    public void gain3coins()
    {

    }

    public void lose3coins()
    {

    }
}
