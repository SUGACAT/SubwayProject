using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MouseRotate M_Rotate;
    private Move theMoveController;

    public bool canMove = true;

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
        if (canMove == false) return;

        UpdateRotate();
        UpdateMove();
    }

    public void LookFront() 
    {
        M_Rotate.UpdateRotate(90f, 0f);
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
