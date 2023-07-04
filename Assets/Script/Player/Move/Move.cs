using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum MoveState
{
    walking,
    running,
    crawling
}

public class Move : MonoBehaviour
{
    [Header("Values")]
    public float moveSpeed = 6;
    private Vector3 moveForce;

    public MoveState _moveState;

    [Header("Scripts")]
    private CharacterController characterController;
    private PlayerAnimationManager thePlayerAnimManager;
    private PlayerController thePlayerController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        thePlayerController = GetComponent<PlayerController>();
        thePlayerAnimManager = GetComponent<PlayerAnimationManager>();
    }

    void Update()
    {
        characterController.Move(moveForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 4;
            _moveState = MoveState.running;

            thePlayerController.DecreaseStamina = Time.deltaTime * 10;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = 1;
            _moveState = MoveState.crawling;
            thePlayerController.IncreaseStamina = Time.deltaTime * 5;
        }
        else
        {
            moveSpeed = 2;
            _moveState = MoveState.walking;
            thePlayerController.IncreaseStamina = Time.deltaTime * 4;
        }
    }

    public void For_Forward(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);

        if(direction == Vector3.zero)
        {
            thePlayerAnimManager.Idle();
            thePlayerController.IncreaseStamina = Time.deltaTime * 5;
        }
        else
            thePlayerAnimManager.Walk();
    }
}
