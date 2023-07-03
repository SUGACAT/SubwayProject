using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CatMove : MonoBehaviour
{
    [SerializeField] bool debugMode = false;
    [Range(0f, 360f)] [SerializeField] float viewAngle = 0f;
    [SerializeField] float viewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask obstacleMask;

    public List<Collider> hitTargetList = new List<Collider>();
    
    private NavMeshAgent _agent;
    private Animator anim;
    
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerManager>().gameObject;

        _agent = GetComponent<NavMeshAgent>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(target.transform.position);
        
        if((transform.position - target.transform.position).magnitude >= 7f)
        {
            anim.SetBool("isWalk", true);
            _agent.speed = 1.3f;
        }
        else
        {
            anim.SetBool("isWalk", false);
            _agent.speed = 3.15f;
        }
    }

    private void OnDrawGizmos()
    {
        if (!debugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, viewRadius);
        
        float lookingAngle = transform.eulerAngles.y;
        
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPos, rightDir * viewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * viewRadius, Color.blue);
        Debug.DrawRay(myPos, lookDir * viewRadius, Color.cyan);
    }
    
    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    
}
