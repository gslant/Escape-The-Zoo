using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsImplementation : MonoBehaviour
{

    [SerializeField]
    public GameObject player;

    private static Transform[] playerWaypoint;

    [HideInInspector]
    public static int waypointIndex;

    private float moveSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        playerWaypoint = player.GetComponent<Transform[]>();
        waypointIndex = player.GetComponent<int>();


    }

    // Update is called once per frame
    void Update()
    {

        transform.position = playerWaypoint[waypointIndex].transform.position;
    }

    public void moveup()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerWaypoint[waypointIndex+3].transform.position, moveSpeed * Time.deltaTime);

    }

    public void GetPowerup()
    {
        moveup();
    }

}
