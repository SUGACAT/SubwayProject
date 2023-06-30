using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Values")]
    public bool canMove = true;
    public bool canRotate = true;

    public float currentStamina;
    public float maxStamina;

    [Header("Scripts")]
    private MouseRotate M_Rotate;
    private Move theMoveController;

    public float DecreaseStamina { get => currentStamina; set => currentStamina -= value; }
    public float IncreaseStamina { get => currentStamina; set => currentStamina += value; }

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        M_Rotate = GetComponent<MouseRotate>();
        theMoveController = GetComponent<Move>();
    }

    private void Start()
    {
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate == false) return;
        UpdateRotate();
        
        if(canMove == false) return;
        UpdateMove();
    }

    public float ControlStamina()
    {
        currentStamina = currentStamina >= maxStamina ? maxStamina : currentStamina;

        return currentStamina;
    }
    
    public void LookFront() 
    {
        M_Rotate.UpdateRotate(90f, 0f);
    }

    public void LerpRotation(Vector3 dir, float speed)
    {
        transform.DORotate(dir, speed, RotateMode.Fast);
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        M_Rotate.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {
        float dir_x = Input.GetAxisRaw("Horizontal");
        float dir_z = Input.GetAxisRaw("Vertical");
        theMoveController.For_Forward(new Vector3(dir_x, 0, dir_z));
    }
}
