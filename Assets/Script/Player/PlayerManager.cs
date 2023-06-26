using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerController thePlayerController;

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

    public void ControlMove(bool value) => thePlayerController.canMove = value;

    public void LookFront() => thePlayerController.LookFront();
}
