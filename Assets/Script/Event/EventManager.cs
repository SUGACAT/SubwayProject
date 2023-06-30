using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    [Header("Name")]
    public string eventName;
    [Header("Time")]
    public float[] duration;
    public Animator _event;
}

public class EventManager : MonoBehaviour
{
    [Header("Events")]
    public Event[] Event_List;
    
    [Header("Scripts")]
    public CanvasManager theCanvasManager;
    public PlayerManager thePlayerManager;
    public MonsterSpawner theMonsterSpawner;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerManager.PlayEvent(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _Event0(string type)
    {
        if (type == "Start")
        {
            theCanvasManager.FadeImageEvent();
            thePlayerManager.LookFront();
        }   
    }

    public void _Event1()
    {
        StartCoroutine("Event1Coroutine");
    }
    
    IEnumerator Event1Coroutine()
    {
        thePlayerManager.ControlMove(false, false);
        thePlayerManager.LerpRotation(Vector3.zero, 1f);

        yield return new WaitForSeconds(Event_List[0].duration[0]);

        thePlayerManager.LookFront();
        thePlayerManager.ControlMove(false, true);
        
        yield return new WaitForSeconds(Event_List[0].duration[1]);

        thePlayerManager.ControlMove(true, true);
        Event_List[0]._event.gameObject.SetActive(false);
    }
}
