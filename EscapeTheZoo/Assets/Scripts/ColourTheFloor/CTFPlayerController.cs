using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public KeyCode testKey;

    Vector2 movement;
    bool isMoving;
    public float movementSpeed;
    private float powerUpStartTime;

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
        //ExpandCollider();
    }

    void ExpandCollider()
    {
        if (Input.GetKey(testKey) && powerUpStartTime == 0)
        {
            powerUpStartTime = Time.time;
            Vector3 initSize = boxCollide.size;
            boxCollide.size = new Vector3(5, 5, 0);

        }
        if (Time.time > powerUpStartTime + 1f)
        {
            boxCollide.size = new Vector3(0.1f, 0.1f, 0);
            powerUpStartTime = 0;
        }
    }

    void Movement()
    {
        if (Input.GetKey(upKey))
        {
            movement.x = 0;
            isMoving = true;
            movement.y = movementSpeed;
        }
        if (Input.GetKey(downKey))
        {
            movement.x = 0;
            isMoving = true;
            movement.y = -movementSpeed;
        }
        if (Input.GetKey(rightKey))
        {
            movement.y = 0;
            isMoving = true;
            movement.x = movementSpeed;
        }
        if (Input.GetKey(leftKey))
        {
            movement.y = 0;
            isMoving = true;
            movement.x = -movementSpeed;
        }
        if (!isMoving)
        {
            movement.x = 0;
            movement.y = 0;
        }

        
        objectRigidbody.MovePosition(objectRigidbody.position + movement);
        isMoving = false;
    }

    public string getName()
    {
        return name;
    }
}
