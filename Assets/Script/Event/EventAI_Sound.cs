using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAI_Sound : MonoBehaviour
{
    public AudioSource Audio1;
    public AudioSource Audio2;

    public bool sound1PlayCheck = true;

    public void WakSound()
    {
        if (sound1PlayCheck)
        {
            Audio1.Play();

            sound1PlayCheck = false;
        }
        else
        {
            Audio2.Play();

            sound1PlayCheck = true;
        }
    }
}
