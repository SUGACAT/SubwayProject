using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class SubtitleManager : MonoBehaviour
{
    public GameObject subtitle_obj;
    public TMPro.TextMeshProUGUI subtitle_txt;

    public bool doingConversation;

    private void Update()
    {
        if (!doingConversation) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void ShowSubtitle(string contents)
    {
        StartCoroutine(ShowSubtitleCoroutine(contents));
    }

    IEnumerator ShowSubtitleCoroutine(string contents)
    {
        subtitle_txt.text = contents;
        subtitle_obj.SetActive(true);
        doingConversation = true;

        yield return new WaitForSeconds(2.6f);
        
        subtitle_obj.SetActive(false);
        doingConversation = false;
    }

    public void ShowNextSubtitle(string contents)
    {
        StartCoroutine(WaitTillNextSubtitle(contents));
    }
    
    IEnumerator WaitTillNextSubtitle(string contents)
    {
        yield return new WaitUntil(() => !doingConversation);
        
        ShowSubtitle(contents);
    }
}
