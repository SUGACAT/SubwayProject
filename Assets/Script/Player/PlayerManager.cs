using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerController thePlayerController;
    [HideInInspector] public CharacterController theCharacterController;
    public EventManager theEventManager;
    public CanvasManager theCanvasManager;
    private MouseRotate theMouseRotate;
    private FlashLightManager theFlashLightManager;

    [Header("Prefabs")]
    public GameObject flash_Obj, defaultLight;

    public bool isHiding;
    public bool isWaiting = false;

    public int currentHeart;

    private void Awake()
    {
        thePlayerController = GetComponent<PlayerController>();
        theCharacterController = GetComponent<CharacterController>();
        theMouseRotate = GetComponent<MouseRotate>();
        theFlashLightManager = GetComponentInChildren<FlashLightManager>();
    }

    private void Start()
    {
        
    }

    public void SetRotate(bool type)
    {
        isWaiting = true;
        if (!type)
        {
            isWaiting = false;
            return;
        }

        theMouseRotate.AngleX = 0;
        theMouseRotate.AngleY = 90;
    }

    public void GetFlash()
    {
        flash_Obj.SetActive(true);
        defaultLight.SetActive(false);
    }

    public void Hide(string type, Vector3 pos, ref bool value)
    {
        if (type == "In")
        {
            isHiding = true;
            theCanvasManager.SetHideImage(true);
            theCanvasManager.SetInteractObject(true);

            ControlMove(false);
            thePlayerController.canRotate = false;

            value = true;
        }
        else
        {
            isHiding = false;
            theCanvasManager.SetHideImage(false);
            theCanvasManager.SetInteractObject(false);

            ControlMove(true);
            thePlayerController.canRotate = true;

            value = false;
        }

        SetPlayerPosition(pos);
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        theCharacterController.enabled = false;
        this.transform.position = pos;
        theCharacterController.enabled = true;
    }
    
    public void Death(Vector3 targetPos)
    {
        theEventManager.DeathEvent(true);
        Debug.Log("You Died");
        SetRotate(true);
    }

    public void AddBattery() => theFlashLightManager.AddBattery();
    public void IncreaseSpeed() => thePlayerController.IncreaseMoveSpeed();
    public float ControlStamina() => thePlayerController.ControlStamina();
    public void AddStamina() => thePlayerController.AddStamina();
    public void ControlMove(bool move) { thePlayerController.canMove = move;}
    public void LookFront() => transform.rotation = Quaternion.Euler(0, 90, 0);
    public void LerpRotation(Vector3 dir, float speed) => thePlayerController.LerpRotation(dir, speed);
}
