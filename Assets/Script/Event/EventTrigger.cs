using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public EventManager theEventManager;
    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        
        if (other.transform.CompareTag("Player"))
        {
            switch (GameManager.instance.eventNumber)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }

            triggered = true;
        }
    }
}
