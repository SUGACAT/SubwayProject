using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] catMonster_Obj;
    public GameObject ratMonster_Obj;

    public Transform[] catSpawnPos;
    public Transform[] ratSpawnPos_B1f;
    public Transform[] ratSpawnPos_B2f;

    public GameObject catMonsterB1;
    public GameObject catMonsterB2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnMonster();
        }
    }

    public void SpawnMonster()
    {
        for (int i = 0; i < ratSpawnPos_B1f.Length; i++)
        {
            Instantiate(ratMonster_Obj, ratSpawnPos_B1f[i].position, Quaternion.identity);
        }
        for (int i = 0; i < ratSpawnPos_B2f.Length; i++)
        {
            Instantiate(ratMonster_Obj, ratSpawnPos_B2f[i].position, Quaternion.identity);
        }

        catMonsterB1 = Instantiate(catMonster_Obj[0], catSpawnPos[0].position, Quaternion.identity);
        catMonsterB2 = Instantiate(catMonster_Obj[1], catSpawnPos[1].position, Quaternion.identity);
    }
}
