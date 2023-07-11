using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("Value")]
    public float interactValue = 0;

    public Image key1Image;
    public Image key2Image;
    public Animator keyPushAnimator;

    public Animator missionCompleteAnimator;
    public TextMeshProUGUI missionComplete_txt;

    public Animator checkpointText;

    [Header("Prefabs")]
    public Image background_Img;
    public Image interact_Img;
    public Image staminaBar_Img;
    public GameObject hideSight_Obj;
    public GameObject death_Obj;
    public GameObject allUI, eventUI, cctvUI;

    [Header("Objects")]
    public GameObject interact_Obj;
    public GameObject leverInteract_Obj;

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

    public void SetLeverInteractObject(bool value)
    {
        leverInteract_Obj.SetActive(value);
    }

    public void ShowCCTVUI(bool value)
    {
        cctvUI.SetActive(value);
        allUI.SetActive(!value);
    }

    public void ShowMissionUI(string txt)
    {
        missionCompleteAnimator.SetTrigger("Complete");
        missionComplete_txt.text = txt;
    }

    public void ShowCheckpointUI()
    {
        checkpointText.SetTrigger("Checkpoint");
    }

    public void SetLeverInteractImageColor(int number)
    {
        if (number == 2)
        {
            key1Image.color = new Color32(125, 0, 0, 210);
            key2Image.color = new Color32(51, 51, 51, 210);

            keyPushAnimator.SetBool("Lever1Click", true);
        }
        else
        {
            key1Image.color = new Color32(51, 51, 51, 210);
            key2Image.color = new Color32(125, 0, 0, 210);

            keyPushAnimator.SetBool("Lever1Click", false);
        }
    }

    public void SetInteractValue()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (theInteract.canInteract)
            {
                interactValue += (Time.deltaTime / 1.6f);

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

    public void HideUI(bool type)
    {
        allUI.SetActive(!type);
        eventUI.SetActive(type);
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
