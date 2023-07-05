using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RatMove : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] GameObject target;

    [Header("Prefabs")]
    [SerializeField] GameObject targetObj;

    public float maxDistance;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = Instantiate(targetObj, transform.position, Quaternion.identity);

        Roaming();
    }

    void Update()
    {
       _agent.SetDestination(target.transform.position);
    }
    
    void Roaming()
    {
        target.transform.position = Random.insideUnitSphere * maxDistance;
        target.transform.position = new Vector3(target.transform.position.x ,2.738f, target.transform.position.z);
    }
}
