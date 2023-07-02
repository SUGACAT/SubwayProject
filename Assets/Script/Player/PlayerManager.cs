using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerController thePlayerController;
    private CharacterController theCharacterController;
    public EventManager theEventManager;
    public CanvasManager theCanvasManager;

    [Header("Prefabs")]
    public GameObject flash_Obj, defaultLight;

    public bool isHiding;

    private void Awake()
    {
        thePlayerController = GetComponent<PlayerController>();
        theCharacterController = GetComponent<CharacterController>();
    }
    
    public void GetFlash()
    {
        flash_Obj.SetActive(true);
        defaultLight.SetActive(false);
    }

    public void PlayEvent(int value)
    {
        switch (value)
        {
            case 0:
                theEventManager._Event0("Start");
                break;
            case 1:
                theEventManager._Event1();
                break;
            case 2:
                break;
        }
    }

    public void Hide(string type, Vector3 pos, ref bool value)
    {
        if (type == "In")
        {
            isHiding = true;
            theCanvasManager.SetHideImage(true);
            theCanvasManager.SetInteractObject(true);

            ControlMove(false, false);

            value = true;
        }
        else
        {
            isHiding = false;
            theCanvasManager.SetHideImage(false);
            theCanvasManager.SetInteractObject(false);

            ControlMove(true, true);

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
    
    public float ControlStamina() => thePlayerController.ControlStamina();
    public void ControlMove(bool move, bool rotate) { thePlayerController.canMove = move; thePlayerController.canRotate = rotate; }
    public void LookFront() => thePlayerController.LookFront();
    public void LerpRotation(Vector3 dir, float speed) => thePlayerController.LerpRotation(dir, speed);
}
