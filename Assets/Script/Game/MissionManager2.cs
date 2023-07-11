using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager2 : MonoBehaviour
{
    public bool leverMissionOver;
    public int currentLeverCount;

    public GameObject light_2f;

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
