using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWTHPushableObject : MonoBehaviour
{
    // Sounds
    public AudioSource pushableObjectDraggingAudio;
    public AudioSource pushableObjectFallingAudio;

    // Components
    private Transform pushableObject;
    private Rigidbody2D objectRigidbody;
    private BoxCollider2D boxCollide;

    // Players
    public DWTHPlayerController player1;
    public DWTHPlayerController player2;

    // Variables
    public DWTHPlayerController touchingPlayer; // The player that is currently touching the pushable object.
    private float scale = 1.0f; // Scale of the pushable object.
    private bool notTouchingBorder = true; // Keeps track of if the pushable object is or isn't touching the border.

    // Start is called before the first frame update
    void Start()
    {
        pushableObject = GetComponent<Transform>();
        objectRigidbody = GetComponent<Rigidbody2D>();
        boxCollide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stops the object in it's place whenever it's not being pushed
        if (notTouchingBorder)
        {
            objectRigidbody.velocity = new Vector3(0, 0, 0);
        }

        // If the object touches the border, play a small animation
        if (!notTouchingBorder)
        {
            objectRigidbody.rotation += 500.0f * Time.deltaTime;
            scale -= 0.25f * Time.deltaTime;
            pushableObject.localScale = new Vector3(scale, scale, scale);
        }

        // If the object can no longer be seen, destroy it
        if (scale <= 0.0f)
        {
            Destroy(pushableObject.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (notTouchingBorder && collision.gameObject.CompareTag("Border")) // If the pushable object is touching the border
        {
            //pushableObjectFallingAudio.Play();
            notTouchingBorder = false;
            Destroy(boxCollide);
            objectRigidbody.velocity = new Vector3(1, -1, 0);
            if (touchingPlayer != null)
            {
                touchingPlayer.numPushed++;
            }
            DWTHController.numObjects--;
        }
        else if (collision.gameObject.CompareTag("Player 1")) // If the pushable object is touching player 1
        {
            touchingPlayer = player1;
        }
        else if (collision.gameObject.CompareTag("Player 2")) // If the pushable object is touching player 2
        {
            touchingPlayer = player2;
        }
        else // If the pushable object is not touching either player
        {
            touchingPlayer = null;
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player 1") && player1.moving) // If the pushable object is touching player 1 and player 1 is moving
        {
            pushableObjectDraggingAudio.mute = false; // Unmute the dragging audio
        }
        else if (collision.gameObject.CompareTag("Player 2") && player2.moving) // If the pushable object is touching player 2 and player 2 is moving
        {
            pushableObjectDraggingAudio.mute = false; // Unmute the dragging audio
        } else
        {
            pushableObjectDraggingAudio.mute = true; // Mute the dragging audio
        }
    }*/

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the pushable object isn't being pushed by anything, mute the pushableObjectDraggingAudio
        if (collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2"))
        {
            //pushableObjectDraggingAudio.mute = true;
        }
    }
}