using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICommand
{
    public abstract void execute();
}

public class Action
{
    private ICommand theCommand;

    public Action(ICommand theCommand)
    {
        this.theCommand = theCommand;
    }

    public void operate()
    {
        theCommand.execute();
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

    public Levers currentLever;

    [Header("Check")]
    public bool canInteract = false;

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

                theCanvasManager.SetInteractObject(true);
                canInteract = true;
            }
            else if (hit.transform.CompareTag("Lever"))
            {
                currentLever = hit.transform.gameObject.GetComponentInParent<Levers>();
                if (currentLever.leverOn || currentLever.isWait) return;

                currentLever.canPull = true;
                currentLever.ShowLeverImage(true);

                theCanvasManager.SetLeverInteractObject(true);
                canInteract = true;
            }
            else if(hit.transform.CompareTag("Door"))
            {
                var doorComponent = hit.transform.gameObject.GetComponent<Door>();

                doorComponent.DivisionType();
            }
            else
            {
                if (!thePlayerManager.isHiding)
                {
                    theCanvasManager.SetInteractObject(false);
                    theCanvasManager.SetLeverInteractObject(false);
                    theCanvasManager.ResetInteractValue();
                    if (currentLever != null)
                    {
                        currentLever.canPull = false;
                        currentLever.ShowLeverImage(false);
                    }
                    canInteract = false;
                }
            }
        }
        else
        {
            if (!thePlayerManager.isHiding)
            {
                theCanvasManager.SetInteractObject(false);
                theCanvasManager.SetLeverInteractObject(false);
                theCanvasManager.ResetInteractValue();
                if (currentLever != null)
                {
                    currentLever.canPull = false;
                    currentLever.ShowLeverImage(false);
                }
                canInteract = false;
            }
        }
    }

    public void InteractSucceed()
    {
        canInteract = false;

        Debug.Log("Interact Complete");

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
            case "Lever":
                break;
                
        }
        Action action = new Action(commandValue);
        action.operate();
    }

    public void StartLeverInput()
    {

    }

    public void EndLeverInput()
    {

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
        }
        else
        {
            interactedObject.SetChildObjects(true);
            thePlayerManager.Hide("Out", interactedObject.OutPos().transform.position, ref canInteract);
            interactedObject.interacted = false;
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
