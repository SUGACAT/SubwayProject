using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private MouseRotate M_Rotate;
    private Move theMoveController;

    public bool canMove = true;

    public bool canRotate = true;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        M_Rotate = GetComponent<MouseRotate>();
        theMoveController = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate == false) return;
        UpdateRotate();
        
        if(canMove == false) return;
        UpdateMove();
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
        
        Debug.Log("1");
    }

    private void UpdateMove()
    {
        float dir_x = Input.GetAxisRaw("Horizontal");
        float dir_z = Input.GetAxisRaw("Vertical");
        theMoveController.For_Forward(new Vector3(dir_x, 0, dir_z));
        
        Debug.Log("2");
    }
}
