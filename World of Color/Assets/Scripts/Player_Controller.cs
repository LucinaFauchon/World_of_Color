﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
#pragma warning disable
    public Transform transform;
    public float movementSpeed = 5f;
    [HideInInspector]
    public Vector2 moveDir;

    //attack combat direction passthrough
    public Player_Combat combatdir;

    public void FixedUpdate()
    {
        if (moveDir.x > 0.1)
        {
            //move right
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.right;
        }
        else if (moveDir.x < -0.1)
        {
            //move left
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.left;
        }
        else if (moveDir.y > 0.1)
        {
            //move up
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.up;
        }
        else if (moveDir.y < -0.1)
        {
            //move down
            transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
            combatdir.attackdir = Player_Combat.AttackDir.down;
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
}
