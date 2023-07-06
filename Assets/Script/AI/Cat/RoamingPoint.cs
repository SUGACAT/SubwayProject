using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoamingPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.gameObject);
        
        if (other.transform.CompareTag("Monster"))
        {
            Debug.Log("Contact");
            other.GetComponent<CatMove>().Roaming();
        }
    }
}
