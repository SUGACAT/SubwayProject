using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    [Header("Value")]
    public float interactValue = 0;

    [Header("Prefabs")]
    public Image backgroundImage;
    public Image interact_img;

    [Header("Objects")]
    public GameObject interact_Obj;

    [Header("Scripts")]
    public EventManager theEventManager;
    public Interact theInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetInteractValue();
    }

    public void FadeImageEvent()
    {
        StartCoroutine("FadeImageCoroutine");
    }

    IEnumerator FadeImageCoroutine()
    {
        yield return new WaitForSeconds(1f);

        backgroundImage.DOColor(Color.white, 4);

        yield return new WaitForSeconds(4.2f);

        Destroy(backgroundImage);
        theEventManager.GameStartEventOver();
    }

    public void SetInteractObject(bool value)
    {
        interact_Obj.SetActive(value);
    }

    public void SetInteractValue()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (theInteract)
            {
                interactValue += (Time.deltaTime / 2);

                interact_img.fillAmount = interactValue;

                if(interactValue >= 1)
                {
                    ResetInteractValue();
                    theInteract.InteractSucceed();
                }
            }
        }
    }

    public void ResetInteractValue()
    {
        interactValue = 0;
        interact_img.fillAmount = interactValue;
    }
}
