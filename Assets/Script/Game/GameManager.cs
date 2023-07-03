using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private MonsterSpawner theMonsterSpawner;
    private EventManager theEventManager;

    private void Awake()
    {
        instance = this;

        AssignScript();
    }

    private void AssignScript()
    {
        theMonsterSpawner = GetComponentInChildren<MonsterSpawner>();
        theEventManager = GetComponentInChildren<EventManager>();
    }

    public void StartMonsterSpawn()
    {
        theMonsterSpawner.SpawnMonster();
    }
}
