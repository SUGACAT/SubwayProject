using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SpawnPoint
{
    public string name;
    public Vector3 point;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Values")]
    public SpawnPoint[] spawnList;

    public int eventNumber = 0;
    
    [Header("Scripts")]
    private MonsterSpawner theMonsterSpawner;
    private EventManager theEventManager;
    [HideInInspector] public CatManager theCatManager;

    private void Awake()
    {
        instance = this;

        AssignScript();
    }

    private void AssignScript()
    {
        theMonsterSpawner = GetComponentInChildren<MonsterSpawner>();
        theEventManager = GetComponentInChildren<EventManager>();
        theCatManager = GetComponentInChildren<CatManager>();
    }

    public void StartMonsterSpawn(int floor)
    {
        theMonsterSpawner.SpawnMonster(floor);
    }

    public Vector3 SpawnPos()
    {
        return spawnList[eventNumber].point;
    }
}
