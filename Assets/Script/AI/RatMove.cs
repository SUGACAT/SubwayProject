using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RatMove : MonoBehaviour
{
    
    
    private NavMeshAgent _agent;

    private Vector3 target;
    private float maxDistance;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {

    }

    void Update()
    {
       _agent.SetDestination(target);
    }
    
    //void Roaming()
    //{
    //    target = Random.insideUnitSphere(maxDistance);
    //}
}
