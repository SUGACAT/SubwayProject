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
    
    [Header("Check")]
    public bool canInteract = false;

    [Header("Scripts")]
    public CanvasManager theCanvasManager;
    private PlayerManager thePlayerManager;

    // Start is called before the first frame update
    void Awake()
    {
        thePlayerManager = GetComponent<PlayerManager>();
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
            else
            {
                theCanvasManager.SetInteractObject(false);
                theCanvasManager.ResetInteractValue();
                canInteract = false;
            }
        }
        else
        {
            if (!thePlayerManager.isHiding)
            {
                theCanvasManager.SetInteractObject(false);
                theCanvasManager.ResetInteractValue();
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
        }
        Action action = new Action(commandValue);
        action.operate();
    }

    public void Command_Flash()
    {
        thePlayerManager.GetFlash();
        thePlayerManager.PlayEvent(1);
        
        commandValue = new GetFlashCommand(new FlashBox());
        interactedObject.interacted = true;
    }

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
}
