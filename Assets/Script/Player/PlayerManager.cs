using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Scripts")]
    private PlayerController thePlayerController;
    public EventManager theEventManager;
    
    [Header("Prefabs")]
    public GameObject flash_Obj, defaultLight;
    
    private void Awake()
    {
        thePlayerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                theEventManager._Event1("Start");
                break;
            case 2:
                break;
        }
    }

    public void ControlMove(bool move, bool rotate) { thePlayerController.canMove = move; thePlayerController.canRotate = rotate; }

    public void LookFront() => thePlayerController.LookFront();

   // public void LookZero() => thePlayerController.LookZero();

    public void LerpRotation(Vector3 dir, float speed) => thePlayerController.LerpRotation(dir, speed);
}
