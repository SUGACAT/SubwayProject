using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CatMove : MonoBehaviour
{
  [Header("Sight")] [SerializeField] bool debugMode = false; // Ȱ��ȭ�ϸ� �÷��̾��� �þ� �ݰ�� ������ ����

  public bool isB1; // ���Ͽ� �����Ǵ� ������� ��� üũ

  [Range(0f, 360f)]
  [SerializeField]
  float viewAngle = 0f; // �÷��̾��� �þ� ����

  [SerializeField]
  float viewRadius = 1f; // �÷��̾��� �ֺ� Ž�� �ݰ�(������)

  Vector3 myPos; // �÷��̾��� ��ġ ����
  
  Vector3 lookDir; // ** 

  [SerializeField] LayerMask TargetMask; // AI�� �i�ư� ��ǥ���� ����ũ
  [SerializeField] LayerMask obstacleMask; // ��ֹ� ����ũ

  public List<Collider> hitTargetList = new List<Collider>(); // AI �ֺ����� Ž���Ǵ� ��ü���� ����Ʈ

  [Header("AI")] private NavMeshAgent _agent;
  public Transform target; // ���� ������� ��ǥ ����

  public int beforeRoamNumber; // ������ �� �ι�����Ʈ ��ȣ ����
  public bool isRoaming; // �ι����϶� üũ

  public bool playerFind; // �þ߷� �÷��̾� �߰�
  public bool foundByRat; // �㿡 ���ؼ� �÷��̾� �߰�

  public float foundTime; // �÷��̾ �߰��� �������� �ð� ���
  public bool wait; // �÷��̾ ���� ������ ���� ���

  private Animator anim;

  // Start is called before the first frame update
  void Start()
  {
	_agent = GetComponent<NavMeshAgent>();
	anim = GetComponentInChildren<Animator>();

	Roaming(); //���� ������ �̵��� ��ġ�� ���ϴ� �Լ� ȣ��
  }

  // Update is called once per frame
  void Update()
  {
	if (GameManager.instance.thePlayerManager.isHiding && wait == false) //�÷��̾ ���� �����ְ�, ���� ��Ⱑ false�϶�
	{
	  wait = true; // ���� ��⸦ true�� ��ȯ
	  Roaming(); // �ι� ����Ʈ �缳��

	  StartCoroutine(WaitPlayerOutCoroutine()); //�÷��̾ ���ڿ��� ���ö����� ��ٸ��� �ڷ�ƾ ȣ��
	}

	ControlAnim();

	RatDetact();

	NormalDetact();


	if (!FindPlayer()) // �÷��̾ �߰ߵ��� �ʾ��� ���
	{
	  _agent.SetDestination(target.transform.position); // �ι�
	  return;
	}

	if (hitTargetList.Count == 0 && !isRoaming) //Ÿ���� �������� �ʾҴµ� �ι����� �ƴ� ��(��������)
	{
	  isRoaming = true; // �ٽ� �ι� ����
	}
  }

  private void ControlAnim() //���� ���¿� ���� �ִϸ��̼� ��ȭ
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

	wait = false; // �÷��̾ ���ڿ��� ������ ���� ��� ���¸� false�� ��ȯ
  }

  public void FindPlayerByRat() //�㿡 ���ؼ� �÷��̾� �߰�
  {
	foundByRat = true;
  }

  public bool FindPlayer() // �÷��̾ Ž���ϴ� �Լ�
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

  public void Roaming() // �ι�
  {
	if (isB1) // ���� ������� ���
	{
	  isRoaming = true; // ���� ���¸� �ι������� üũ
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

  private void OnDrawGizmos() // ������ �þ߰��� �ð������� ������
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
