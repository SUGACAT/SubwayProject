using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Events
{
    DeathEvent,
    FlashLightEvent,
    LobbyAppearEvent,
}

[System.Serializable]
public class Event
{
    [Header("Progress")]
    public string eventName;
    public float[] progressDuration;
    
    [Header("values")]
    public float moveDuration;
    public Transform[] startPos;
}

public class EventManager : MonoBehaviour
{
    [Header("Events")]
    public Event[] Event_List;

    [Header("Prefabs")]
    public GameObject e_CatMonster, e_RatMonster;
    public GameObject deathObject;
    
    [Header("Scripts")]
    public CanvasManager theCanvasManager;
    public PlayerManager thePlayerManager;
    public MonsterSpawner theMonsterSpawner;

    // Start is called before the first frame update
    void Start()
    {
        AwakeEvent("Start");
    }

    public void AwakeEvent(string type)
    {
        if (type == "Start")
        {
            theCanvasManager.FadeImageEvent();
            thePlayerManager.LookFront();
            
            thePlayerManager.SetRotate(true);
        }   
    }

    public void FirstAppearEvent()
    {
        StartCoroutine("FlashEventCoroutine");
    }
    
    IEnumerator FlashEventCoroutine()
    {
        int number = GameManager.instance.eventNumber;
        
        thePlayerManager.ControlMove(false);
        thePlayerManager.LerpRotation(Vector3.zero, 1f);

        yield return new WaitForSeconds(Event_List[0].progressDuration[0]);
        
        thePlayerManager.SetRotate(false);

        e_CatMonster.transform.position = Event_List[0].startPos[0].position;
        e_CatMonster.SetActive(true);

        yield return new WaitForSeconds(Event_List[0].progressDuration[1]);

        thePlayerManager.ControlMove(true);

        yield return new WaitUntil(() =>  thePlayerManager.isHiding);

        Debug.Log("NICE");

        yield return new WaitForSeconds(Event_List[0].progressDuration[2]);

        e_CatMonster.SetActive(false);
        Debug.Log("Monster Is Gone");
        GameManager.instance.StartMonsterSpawn(1);
    }

    /*public void LobbyAppearEvent()
    {
        e_CatMonster.transform.position = Event_List[2].startPos[0].position;
        e_CatMonster.SetActive(true);
        
        Debug.Log("LobbyAppear");
    }*/
    
    public void DeathEvent(bool type)
    {
        thePlayerManager.gameObject.SetActive(!type);
        thePlayerManager.LookFront();
        if (type)
        {
            deathObject.SetActive(true);
            thePlayerManager.gameObject.transform.position = GameManager.instance.SpawnPos();
            StartCoroutine(DeathCoroutine());
        }
        else
        {
            deathObject.SetActive(false);
            thePlayerManager.SetRotate(false);
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(Event_List[1].progressDuration[0]);
        
        DeathEvent(false);
    }
}
