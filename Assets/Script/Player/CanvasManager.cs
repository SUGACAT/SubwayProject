using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class CanvasManager : MonoBehaviour
{
    [Header("Value")]
    public float interactValue = 0;

    [Header("Prefabs")]
    public Image background_Img;
    public Image interact_Img;
    public Image staminaBar_Img;
    public GameObject hideSight_Obj;
    public GameObject death_Obj;
    
    [Header("Objects")]
    public GameObject interact_Obj;

    [Header("Scripts")]
    public EventManager theEventManager;
    public Interact theInteract;
    public PlayerManager thePlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetInteractValue();
        SetStaminaBar();
    }

    public void FadeImageEvent()
    {
        StartCoroutine("FadeImageCoroutine");
    }

    IEnumerator FadeImageCoroutine()
    {
        yield return new WaitForSeconds(1f);

        background_Img.DOColor(Color.white, 4);

        yield return new WaitForSeconds(4.2f);

        Destroy(background_Img);
        thePlayerManager.SetRotate(false);
    }

    public void SetStaminaBar()
    {
        staminaBar_Img.fillAmount = thePlayerManager.ControlStamina() / 100;
    }
    
    public void SetInteractObject(bool value)
    {
        interact_Obj.SetActive(value);
    }

    public void SetInteractValue()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (theInteract.canInteract)
            {
                interactValue += (Time.deltaTime / 2);

                interact_Img.fillAmount = interactValue;

                if(interactValue >= 1)
                {
                    ResetInteractValue();
                    theInteract.InteractSucceed();
                }
            }
        }
        else
        {
            ResetInteractValue();
        }
    }

    public void SetHideImage(bool value)
    {
        hideSight_Obj.SetActive(value);
    }
    
    public void SetDeathImage(bool value)
    {
        death_Obj.SetActive(value);
    }

    public void ResetInteractValue()
    {
        interactValue = 0;
        interact_Img.fillAmount = interactValue;
    }
}
