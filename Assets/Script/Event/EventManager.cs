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
    [Header("Values")]
    public bool isKeyring = false;
    public string currentKey;

    public int keyInputCount;
    public int keyLength;

    private KeyCode keyCode1;
    private KeyCode keyCode2;

    [Header("Events")]
    public Event[] Event_List;

    [Header("Prefabs")]
    public GameObject e_CatMonster, e_RatMonster;
    public GameObject deathObject;

    public GameObject leverKeyObj, catKeyObj;
    
    [Header("Scripts")]
    public CanvasManager theCanvasManager;
    public PlayerManager thePlayerManager;
    public MonsterSpawner theMonsterSpawner;

    // Start is called before the first frame update
    void Start()
    {
        AwakeEvent("Start");
    }

    private void Update()
    {
        if (isKeyring)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                thePlayerManager.theCanvasManager.HideUI(false);
                thePlayerManager.gameObject.SetActive(true);
                switch (currentKey)
                {
                    case "Lever":
                        leverKeyObj.gameObject.SetActive(false);
                        break;
                    case "Cat":
                        catKeyObj.gameObject.SetActive(false);
                        break;
                }

                isKeyring = false;
            }
        }

        if (keyInputCount < 0) keyInputCount = 0;

        if (Input.GetKeyDown(keyCode1))
        {
            keyInputCount++;
        }
        else if(Input.GetKeyUp(keyCode1))
        {
            keyInputCount--;
        }

        if (keyLength != 2) return;

        if(Input.GetKeyDown(keyCode2))
        {
            keyInputCount++;
        }
        else if(Input.GetKeyUp(keyCode2))
        {
            keyInputCount--;
        }

        if(keyInputCount == 1 && keyLength == 1)
        {
            Debug.Log("object unlocked_1");
            keyInputCount = 0;
            keyCode1 = KeyCode.None;
            keyCode2 = KeyCode.None;
        }
        else if(keyInputCount == 2 && keyLength == 2)
        {
            Debug.Log("object unlocked_2");
            keyInputCount = 0;
            keyCode1 = KeyCode.None;
            keyCode2 = KeyCode.None;
        }
    }

    public void AwakeEvent(string type)
    {
        if (type == "Start")
        {
            theCanvasManager.FadeImageEvent();
            thePlayerManager.LookFront();
            
            //thePlayerManager.SetRotate(true); // Set Player Roataion false
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

    public void ShowKeyringEvent(string codeName)
    {
        isKeyring = true;

        thePlayerManager.theCanvasManager.HideUI(true);
        thePlayerManager.gameObject.SetActive(false);

        switch (codeName)
        {
            case "LeverKeyring":
                leverKeyObj.gameObject.SetActive(true);
                currentKey = "Lever";

                keyCode1 = KeyCode.F;
                keyLength = 1;
                break;
            case "CatKeyring":
                catKeyObj.gameObject.SetActive(true);
                currentKey = "Cat";

                keyCode1 = KeyCode.E;
                keyCode2 = KeyCode.B;
                keyLength = 2;
                break;
        }
    }
    
    public void ShowMissionSceneEvent(string name)
    {

    }

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
