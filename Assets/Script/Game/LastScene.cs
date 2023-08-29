using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
           GameManager.instance.theSceneManager.MoveToCreditScene();
    }
}
