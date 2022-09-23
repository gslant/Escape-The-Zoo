using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isJumping;
    private Rigidbody2D rb;
    public string pName;
    public KeyCode jumpKey;

    public ETLController con;
    void Start()
    {
        Debug.Log(pName);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(jumpKey) && !isJumping)
        {
            rb.velocity = new Vector3(0, 5, 0);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(pName + " Hit the lion");
            con.PlayerDies(this.name);
        }
    }

    public string getName()
    {
        return name;
    }
}
