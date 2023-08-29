using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public interface ICommand
{
    public abstract void execute(Interact i);
}

public class Action
{
    private ICommand theCommand;

    public Action(ICommand theCommand)
    {
        this.theCommand = theCommand;
    }

    public void operate(Interact i)
    {
        theCommand.execute(i);
    }
}

public class Interact : MonoBehaviour
{
    [Header("Values")]
    public float interactDistance;
    public string obj_CodeName;
    private ICommand commandValue;

    private RaycastHit hit = new RaycastHit();
    private Ray ray;

    public InteractableObject interactedObject;
    public GameObject interactedNormalObject;
    
    public Levers currentLever;
    public Door theDoor;

    public bool isFirstTime;
    
    [Header("Check")]
    public bool canInteract = false;

    [FormerlySerializedAs("isDoor")] public bool isSingleInteraction = false;
    
    public bool canGetFlashlight = false;

    [Header("Scripts")]
    public CanvasManager theCanvasManager;
    public Inventory theInventory;
    private PlayerManager thePlayerManager;
    private MissionInput theMissionInput;

    // Start is called before the first frame update
    void Awake()
    {
        thePlayerManager = GetComponent<PlayerManager>();
        theMissionInput = GetComponent<MissionInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            thePlayerManager.canLeverMission = true;

        FindObjects();
    }

    public void FindObjects()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                var i_Component = hit.transform.gameObject.GetComponent<InteractableObject>();

                if (i_Component.interacted && obj_CodeName != "SandBox") { return; }

                obj_CodeName = i_Component.codeName;
                interactedObject = i_Component;

                switch (obj_CodeName)
                {
                    case "FlashContainer":
                        theCanvasManager.interactTypeText.text = "얻기";
                        break;
                    case "SandBox" :
                        theCanvasManager.interactTypeText.text = "숨기";
                        break;
                    case "Drink":
                        theCanvasManager.interactTypeText.text = "줍기";
                        break;
                    case "Chocobar":
                        theCanvasManager.interactTypeText.text = "줍기";
                        break;
                    case "Battery":
                        theCanvasManager.interactTypeText.text = "줍기";
                        break;
                    case "LeverKeyring":
                        theCanvasManager.interactTypeText.text = "보기";
                        return;
                    case "CatKeyring":
                        theCanvasManager.interactTypeText.text = "보기";
                        return;
                    case "Door":
                        theCanvasManager.interactTypeText.text = "열기";
                        return;
                }
                
