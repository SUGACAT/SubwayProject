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
                theEventManager.AppearMonsterEvent();
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    
    public void ControlMove(bool value) => thePlayerController.canMove = value;

    public void LookFront() => thePlayerController.LookFront();
}
