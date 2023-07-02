using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private MonsterSpawner theMonsterSpawner;
    private EventManager theEventManager;

    private void Awake()
    {
        gameManager = this;

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
