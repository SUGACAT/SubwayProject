using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MouseRotate M_Rotate;
    private CharacterMoveController move;    
    
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        M_Rotate = GetComponent<MouseRotate>();
        move = GetComponent<CharacterMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotate();
        UpdateMove();
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
        move.For_Forward(new Vector3(dir_x, 0, dir_z));
    }
}
