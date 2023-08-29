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
    Vector3 myPos;

    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;

    public List<Collider> hitTargetList = new List<Collider>();

    PlayerManager thePlayerManager;

    public float soundValue;

    private void Awake()
    {
        thePlayerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundValue <= 0) soundValue = 0;

        myPos = transform.position + Vector3.up * 0.5f;

        float lookingAngle = transform.eulerAngles.y;

        Vector3 lookDir = AngleToDir(lookingAngle);

        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, viewRadius, TargetMask);

        if (Targets.Length == 0) return;
        Debug.Log("Wa");

        foreach (Collider EnemyColli in Targets)
        {
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            hitTargetList.Add(EnemyColli);
            if (debugMode) Debug.DrawLine(myPos, targetPos, Color.red);
        }

        if (hitTargetList.Count == 0) return;

        SpreadSound();
    }

    private void OnDrawGizmos()
    {
        if (!debugMode) return;

        myPos = transform.position;

        Gizmos.DrawWireSphere(myPos, viewRadius);
    }

    public void SpreadSoundByObject()
    {
        if (hitTargetList.Count != 0)
        {
            for (int i = 0; i < hitTargetList.Count; i++)
            {
                hitTargetList[i].GetComponent<RatMove>().HearSound();
            }
        }
    }

    public void SpreadSound()
    {
        if (thePlayerManager.isRunning)
        {
            Debug.Log("A");

            soundValue += Time.deltaTime;

            if (soundValue >= 2f)
            {
                for (int i = 0; i < hitTargetList.Count; i++)
                {
                    hitTargetList[i].GetComponent<RatMove>().HearSound();
                }
            }
        }
        else
        {
            soundValue -= Time.deltaTime;
            Debug.Log("B");
        }
    }

    public void Footsteps1()
    {
        SoundManager.instance.PlaySE("Footsteps1");
    }

    public void Footsteps2()
    {
        int randomValue = Random.Range(1, 3);

        Debug.Log(randomValue);
        
        if (randomValue == 1)
            SoundManager.instance.PlaySE("Footsteps2");
        else
            SoundManager.instance.PlaySE("Footsteps3");
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    public void MakeNoise()
    {
        Debug.Log("NoiseStart");

        var obj = GameManager.instance.theMonsterSpawner.catMonster_Obj[0].GetComponent<CatMove>();
        obj.FindPlayerByRat();
    }
}
