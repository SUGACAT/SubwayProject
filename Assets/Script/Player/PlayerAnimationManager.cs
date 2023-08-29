using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator anim;

    public bool can1 = true;

    public bool stateChanged1 = false;
    public bool stateChanged2 = false;
    public bool stateChanged3 = false;
    public bool stateChanged4 = false;

    public AudioSource AudioSource1;
    public AudioSource AudioSource2;

    public AudioClip[] footClip;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Idle()
    {
        anim.SetInteger("MoveState", 0);

        StopAllCoroutines();

        stateChanged1 = false;
        stateChanged2 = false;
        stateChanged3 = false;
        stateChanged4 = false;
    }

    public void Walk()
    {
        if (stateChanged2) return;

        anim.SetInteger("MoveState", 1);

        StopAllCoroutines();
        StartCoroutine(PlaySoundCoroutine(0.56f, false));

        stateChanged1 = false;
        stateChanged2 = true;
        stateChanged3 = false;
        stateChanged4 = false;
    }

    public void Crouch()
    {
        StopAllCoroutines();
        anim.SetInteger("MoveState", 2);
    }

    public void Run()
    {
        Debug.Log("Q");

        if (stateChanged3) return;
        anim.SetInteger("MoveState", 3);

        Debug.Log("E");

        StopAllCoroutines();
        StartCoroutine(PlaySoundCoroutine(0.38f, false));

        stateChanged1 = false;
        stateChanged2 = false;
        stateChanged3 = true;
        stateChanged4 = false;
    }


    IEnumerator PlaySoundCoroutine(float duration, bool type)
    {
        if (!type)
        {
            WalkSound();

            yield return new WaitForSeconds(duration);

            WalkSound();

            StartCoroutine(PlaySoundCoroutine(duration, false));
        }
        else
        {
            yield return null;
        }
    }

    public void WalkSound()
    {
        if (can1)
        {
            WalkSound1();
            can1 = false;
        }
        else
        {
            WalkSound2();
            can1 = true;
        }
    }

    public void WalkSound1()
    {
        AudioSource1.Play();
    }

    public void WalkSound2()
    {
        int random = Random.Range(0, 2);

        AudioSource2.clip = footClip[random];
        AudioSource2.Play();
    }
}
