using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWTHPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public string pName;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    public DWTHController con;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        if (Input.GetKey(upKey) || Input.GetKey(downKey) || Input.GetKey(rightKey) || Input.GetKey(leftKey))
        {
            rb.velocity = new Vector3(((Input.GetKey(leftKey) ? 1 : 0) * -5) + ((Input.GetKey(rightKey) ? 1 : 0) * 5), ((Input.GetKey(downKey) ? 1 : 0) * -5) + ((Input.GetKey(upKey) ? 1 : 0) * 5), 0);
        } else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public string getName()
    {
        return name;
    }
}
