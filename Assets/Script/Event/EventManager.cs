using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public CanvasManager theCanvasManager;
    public PlayerManager thePlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        GameStartEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStartEvent()
    {
        theCanvasManager.FadeImageEvent();
        thePlayerManager.ControlMove(false);
        thePlayerManager.LookFront();
    }

    public void GameStartEventOver()
    {
        thePlayerManager.ControlMove(true);
    }
}
