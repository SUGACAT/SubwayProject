using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CatMove : MonoBehaviour
{
  [Header("Sight")] [SerializeField] bool debugMode = false; // 활성화하면 플레이어의 시야 반경과 각도가 보임

  public bool isB1; // 지하에 스폰되는 고양이일 경우 체크

  [Range(0f, 360f)]
  [SerializeField]
  float viewAngle = 0f; // 플레이어의 시야 각도

  [SerializeField]
  float viewRadius = 1f; // 플레이어의 주변 탐지 반경(반지름)

  Vector3 myPos; // 플레이어의 위치 저장
  
  Vector3 lookDir; // ** 

  [SerializeField] LayerMask TargetMask; // AI가 쫒아갈 목표물의 마스크
  [SerializeField] LayerMask obstacleMask; // 장애물 마스크

  public List<Collider> hitTargetList = new List<Collider>(); // AI 주변으로 탐지되는 물체들의 리스트

  [Header("AI")] private NavMeshAgent _agent;
  public Transform target; // 현재 고양이의 목표 지점

  public int beforeRoamNumber; // 직전에 간 로밍포인트 번호 저장
  public bool isRoaming; // 로밍중일때 체크

  public bool playerFind; // 시야로 플레이어 발견
  public bool foundByRat; // 쥐에 의해서 플레이어 발견

  public float foundTime; // 플레이어를 발견한 시점부터 시간 기록
  public bool wait; // 플레이어가 숨고 있을때 공격 대기

  private Animator anim;

  // Start is called before the first frame update
  void Start()
  {
	_agent = GetComponent<NavMeshAgent>();
	anim = GetComponentInChildren<Animator>();

	Roaming(); //몬스터 스폰시 이동할 위치를 정하는 함수 호출
  }

  // Update is called once per frame
  void Update()
  {
	if (GameManager.instance.thePlayerManager.isHiding && wait == false) //플레이어가 현재 숨고있고, 공격 대기가 false일때
	{
	  wait = true; // 공격 대기를 true로 전환
	  Roaming(); // 로밍 포인트 재설정

	  StartCoroutine(WaitPlayerOutCoroutine()); //플레이어가 상자에서 나올때까지 기다리는 코루틴 호출
	}

	ControlAnim();

	RatDetact();

	NormalDetact();


	if (!FindPlayer()) // 플레이어가 발견되지 않았을 경우
	{
	  _agent.SetDestination(target.transform.position); // 로밍
	  return;
	}

	if (hitTargetList.Count == 0 && !isRoaming) //타겟이 감지되지 않았는데 로밍중이 아닐 때(멈춤현상)
	{
	  isRoaming = true; // 다시 로밍 시작
	}
  }

  private void ControlAnim() //현재 상태에 따른 애니메이션 변화
  {
	anim.SetBool("isWalk", true);
	_agent.speed = 1.4f;

	if (hitTargetList.Count != 0)
	{
	  if ((transform.position - hitTargetList[0].transform.position).magnitude >= 10f)
	  {
		anim.SetBool("isWalk", true);
		_agent.speed = 1.4f;
	  }
	  else
	  {
		anim.SetBool("isWalk", false);
		_agent.speed = 3.15f;
	  }
	}
  }

  private void RatDetact()
  {
	if (foundByRat)
	{
	  isRoaming = false;

	  _agent.SetDestination(GameManager.instance.PlayerObject.transform.position);
	  foundTime += Time.deltaTime;

	  anim.SetBool("isWalk", false);
	  _agent.speed = 3.15f;

	  if (foundTime >= 12)
	  {
		foundTime = 0;
		foundByRat = false;

		Roaming();
	  }

	  return;
	}
  }

  private void NormalDetact()
  {
	if (playerFind)
	{
	  isRoaming = false;

	  _agent.SetDestination(hitTargetList[0].transform.position);

	  if ((transform.position - hitTargetList[0].transform.position).magnitude > 12f)
	  {
		playerFind = false;
		WaitplayerCoroutine();
	  }
	  return;
	}
  }

  IEnumerator WaitplayerCoroutine()
  {
	yield return new WaitForSeconds(3f);

	if (playerFind == false)
	  Roaming();
  }

  IEnumerator WaitPlayerOutCoroutine()
  {
	yield return new WaitUntil(() => !GameManager.instance.thePlayerManager.isHiding);

	wait = false; // 플레이어가 상자에서 나오면 공격 대기 상태를 false로 전환
  }

  public void FindPlayerByRat() //쥐에 의해서 플레이어 발견
  {
	foundByRat = true;
  }

  public bool FindPlayer() // 플레이어를 탐지하는 함수
  {
	myPos = transform.position + Vector3.up * 0.5f;

	hitTargetList.Clear();
	Collider[] Targets = Physics.OverlapSphere(myPos, viewRadius, TargetMask);

	if (Targets.Length == 0) return false;

	foreach (Collider EnemyColli in Targets)
	{
	  Vector3 targetPos = EnemyColli.transform.position;
	  Vector3 targetDir = (targetPos - myPos).normalized;

	  float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;

	  Debug.DrawRay(myPos, targetDir, Color.magenta);
	  playerFind = true;

	  hitTargetList.Add(EnemyColli);
	  if (debugMode) Debug.DrawLine(myPos, targetPos, Color.red);

	  return true;
	}

	return false;
  }

  public void Roaming() // 로밍
  {
	if (isB1) // 지하 고양이일 경우
	{
	  isRoaming = true; // 현재 상태를 로밍중으로 체크
	  int roamNumber = Random.Range(0, GameManager.instance.theCatManager.B1_roamingPos.Length);

	  while (roamNumber == beforeRoamNumber)
		roamNumber = Random.Range(0, GameManager.instance.theCatManager.B1_roamingPos.Length);

	  beforeRoamNumber = roamNumber;
	  target = GameManager.instance.theCatManager.B1_roamingPos[roamNumber];
	}
	else
	{
	  isRoaming = true;
	  int roamNumber = Random.Range(0, GameManager.instance.theCatManager.B2_roamingPos.Length);

	  while (roamNumber == beforeRoamNumber)
		roamNumber = Random.Range(0, GameManager.instance.theCatManager.B2_roamingPos.Length);

	  beforeRoamNumber = roamNumber;
	  target = GameManager.instance.theCatManager.B2_roamingPos[roamNumber];
	}
  }

  Vector3 AngleToDir(float angle)
  {
	float radian = angle * Mathf.Deg2Rad;
	return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
  }

  private void OnDrawGizmos() // 씬에서 시야각을 시각적으로 보여줌
  {
	if (!debugMode) return;

	Gizmos.DrawWireSphere(myPos, viewRadius);

	float lookingAngle = transform.eulerAngles.y;

	Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
	Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);
	lookDir = AngleToDir(lookingAngle);

	Debug.DrawRay(myPos, rightDir * viewRadius, Color.blue);
	Debug.DrawRay(myPos, leftDir * viewRadius, Color.blue);
	Debug.DrawRay(myPos, lookDir * viewRadius, Color.cyan);
  }

}
