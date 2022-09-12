using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerInterface
{
    string getName();
}
public class PlayerController : MonoBehaviour, PlayerInterface
{
    private bool isJumping;
    private Rigidbody2D rb;
    public string pName;

    PlayerControls playerControls;
    public ETLController con;
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool keyDown = playerControls.Player1.Jump.ReadValue<float>() > 0.1f;

        if(keyDown && !isJumping)
        {
            rb.velocity = new Vector3(0, 5, 0);
            isJumping = true;
        }

        /*if(Input.GetKey(KeyCode.UpArrow) && !isJumping)
        {
            rb.velocity = new Vector3(0, 5, 0);
            isJumping = true;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit the lion");
            con.PlayerDies(this);
        }
    }

    public string getName()
    {
        return name;
    }
}
