using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSound : MonoBehaviour
{
    public AudioSource Audio1;
    public AudioSource Audio2;

    public bool sound1PlayCheck = true;

    CatMove theCatMove;

    private void Awake()
    {
        theCatMove = GetComponentInParent<CatMove>();
    }

    public void WakSound()
    {
        //if (theCatMove.isB1 && !GameManager.instance.isFloor1) return;
        //if (!theCatMove.isB1 && GameManager.instance.isFloor1) return;

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
