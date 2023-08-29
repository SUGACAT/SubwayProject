using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour
{
    [SerializeField] GameObject light_obj;
    public GameObject defaultLight;

    public float currentBattery;
    [SerializeField] float maxBattery;
    [SerializeField] float addAmount;

    [SerializeField] float decreaseSpeed;

    [SerializeField] UnityEngine.UI.Image batteryBar_img;
    [SerializeField] TMPro.TextMeshProUGUI battery_txt;

    [SerializeField] bool batteryMax = false;
    [SerializeField] bool isOn = true;


    [Header("Scripts")]
    private PlayerManager thePlayerManager;

    private void Awake()
    {
        thePlayerManager = GetComponentInParent<PlayerManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isOn)
            {
                SetLight(false);
                defaultLight.SetActive(true);
                SoundManager.instance.PlaySE("FlashOff");
            }
            else
            {
                SetLight(true);
                defaultLight.SetActive(false);
                SoundManager.instance.PlaySE("FlashOn");
            }
        }

        if (!isOn || thePlayerManager.isWaiting) return;

        batteryBar_img.fillAmount = currentBattery / 100;
        battery_txt.text = currentBattery.ToString("N0") + "%";

        DecreaseBattery();
    }

    public void SetLight(bool type)
    {
        light_obj.SetActive(type);
        isOn = type;
    }

    public void DecreaseBattery()
    {
        if (currentBattery >= maxBattery)
            currentBattery = maxBattery;
        else if (currentBattery <= 0)
        {
            currentBattery = 0;
            SetLight(false);
            defaultLight.SetActive(true);
            return;
        }
        else
        {
            defaultLight.SetActive(false);
        }

        currentBattery -= Time.deltaTime * decreaseSpeed;
    }

    public void ChangeFlashLight()
    {
        currentBattery = maxBattery;
    }

    public void AddBattery()
    {
        currentBattery += (maxBattery / 2);
    }
}
