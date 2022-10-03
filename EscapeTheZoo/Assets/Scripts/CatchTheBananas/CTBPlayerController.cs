using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTBPlayerController : MonoBehaviour
{
    // This script is assigned to the player objects for movement

    [SerializeField] GameObject minigameManager;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode upKey;

    Rigidbody2D rb;
    CTBManager ctbManager;

    const float PLAYER_SPEED = 6f;

    bool canJump = true;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ctbManager = minigameManager.GetComponent<CTBManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoves();
    }

    void PlayerMoves()
    {
        if (canMove)
        {
            if (Input.GetKey(upKey) && canJump == true) // jump up
            {
                rb.velocity = new Vector2(0, PLAYER_SPEED);
                canJump = false;
            }
            else if (Input.GetKey(leftKey)) // move to the left
            {
                rb.velocity = new Vector2(-1 * PLAYER_SPEED, rb.velocity.y);
            }
            else if (Input.GetKey(rightKey)) // move to the right
            {
                rb.velocity = new Vector2(1 * PLAYER_SPEED, rb.velocity.y);
            }
            else // if no key is pressed, the player stops moving
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
        if (collision.gameObject.CompareTag("Banana"))
        {
            Destroy(collision.gameObject);
            ctbManager.IncrementScore(name); // The player's score is incremented in the CTB manager
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            PlayerSlips();
            StartCoroutine(StunPlayer());
        }
    }

    private void PlayerSlips()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 4, rb.velocity.y);
    }

    IEnumerator StunPlayer()
    {
        canMove = false;
        Debug.Log("stunned!");
        yield return new WaitForSeconds(2);
        canMove = true;
    }
}
