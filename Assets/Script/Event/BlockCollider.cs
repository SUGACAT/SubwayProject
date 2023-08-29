using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public Material myMaterial;

    public bool isIn;
    public float alphaValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isIn)
        {
            alphaValue += Time.deltaTime * 5;
        }
        else
            alphaValue -= Time.deltaTime * 5;
        
        myMaterial.color = new Color(255, 255, 255, alphaValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isIn = true;
            print(("1"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isIn = false;
        
        if (other.transform.CompareTag("Player"))
        {
            print(("2"));
        }
    }
}
