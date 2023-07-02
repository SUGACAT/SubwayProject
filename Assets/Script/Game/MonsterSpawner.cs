using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject catMonster_Obj, ratMonster_Obj;

    public Transform[] catSpawnPos;
    public Transform[] ratSpawnPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMonster()
    {
        for (int i = 0; i < ratSpawnPos.Length; i++)
        {
            if(i < 2) Instantiate(catMonster_Obj, catSpawnPos[i].position, Quaternion.identity);
            Instantiate(ratMonster_Obj, ratSpawnPos[i].position, Quaternion.identity);
        }
    }

    public void SpawnMonsterByPosition(Transform[] positions)
    {
        
    }
}
