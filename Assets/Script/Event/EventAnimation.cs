using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    public EventManager theEventManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateActive()
    {
        theEventManager._Event1("Rotate");
    }

    public void _Event1Over()
    {
        theEventManager._Event1("End"); 
    }
}
