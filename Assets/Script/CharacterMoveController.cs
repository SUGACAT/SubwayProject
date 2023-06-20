using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMoveController : MonoBehaviour
{
    public float moveSpeed = 6;
    private Vector3 moveForce;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        characterController.Move(moveForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 6;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = 2;
        }
        else
        {
            moveSpeed = 4;
        }
    }
    public void For_Forward(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }
}
