using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager2 : MonoBehaviour
{
    public bool leverMissionOver;
    public int currentLeverCount;

    public Animator ironfence;
    public GameObject light_2f;

    public void LeverMissionComplete()
    {
        currentLeverCount++;
        SoundManager.instance.PlaySE("MissionClear");

        if(currentLeverCount >= 4)
        {
            leverMissionOver = true;
            Debug.Log("Mission Complete");
            
            ironfence.SetTrigger("Up");
            ironfence.gameObject.SetActive(true);
            GameManager.instance.thePlayerManager.gameObject.SetActive(false);
            GameManager.instance.theCanvasManager.ShowCCTVUI(true);
            
            light_2f.SetActive(true);

            StartCoroutine(CCTVCoroutine());
        }
    }

    IEnumerator CCTVCoroutine()
    {
        yield return new WaitForSeconds(16.54f);
        
        ironfence.gameObject.SetActive(false);
        GameManager.instance.thePlayerManager.gameObject.SetActive(true);
        GameManager.instance.theCanvasManager.ShowCCTVUI(false);

    }
}
