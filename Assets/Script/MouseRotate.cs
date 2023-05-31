using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseRotate : MonoBehaviour
{
    [SerializeField] private float rotatecamXspeed = 1;
    [SerializeField] private float rotatecamYspeed = 1;
    private float limitMinX = -80;
    private float limitMaxX = 50;
    private float AngleX;
    private float AngleY;
    
    public void UpdateRotate(float mouseX, float mouseY)
    {
        AngleY += mouseX * rotatecamYspeed;
        AngleX -= mouseY * rotatecamXspeed;
        AngleX = ClampAngle(AngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(AngleX, AngleY, 0);
    }
    
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
