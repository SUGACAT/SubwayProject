using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint
{
    public string name;
    public int missionNumber;
    public Vector3 point;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Values")]
    public SpawnPoint[] spawnList;
    
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

    public void StartMonsterSpawn()
    {
        theMonsterSpawner.SpawnMonster(1);
    }
}
