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

  public bool isFloor1;

  [Header("Scripts")]
  [HideInInspector] public MonsterSpawner theMonsterSpawner;
  [HideInInspector] public EventManager theEventManager;
  [HideInInspector] public CatManager theCatManager;
  public MoveSceneManager theSceneManager;
  public PlayerManager thePlayerManager;
  public CanvasManager theCanvasManager;
  public MissionManager2 theMissionManager;
  private SubtitleManager theSubtitleMaanger;

  private void Awake()
  {
	instance = this;
	Application.targetFrameRate = 60;

	AssignScript();
	theSubtitleMaanger = GetComponent<SubtitleManager>();
  }

  private void AssignScript()
  {
	theMonsterSpawner = GetComponentInChildren<MonsterSpawner>();
	theEventManager = GetComponentInChildren<EventManager>();
	theCatManager = GetComponentInChildren<CatManager>();
  }

  public void StartMonsterSpawn()
  {
	theMonsterSpawner.SpawnMonster();
  }

  public Vector3 SpawnPos()
  {
	return spawnList[eventNumber].point;
  }

  public void SetSubtitle(string contents) => theSubtitleMaanger.ShowSubtitle(contents);
  public void NextSubtitle(string contents) => theSubtitleMaanger.ShowNextSubtitle(contents);

  public GameObject PlayerObject { get { return thePlayerManager.gameObject; } }
}