                theCanvasManager.SetInteractObject(true);
                canInteract = true;
            }
            else if (hit.transform.CompareTag("Lever"))
            {
                if (!thePlayerManager.canLeverMission) return;
                currentLever = hit.transform.gameObject.GetComponentInParent<Levers>();
                if (currentLever.leverOn || currentLever.isWait) return;

                currentLever.canPull = true;
                currentLever.ShowLeverImage(true);

                theCanvasManager.SetLeverInteractObject(true);
                canInteract = true;
                isSingleInteraction = false;
            }
            else if(hit.transform.CompareTag("Door"))
            {
                theDoor = hit.transform.gameObject.GetComponentInParent<Door>();
                
                theCanvasManager.SetInteractObject(true);
                theCanvasManager.interactTypeText.text = "열기";
                obj_CodeName = "Door";

                canInteract = true;
                isSingleInteraction = true;
            }
            else if (hit.transform.CompareTag("Key"))
            {
                theCanvasManager.SetInteractObject(true);
                theCanvasManager.interactTypeText.text = "줍기";
                isSingleInteraction = true;
                canInteract = true;

                interactedNormalObject = hit.transform.gameObject;
                obj_CodeName = "Key";
            }
            else if (hit.transform.CompareTag("LockedDoor"))
            {
                canInteract = true;

                theCanvasManager.interactTypeText.text = "열기";
                
                if (thePlayerManager.currentKeyCount == 1)
                {
                    theCanvasManager.SetInteractObject(true);
                    isSingleInteraction = true;

                    interactedNormalObject = hit.transform.gameObject;
                    obj_CodeName = "RedDoor";
                }
                else if(thePlayerManager.currentKeyCount == 2) 
                {
                    theCanvasManager.SetInteractObject(true);
                    isSingleInteraction = true;

                    interactedNormalObject = hit.transform.gameObject;
                    obj_CodeName = "BlueDoor";
                }
            }
            else
            {
                if (!thePlayerManager.isHiding)
                {
                    Debug.Log("B");

                    theCanvasManager.SetInteractObject(false);
                    theCanvasManager.SetLeverInteractObject(false);
                    theCanvasManager.ResetInteractValue();
                    if (currentLever != null)
                    {
                        currentLever.canPull = false;
                        currentLever.ShowLeverImage(false);
                    }
                    canInteract = false;
                    isSingleInteraction = false;
                }
            }
        }
        else
        {
            if (!thePlayerManager.isHiding)
            {
                Debug.Log("C");

                theCanvasManager.SetInteractObject(false);
                theCanvasManager.SetLeverInteractObject(false);
                theCanvasManager.ResetInteractValue();
                if (currentLever != null)
                {
                    currentLever.canPull = false;
                    currentLever.ShowLeverImage(false);
                }
                canInteract = false;
                isSingleInteraction = false;
            }
        }
    }

    public void InteractSucceed()
    {
        canInteract = false;
        isSingleInteraction = false;

        switch (obj_CodeName)
        {
            case "FlashContainer":
                Command_Flash();
                break;
            case "SandBox" :
                Command_Sand();
                break;
            case "Drink":
                Command_Drink();
                break;
            case "Chocobar":
                Command_Chocobar();
                break;
            case "Battery":
                Command_Battery();
                break;
            case "LeverKeyring":
                thePlayerManager.ShowKeyEvent("LeverKeyring");
                return;
            case "CatKeyring":
                thePlayerManager.ShowKeyEvent("CatKeyring");
                return;
            case "Door":
                theDoor.DivisionType();
                return;
            case "Key":
                interactedNormalObject.SetActive(false);
                
                if (thePlayerManager.currentKeyCount == 0)
                    thePlayerManager.ActiveKeyUI(0);
                else
                    thePlayerManager.ActiveKeyUI(1);

                thePlayerManager.currentKeyCount++;
                
                SoundManager.instance.PlaySE("GetSound");
                return;
            case "RedDoor":
                interactedNormalObject.GetComponentInParent<Animator>().SetTrigger("Open");
                return;
        }

        Action action = new Action(commandValue);
        action.operate(this);
    }

    /// <summary>
    /// //////////////////////////////////////////////////
    /// </summary>

    public void Command_Sand()
    {
        if (!thePlayerManager.isHiding)
        {
            interactedObject.SetChildObjects(false);
            thePlayerManager.Hide("In", interactedObject.transform.position, ref canInteract);

            commandValue = new GetSandBoxCommand(new SandBox());
            interactedObject.interacted = true;
            
            SoundManager.instance.PlaySE("SandBox");

            if (isFirstTime)
            {
                GameManager.instance.theEventManager.blockCollider[0].SetActive(false);
                GameManager.instance.theEventManager.blockCollider[1].SetActive(true);
            }
        }
        else
        {
            thePlayerManager.Undying();
            
            theCanvasManager.interactTypeText.text = "";

            interactedObject.SetChildObjects(true);
            thePlayerManager.Hide("Out", interactedObject.OutPos().transform.position, ref canInteract);
            interactedObject.interacted = false;
            
            SoundManager.instance.PlaySE("SandBox");

            if(thePlayerManager.outDead) thePlayerManager.Death();
            
            if (isFirstTime)
            {
                string[] textList = new string[2];
                textList[0] = "이게 어디서 나는 소리지...?";
                textList[1] = "저기 빛이 나는 곳으로 가보자";
                
                StartCoroutine(GameManager.instance.theEventManager.PlaySEByTime(2f, "Creaking", textList));

                isFirstTime = false;
            }
                
        }
    }

    public void Command_Flash()
    {
        if (!canGetFlashlight)
        {
            thePlayerManager.GetFlash();
            thePlayerManager.theEventManager.FirstAppearEvent();

            commandValue = new GetFlashCommand(new FlashBox());
            interactedObject.interacted = true;
            canGetFlashlight = true;
        }
        else
        {
            thePlayerManager.GetNewFlash();
        }
    }

    public void Command_Drink()
    {
        commandValue = new GetDrinkCommand(new Drink());
        Destroy(interactedObject.gameObject);

        theInventory.AddItemInInventory(Item.drink);
    }

    public void Command_Chocobar()
    {                                   
        commandValue = new GetChocobarCommand(new Chocobar());
        Destroy(interactedObject.gameObject);

        theInventory.AddItemInInventory(Item.chocobar);
    }

    public void Command_Battery()
    {
        commandValue = new GetBatteryCommand(new Battery());
        Destroy(interactedObject.gameObject);

        theInventory.AddItemInInventory(Item.battery);
    }
}
