using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform lookPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerManager>().isHiding)
            {
                return;
            }
            Debug.Log("Player Found");
            other.GetComponent<PlayerManager>().Death();
        }
    }
}
