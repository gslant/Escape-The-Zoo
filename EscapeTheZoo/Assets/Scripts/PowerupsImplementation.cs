using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsImplementation : MonoBehaviour
{

    [SerializeField]
    public GameObject p1, p2;

    private Transform[] player1Waypoint, player2Waypoint;

    [HideInInspector]
    public int waypointIndexp1, waypointIndexp2;


    //public static listOfPlayersPlaying;


    // Start is called before the first frame update
    void Start()
    {
        player1Waypoint = p1.GetComponent<Transform[]>();
        player2Waypoint = p2.GetComponent<Transform[]>();
        waypointIndexp1 = p1.GetComponent<int>();
        waypointIndexp2 = p2.GetComponent<int>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player1Waypoint[waypointIndexp1].transform.position;
        transform.position = player2Waypoint[waypointIndexp2].transform.position;

        moveup();
    }

    public void moveup()
    {
        transform.position = player1Waypoint[waypointIndexp1++].transform.position;
    }

}
