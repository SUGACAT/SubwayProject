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
    [SerializeField] float maxDistance;

    private NavMeshAgent _agent;
    private Animator anim;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        target = Instantiate(targetObj, transform.position, Quaternion.identity);

        StartCoroutine(Roaming());
    }

    void Update()
    {
        _agent.SetDestination(target.transform.position);
    }

    IEnumerator Roaming()
    {
        anim.SetBool("isIdle", false);
        target.transform.position = transform.position - Random.insideUnitSphere * maxDistance;
        target.transform.position = new Vector3(target.transform.position.x ,this.transform.position.y, target.transform.position.z);

        int randomTime = UnityEngine.Random.Range(2, 6);
        
        yield return new WaitForSeconds(randomTime);

        StartCoroutine(Roaming());
    }
}
