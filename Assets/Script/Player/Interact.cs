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

    private RaycastHit hit = new RaycastHit();
    private Ray ray;

    private ICommand commandValue;

    [Header("Check")]
    public bool canInteract = false;

    [Header("Scripts")]
    public CanvasManager theCanvasManager;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if(Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                obj_CodeName = hit.transform.gameObject.GetComponent<InteractableObject>().codeName;

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
            theCanvasManager.SetInteractObject(false);
            theCanvasManager.ResetInteractValue();
            canInteract = false;
        }
    }

    public void InteractSucceed()
    {
        Debug.Log("Interact Complete");

        commandValue = new GetFlashCommand(new FlashBox());
        Action action = new Action(commandValue);

        action.operate();
    }
}
