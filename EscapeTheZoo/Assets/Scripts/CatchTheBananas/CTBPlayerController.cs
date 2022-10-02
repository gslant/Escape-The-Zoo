using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTBPlayerController : MonoBehaviour
{
    // This script is assigned to the player objects for movement

    Rigidbody2D rb;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;

    const float PLAYER_SPEED = 6f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {

        if (Input.GetKey(leftKey)) // move to the left
        {
            rb.velocity = new Vector2(-1 * PLAYER_SPEED, 0);
        }
        else if (Input.GetKey(rightKey)) // move to the right
        {
            rb.velocity = new Vector2(1 * PLAYER_SPEED, 0);
        }
        else // if no key is pressed, the player doesn't move
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
