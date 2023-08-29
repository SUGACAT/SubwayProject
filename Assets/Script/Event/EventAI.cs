using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EventAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject target;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(target.transform.position);

        anim.SetBool("isWalk", false);
        _agent.speed = 3.2f;

        /* if ((transform.position - target.transform.position).magnitude < 7)
         {
             Debug.Log("Run!");
             anim.SetBool("isWalk", false);
             _agent.speed = 3.15f;
         }
         else
         {
             anim.SetBool("isWalk", true);
             _agent.speed = 1.4f;
         }
      */
    }
}
