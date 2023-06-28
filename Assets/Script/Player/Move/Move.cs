using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    public float moveSpeed = 6;
    private Vector3 moveForce;
    private CharacterController characterController;

    private PlayerAnimationManager thePlayerAnimManager;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        thePlayerAnimManager = GetComponent<PlayerAnimationManager>();
    }

    void Update()
    {
        characterController.Move(moveForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 4;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = 1;
        }
        else
        {
            moveSpeed = 2;
        }
    }

    public void For_Forward(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);

        if(direction == Vector3.zero)
        {
            thePlayerAnimManager.Idle();
        }
        else
            thePlayerAnimManager.Walk();
    }
}
