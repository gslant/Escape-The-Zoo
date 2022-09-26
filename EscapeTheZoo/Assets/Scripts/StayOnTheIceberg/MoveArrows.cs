using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrows : MonoBehaviour
{

    public CharacterController2D controller;

    public float moveSpeed = 40f;

    float horizontalMove = 0;

    bool jump = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontalMove = 1 * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
