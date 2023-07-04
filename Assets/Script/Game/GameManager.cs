using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
