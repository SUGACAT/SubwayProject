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
    public float defaultSpeed;
    public float moveSpeed = 6;
    private Vector3 moveForce;

    public MoveState _moveState;

    [SerializeField] bool isCrouching = false;
    [SerializeField] bool isRunning;
    public bool canRun = true;

    [Header("Scripts")]
    private CharacterController characterController;
    private PlayerAnimationManager thePlayerAnimManager;
    private PlayerController thePlayerController;

    [SerializeField]
    private float gravity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        thePlayerController = GetComponent<PlayerController>();
        thePlayerAnimManager = GetComponent<PlayerAnimationManager>();
    }

    void Update()
    {
        if (!characterController.isGrounded)
        {
            moveForce.y += gravity * Time.deltaTime;
        }

        switch (_moveState)
        {
            case MoveState.walking:
                thePlayerController.IncreaseStamina = Time.deltaTime * 4;
                break;
            case MoveState.running:
                thePlayerController.DecreaseStamina = Time.deltaTime * 10;
                break;
            case MoveState.crawling:
                thePlayerController.IncreaseStamina = Time.deltaTime * 5;
                break;
        }

        characterController.Move(moveForce * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            moveSpeed = 1;
            _moveState = MoveState.crawling;
            thePlayerAnimManager.Crouch();

            isCrouching = true;

            Debug.Log("1");
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl)) 
        {
            moveSpeed = 2;
            _moveState = MoveState.walking;
            thePlayerAnimManager.Walk();

            isCrouching = false;

            Debug.Log("2");
        }

        if (isCrouching) return;

        ////--------------------//

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!canRun) return;

            moveSpeed = 4;
            _moveState = MoveState.running;
            Debug.Log("3");
            isRunning = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        else if(!isRunning)
        {
            moveSpeed = 2;
            _moveState = MoveState.walking;
            thePlayerAnimManager.Walk();
            Debug.Log("4");
        }
    }

    public void For_Forward(Vector3 direction)
    {
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        moveForce = new Vector3(direction.x * (defaultSpeed + moveSpeed), moveForce.y, direction.z * (defaultSpeed + moveSpeed));

        if (direction == Vector3.zero)
        {
            thePlayerAnimManager.Idle();
            thePlayerController.IncreaseStamina = Time.deltaTime * 5;
        }
    }
}
