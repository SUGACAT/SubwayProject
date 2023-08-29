using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

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

    public GameObject leverKeyObj, catKeyObj;
    public Animator showEvent;

    public GameObject[] blockCollider;

    [Header("Prefabs")]
    public GameObject e_CatMonster, e_RatMonster;
    public GameObject deathObject;

    [Header("Scripts")]
    public CanvasManager theCanvasManager;
    public PlayerManager thePlayerManager;
    public MonsterSpawner theMonsterSpawner;

    public int alpha = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        AwakeEvent("Start");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isKeyring)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (alpha >= 1)
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
        }

        if (keyInputCount == 1 && keyLength == 1)
        {
            alpha++;
            
            Debug.Log("object unlocked_1");
            keyInputCount = 0;
            keyCode1 = KeyCode.None;
            keyCode2 = KeyCode.None;

            ShowMissionSceneEvent("LeverKeyring");
        }
        else if (keyInputCount == 2 && keyLength == 2)
        {
            Debug.Log("object unlocked_2");
            keyInputCount = 0;
            keyCode1 = KeyCode.None;
            keyCode2 = KeyCode.None;

            ShowMissionSceneEvent("CatKeyring");
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
    }

    public void AwakeEvent(string type)
    {
        if (type == "Start")
        {
            theCanvasManager.FadeImageEvent();
            thePlayerManager.LookFront();
            thePlayerManager.ControlMove(false);
            //thePlayerManager.SetRotate(true); // Set Player Roataion false
        }   
    }

    public void FirstAppearEvent()
    {
        StartCoroutine("FlashEventCoroutine");
    }
    
    IEnumerator FlashEventCoroutine()
    {
        yield return new WaitForSeconds(Event_List[0].progressDuration[0]);

        e_CatMonster.transform.position = Event_List[0].startPos[0].position;
        SoundManager.instance.PlayBGM("Chase");
        e_CatMonster.SetActive(true);

        yield return new WaitUntil(() =>  thePlayerManager.isHiding);

        SoundManager.instance.StopBGM();
        e_CatMonster.SetActive(false);

        thePlayerManager.outDead = true;
        
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(1.2f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(1.2f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(1.2f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySE("MonsterFootSteps");
        yield return new WaitForSeconds(1f);
        
        thePlayerManager.outDead = false;
        
        yield return new WaitUntil(() => !thePlayerManager.isHiding);
        
        SoundManager.instance.PlayBGM("Wind");
        
        GameManager.instance.StartMonsterSpawn();

        //theCanvasManager.ShowCheckpointUI();
        blockCollider[1].SetActive(false);
        
        GameManager.instance.SetSubtitle("내가 방금 뭘 본 거지..?");
        GameManager.instance.NextSubtitle("헛것을 본 건가?");
        GameManager.instance.NextSubtitle("어서 나갈 방법을 찾아보자");
            
        yield return new WaitForSeconds(7f);
        
        theCanvasManager.ShowMissionUI("나갈 단서 찾기");
        SoundManager.instance.PlaySE("Notification");
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

        theCanvasManager.ShowMissionUI("New Mission");
    }
    
    public void ShowMissionSceneEvent(string name)
    {
        showEvent.gameObject.SetActive(true);
        isKeyring = false;

        switch (name)
        {
            case "LeverKeyring":
                thePlayerManager.gameObject.SetActive(false);
                leverKeyObj.SetActive(false);

                showEvent.SetTrigger("ShowLever");
                StartCoroutine(ShowEventCoroutine(14.3f));
                thePlayerManager.canLeverMission = true;
                break;
            case "CatKeyring":
                thePlayerManager.gameObject.SetActive(false);
                catKeyObj.SetActive(false);
                break;
        }
    }

    public IEnumerator PlaySEByTime(float time, string name, string[] text)
    {
        yield return new WaitForSeconds(time);
        
        SoundManager.instance.PlaySE(name);

        yield return new WaitForSeconds(3f);
        
        GameManager.instance.SetSubtitle(text[0]);
        GameManager.instance.NextSubtitle(text[1]);

        yield return new WaitForSeconds(3f);
        
        theCanvasManager.ShowMissionUI("손전등 얻기");
        SoundManager.instance.PlaySE("Notification");
    }

    IEnumerator ShowEventCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        isKeyring = true;

        keyCode1 = KeyCode.F;
        leverKeyObj.SetActive(true);
        showEvent.gameObject.SetActive(false);
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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GameManager.instance.theSceneManager.MoveToLobbyScene();
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(Event_List[1].progressDuration[0]);
        
        DeathEvent(false);
    }
}
