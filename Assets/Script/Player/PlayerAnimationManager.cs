using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Idle()
    {
        anim.SetInteger("MoveState", 0);
    }

    public void Walk()
    {
        anim.SetInteger("MoveState", 1);
    }

    public void Run()
    {

    }

    public void StartAnim()
    {

    }
}
