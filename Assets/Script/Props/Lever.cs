using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Key
{
    Q, E
}

public class Lever : MonoBehaviour
{
    public GameObject lever_obj;

    public Image fill_img;
    public GameObject succeed_obj;
    public GameObject fail_obj;
    public GameObject progress_obj;

    public float interactValue;

    public int targetValue;

    public bool canPull = false;
    public bool leverOn = false;

    public bool canKey1 = true;
    public bool canKey2 = false;

    public bool isWait = false;

    public Key theKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPull || leverOn  || isWait) return;

        InputKey();
        fill_img.fillAmount = interactValue / 100;
        Debug.Log(interactValue / 100);

        if (interactValue >= targetValue)
        {
            LeverOn();
        }
    }

    public void ShowLeverImage(bool type)
    {
        progress_obj.SetActive(type);
    }

    public void ShowSuccessImage(bool type)
    {
        progress_obj.SetActive(!type);
        succeed_obj.SetActive(type);
    }

    public void ShowFailImage(bool type)
    {
        progress_obj.SetActive(!type);
        fail_obj.SetActive(type);
    }

    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!canKey1)
            {
                ResetValue();
                return;
            }

            canKey1 = false;
            canKey2 = true;

            GameManager.instance.theCanvasManager.SetLeverInteractImageColor(1);

            lever_obj.transform.localRotation = Quaternion.Euler(new Vector3(0,0, -interactValue));

            interactValue++;
            Debug.Log("Key1Down");
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            if (!canKey2)
            {
                ResetValue();
                return;
            }

            canKey1 = true;
            canKey2 = false;

            GameManager.instance.theCanvasManager.SetLeverInteractImageColor(2);
            lever_obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -interactValue));

            interactValue++;
            Debug.Log("Key2Down");
        }
    }

    public void ResetValue()
    {
        isWait = true;
        ShowFailImage(true);
        ShowLeverImage(false);

        lever_obj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        StartCoroutine(ResetCoolTimeCoroutine());
    }

    IEnumerator ResetCoolTimeCoroutine()
    {
        yield return new WaitForSeconds(5f);
        interactValue = 0;
        isWait = false;
        ShowFailImage(false);
        ShowLeverImage(true);
    }

    public void LeverOn()
    {
        Debug.Log("Lever On");
        GameManager.instance.theMissionManager.LeverMissionComplete();
        ShowSuccessImage(true);

        if (GameManager.instance.theMissionManager.leverMissionOver)
            GameManager.instance.theCanvasManager.ShowMissionUI($"Lever Complete");
        else
            GameManager.instance.theCanvasManager.ShowMissionUI($"Lever {GameManager.instance.theMissionManager.currentLeverCount} / 4");

        leverOn = true;
    }
}
