using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTBPlayerController : MonoBehaviour
{
    // Sounds
    public AudioSource bananaCaughtAudio;
    public AudioSource slippingOnBananaAudio;
    public AudioSource playerJumpAudio;

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

    void FixedUpdate()
    {
        PlayerMoves();
    }

    void PlayerMoves()
    {
        int leftHorizontal = -1;
        int rightHorizontal = 1;

        if (canMove)
        {
            if (Input.GetKey(upKey) && canJump == true)
            {
                playerJumpAudio.Play();
                rb.velocity = new Vector2(0, PLAYER_SPEED);
                canJump = false;
            }
            else if (Input.GetKey(leftKey))
            {
                rb.velocity = new Vector2(leftHorizontal * PLAYER_SPEED, rb.velocity.y);
            }
            else if (Input.GetKey(rightKey))
            {
                rb.velocity = new Vector2(rightHorizontal * PLAYER_SPEED, rb.velocity.y);
            }
            else // if no key is pressed, the player stops moving
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            canJump = true;
        }
        if (collision.gameObject.CompareTag("Banana"))
        {
            Destroy(collision.gameObject);
            bananaCaughtAudio.Play();
            ctbManager.IncrementScore(name);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            slippingOnBananaAudio.Play();
            PlayerSlips();
            StartCoroutine(StunPlayer());
        }
    }

    private void PlayerSlips()
    {
        int slipSpeed = 4;
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * slipSpeed, rb.velocity.y);
    }

    IEnumerator StunPlayer()
    {
        canMove = false;
        yield return new WaitForSeconds(2);
        canMove = true;
    }
}
