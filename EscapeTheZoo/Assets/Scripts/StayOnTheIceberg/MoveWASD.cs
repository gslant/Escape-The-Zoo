using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{

    public CharacterController2D controller;

    public float moveSpeed = 40f;

    float horizontalMove = 0;

    bool jump = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            horizontalMove = 1 * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            horizontalMove = -1 * moveSpeed;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
