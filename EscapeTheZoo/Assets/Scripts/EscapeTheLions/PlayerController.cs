using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Sounds
    public AudioSource playerJumpingAudio;

    private bool isJumping;
    private Rigidbody2D rb;
    public string pName;
    public KeyCode jumpKey;

    public ETLController con;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerJumpingAudio.mute = false;
    }

    void FixedUpdate()
    {
        //If touching the ground and jump key is held
        if(Input.GetKey(jumpKey) && !isJumping)
        {
            playerJumpingAudio.Play();
            rb.velocity = new Vector3(0, 5, 0);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Allow jumping again
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        //Lion has the enemy tag, this activates the end of the minigame
        if(collision.gameObject.CompareTag("Enemy"))
        {
            con.PlayerDies(this.name);
        }
    }

    public string getName()
    {
        return name;
    }
}
