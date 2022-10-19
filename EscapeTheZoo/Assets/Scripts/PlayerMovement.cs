using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] waypoints;

    private static float moveSpeed = 10f;

    [HideInInspector]
    public int waypointIndex = 1;

    public bool myTurn = false;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (myTurn)
            Move();
    }

    private void Move()
    {
        if (GameControl.diceSideThrown < 0 && waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex - 2].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position.x == waypoints[waypointIndex - 2].transform.position.x && transform.position.y == waypoints[waypointIndex - 2].transform.position.y)
            {
                waypointIndex -= 1;
            }
        }
        else if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position.x == waypoints[waypointIndex].transform.position.x && transform.position.y == waypoints[waypointIndex].transform.position.y)
            {
                waypointIndex += 1;
            }
        }
    }
}

