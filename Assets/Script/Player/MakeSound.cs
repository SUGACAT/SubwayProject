using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{
    [Header("Sight")] [SerializeField] bool debugMode = false;

    [Range(0f, 360f)]
    [SerializeField]
    float viewAngle = 0f;

    [SerializeField]
    float viewRadius = 1f;

    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;

    public List<Collider> hitTargetList = new List<Collider>();

    // Update is called once per frame
    void Update()
    {
        if (hitTargetList.Count == 0) return;
    }

    private void OnDrawGizmos()
    {
        if (!debugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, viewRadius);

        float lookingAngle = transform.eulerAngles.y;

        Vector3 lookDir = AngleToDir(lookingAngle);

        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, viewRadius, TargetMask);

        if (Targets.Length == 0) return;

        foreach (Collider EnemyColli in Targets)
        {
            Debug.Log("AAAAAAA");

            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, viewRadius, ObstacleMask))
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

    public void MakeNoise()
    {
        Debug.Log("NoiseStart");

        for (int i = 0; i < hitTargetList.Count; i++)
        {
            hitTargetList[i].GetComponent<RatMove>();
            Debug.Log("Noise");
        }
    }
}
