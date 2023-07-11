using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;

    public bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DivisionType()
    {
        if (isOpened)
            Close();
        else
            Open();
    }

    public void Open()
    {
        Debug.Log("Door Opened");
        isOpened = true;
    }

    public void Close()
    {
        Debug.Log("Door Closed");
        isOpened = false;
    }
}
