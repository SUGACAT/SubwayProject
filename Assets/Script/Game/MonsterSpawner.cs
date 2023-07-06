using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject catMonster_Obj, ratMonster_Obj;

    public Transform[] catSpawnPos;
    public Transform[] ratSpawnPos_B1f;
    public Transform[] ratSpawnPos_B2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMonster(int floor)
    {
        switch (floor)
        {
            case 1:
                for (int i = 0; i < ratSpawnPos_B1f.Length; i++)
                {
                    Instantiate(ratMonster_Obj, ratSpawnPos_B1f[i].position, Quaternion.identity);
                }
                break;
            case 2:
                for (int i = 0; i < ratSpawnPos_B2f.Length; i++)
                {
                    Instantiate(ratMonster_Obj, ratSpawnPos_B2f[i].position, Quaternion.identity);
                }
                break;
        }

        Instantiate(catMonster_Obj, catSpawnPos[floor - 1].position, Quaternion.identity);
    }
}
