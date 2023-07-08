using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CatMove : MonoBehaviour
{
    [Header("Sight")]
    [SerializeField] bool debugMode = false;
    [FormerlySerializedAs("ViewAngle")] [Range(0f, 360f)] [SerializeField] float viewAngle = 0f;
    [FormerlySerializedAs("ViewRadius")] [SerializeField] float viewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask obstacleMask;

    public List<Collider> hitTargetList = new List<Collider>();
    
    [Header("AI")]
    private NavMeshAgent _agent;
    public Transform target;

    public int beforeRoamNumber;
    
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        Roaming();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        _agent.SetDestination(target.position);
        
        /*if((transform.position - target.transform.position).magnitude >= 7f)
        {
            anim.SetBool("isWalk", true);
            _agent.speed = 1.3f;
        }
        else
        {
            anim.SetBool("isWalk", false);
            _agent.speed = 3.15f;
        } */
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

        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, viewRadius, TargetMask);

        if (Targets.Length == 0) return;

        foreach (Collider EnemyColli in Targets)
        {
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, viewRadius, obstacleMask))
            {
                hitTargetList.Add(EnemyColli);
                if (debugMode) Debug.DrawLine(myPos, targetPos, Color.red);
            }
        }
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    public void Roaming()
    {
        int roamNumber = Random.Range(0, GameManager.instance.theCatManager.B2_roamingPos.Length);

        while (roamNumber == beforeRoamNumber)
            roamNumber = Random.Range(0, GameManager.instance.theCatManager.B2_roamingPos.Length);

        beforeRoamNumber = roamNumber;
        target = GameManager.instance.theCatManager.B2_roamingPos[roamNumber];
    }

    
}
