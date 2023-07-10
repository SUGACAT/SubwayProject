using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public bool leverMissionOver;
    public int currentLeverCount;

    public GameObject light_2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverMissionComplete()
    {
        currentLeverCount++;
        SoundManager.instance.PlaySE("MissionClear");

        if(currentLeverCount >= 4)
        {
            leverMissionOver = true;
            Debug.Log("Mission Complete");

            light_2f.SetActive(true);
        }
    }
}
