using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public bool isFloor1Trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if(isFloor1Trigger)
            {
                GameManager.instance.isFloor1 = true;
            }
            else
            {
                GameManager.instance.isFloor1 = false;
            }
        }
    }
}
