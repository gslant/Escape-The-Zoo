 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 1f;

    [HideInInspector]
    public int waypointIndex = 0;

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
        //Debug.Log(waypointIndex);
        //Debug.Log(waypoints.Length - 1);
        //Debug.Log(waypoints[waypointIndex]);
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            //Debug.Log(transform.position.x);
            //Debug.Log(transform.position.y);
            //Debug.Log(transform.position.z);
            //Debug.Log(waypoints[waypointIndex].transform.position.x);
            //Debug.Log(waypoints[waypointIndex].transform.position.y);
            //Debug.Log(waypoints[waypointIndex].transform.position.z);
            //Debug.Log(waypoints[waypointIndex].transform.position.Equals(transform.position));

            if (waypointIndex <= waypoints.Length - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

                if (transform.position.x == waypoints[waypointIndex].transform.position.x && transform.position.y == waypoints[waypointIndex].transform.position.y)
                {
                    waypointIndex += 1;
                }
            }
        }
    }
}

