using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWTHPlayerController : MonoBehaviour
{
    // Sounds
    public AudioSource playerDeath;

    // Objects
    public GameObject silhouette;
//    public DWTHController con;

    // Components
    private Transform playerTransform;
    private Rigidbody2D objectRigidbody;
    private BoxCollider2D boxCollide;

    // Variables
    public int numPushed = 0; // Number of pushable objects pushed off the shelf.
    public bool alive = true; // Keeps track of if the player is alive.
    private int deathTimePlusThree; // The time 3 Seconds after the player dies.
    public bool moving = false; // Keeps track of if the player is moving.

    // Input Keys
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        objectRigidbody = GetComponent<Rigidbody2D>();
        boxCollide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            // Player Movement
            if (Input.GetKey(upKey) || Input.GetKey(downKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey))
            {
                moving = true;
                objectRigidbody.velocity = new Vector3(((Input.GetKey(leftKey) ? 1 : 0) * -5) + ((Input.GetKey(rightKey) ? 1 : 0) * 5), ((Input.GetKey(downKey) ? 1 : 0) * -5) + ((Input.GetKey(upKey) ? 1 : 0) * 5), 0);

                // If the player is moving while the human is looking around
                /*if (silhouette.activeSelf == true)
                {
                    alive = false;
                    Destroy(boxCollide);
                    objectRigidbody.velocity = new Vector3(-5, +5, 0);
                    deathTimePlusThree = (int)Time.fixedTime + 3;
                    playerDeath.Play();
                }*/
            }
            else
            {
                moving = false;
                objectRigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
        else if ((int)Time.fixedTime < deathTimePlusThree) // When the player is dead, play death animation
        {
            objectRigidbody.rotation += 500.0f * Time.deltaTime;
        }
        /*else // When the player has been dead of 3 seconds, update the number of players remaining
        {
            DWTHController.numberOfPlayersRemaining--;
        }*/
    }

    public string getName()
    {
        return name;
    }
}
