using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTFPlayerController : MonoBehaviour
{
    public CTFController con;

    // Components
    private Transform playerTransform;
    private Rigidbody2D objectRigidbody;
    private BoxCollider2D boxCollide;


    // Input Keys
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    Vector2 movement;
    bool isMoving;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        objectRigidbody = GetComponent<Rigidbody2D>();
        boxCollide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        /* Player Movement
        if (Input.GetKey(upKey) || Input.GetKey(downKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey))
        {
            objectRigidbody.velocity = new Vector3(((Input.GetKey(leftKey) ? 1 : 0) * -5) + ((Input.GetKey(rightKey) ? 1 : 0) * 5), ((Input.GetKey(downKey) ? 1 : 0) * -5) + ((Input.GetKey(upKey) ? 1 : 0) * 5), 0);               
        }
        else
        {
            objectRigidbody.velocity = new Vector3(0, 0, 0);
        }*/
        

    }

    void Movement()
    {
        if (Input.GetKey(upKey))
        {
            isMoving = true;
            movement.y = movementSpeed;
        }
        if (Input.GetKey(downKey))
        {
            isMoving = true;
            movement.y = -movementSpeed;
        }
        if (Input.GetKey(rightKey))
        {
            isMoving = true;
            movement.x = movementSpeed;
        }
        if (Input.GetKey(leftKey))
        {
            isMoving = true;
            movement.x = -movementSpeed;
        }
        if (!isMoving)
        {
            movement.x = 0;
            movement.y = 0;
        }

        

        objectRigidbody.MovePosition(objectRigidbody.position + movement.normalized);
        isMoving = false;
    }

    public string getName()
    {
        return name;
    }
}
