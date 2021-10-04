using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public float bMoveSpeed = 7.0f;  // 보스의 이동속도 변수
    public float sightDistance = 10.0f;  // 보스 시야의 길이
    public float sightDegree = 30.0f;  // 보스의 시야 각도의 크기
    public float attackRange = 2.0f;  // 보스의 평타 공격 범위

    public GameObject bossHPBar;  // 보스 HP바를 SetActive 할 수 있게 만든 변수
    public GameObject[] bossSkillEffect = new GameObject[3];  // 보스 스킬 이펙트
    public GameObject bossSword;  // 보스의 검을 가지고 올 변수

    Transform player;  // "Player" 태그를 찾기위한 변수
    Rigidbody bossRB;  // 보스의 리지드 바디 변수
    Animator bossAnim;  // 보스의 애니메이터 변수
    BossSkillSensor bossSkill;  // BossSkillSensor 스크립트를 거져올 변수
    GameObject bsc;  // BossSkillCollider의 게임 오브젝트를 넣을 변수
    CapsuleCollider capsulcollier;
    //ParticleSystem bossSkillParticle;  // 파티클 시스템을 넣을 변수

    bool isbooked = false;
    bool attackmotion = false;  // 보스가 공격 상태로 전환되는 변수. true(공격상태) false(비공격상태)
    bool isSkillCollider = false;  // 보스의 스킬의 콜라이더가 켜졌는지 꺼졌는지 체크하는 변수.

    // 보스의 열거형 상수
    public enum BossState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Damaged,
        Die
    }
    public BossState bState;

    void Start()
    {
        // 리지드 바디 캐싱
        bossRB = GetComponent<Rigidbody>();

        // 최초의 상태는 Idle(대기) 상태로 시작한다.
        bState = BossState.Idle;

        // Player라는 태그를 찾는다.
        player = GameObject.FindWithTag("Player").transform;

        capsulcollier = GetComponent<CapsuleCollider>();

        // 자식 오브젝트로부터 Animator 컴포넌트를 가져온다.
        bossAnim = GetComponentInChildren<Animator>();

        // BossSkillSensor 스크립트를 가져온다.
        bossSkill = GetComponentInChildren<BossSkillSensor>();

        // 자식으로 있는 BossSkillCollider를 비활성화 시킨다.
        bsc = transform.Find("BossSkillColliders").gameObject;
        bsc.SetActive(false);
    }

    void Update()
    {
        switch (bState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Skill:
                Skill();
                break;
            case BossState.Damaged:
                Damaged();
                break;
            default:
                break;
        }

        // 보스의 정면 벡터를 플레이어를 향하도록 회전한다.
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir);

        // 보스의 체력이 0이 되면
        if(BossStatus.instance.bossCurrentHP <= 0)
        {
            // 스크립트를 비활성화 시킨다.
            this.enabled = false;
            capsulcollier.isTrigger = true;
        }
    }

    private void Idle()
    {
        // 플레이어의 포지션과 나(보스)의 포지션의 거리가 시야 범위보다 작다면
        // (=플레이어가 보스의 시야 안으로 들어갔다면)
        if (Vector3.Distance(player.position, transform.position) < sightDistance)
        {
            Vector3 lookPlayer = (player.position - transform.position).normalized;  // ???????????????? why noramlized?

            // 두 벡터를 내적(기준 벡터, 비교할 벡터)
            float cosValue = Vector3.Dot(transform.forward, lookPlayer);

            // 내적의 결과가 양수라면(즉, 플레이어가 앞에 있다면)
            if (cosValue > 0)
            {
                // 나의 정면 벡터와 플레이어를 바라보는 벡터와의 사잇각을 구한다.  ??????????????????????
                float degree = Mathf.Acos(cosValue) * Mathf.Rad2Deg;

                // 만약 보스의 시야각 안으로 들어왔다면
                if (degree < sightDegree)
                {
                    bossHPBar.SetActive(true);

                    //// 보스의 정면 벡터를 플레이어를 향하도록 회전한다.
                    //Vector3 dir = (player.position - transform.position).normalized;
                    //transform.rotation = Quaternion.LookRotation(dir);

                    SetMoveState();
                }
            }
        }
    }

    private void Move()
    {
        // 나는(보스) 플레이어의 방향을 가지고
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        dir.Normalize();

        // 움직이고 싶다.
        transform.position += dir * bMoveSpeed * Time.deltaTime;

        // 나(보스)와 플레이어의 거리가 평타 공격 범위 안에 들어왔다면(평타 공격 범위 > 나와 플레이어의 거리)
        //if (attackRange > Vector3.Distance(player.position, transform.position))
        if (attackRange > (player.position - transform.position).magnitude)
        {
            // 무브 애니메이션 호출
            bossAnim.SetTrigger("RunToAttack");
            print("공격해");
            // 이동을 멈추고
            bossRB.velocity = Vector3.zero;
            
            // 어택 상태로 전환한다.
            bState = BossState.Attack;

            //// 공격 애니메이션을 실행한다.  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //bossAnim.SetTrigger("MoveToAttack");  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }
    }

    private void Attack()
    {
        if (attackmotion == false)
        {
            // 만약 나(보스)와 플레이어와의 거리가 평타 공격 범위보다 멀어지면(평타 공격 범위 < 나와 플레이어의 거리)
            if (attackRange < (player.position - transform.position).magnitude)
            {
                bossAnim.SetTrigger("AttackToIdle");
                if (!isbooked)
                {
                    // Move상태로 1.5초 후에 전환한다.
                    Invoke("SetMoveState", 0.5f);
                    //return;

                    //bossAnim.SetTrigger("IdleToMove");
                }
            }
            else
            {
                AttackPattern();
            }
        }
    }

    private void Skill()
    {
        if(isSkillCollider == false)
        {
            isSkillCollider = true;
            // 스킬 패턴을 시작하는 애니메이션을 실행하고
            bossAnim.SetTrigger("OnSkill");

            //StartCoroutine(AnimStopPlay()); @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            //// 보스의 검 끝을 바닥을 향하도록 회전한다.
            //bossSword.transform.Rotate(102, 0, 0);

            // n초 후 파티클을 실행한다.
            StartCoroutine(BossSkillEffect());
        }
        bState = BossState.Idle;
    }

    private void Damaged()
    {

    }

    void SetMoveState()
    {
        // 보스의 상태를 Move 상태로 전환한다.
        bState = BossState.Move;

        // 이동 애니메이션을 실행한다.
        bossAnim.SetTrigger("IdleToMove");

        isbooked = false;
    }

    /// <summary>
    /// 보스의 시야각을 만든 함수
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    Vector3[] BossViewingAngle(float radius, float angle)
    {
        Vector3[] results = new Vector3[2];

        // 우측 끝 점의 좌표를 구한다.
        float theta = 90 - angle - transform.eulerAngles.y;
        float posX = Mathf.Cos(theta * Mathf.Deg2Rad) * radius;
        float posY = transform.position.y;
        float posZ = Mathf.Sin(theta * Mathf.Deg2Rad) * radius;
        results[0] = new Vector3(posX, posY, posZ);

        // 좌측 끝 점의 좌표를 구한다.
        theta = 90 + angle - transform.eulerAngles.y;
        posX = Mathf.Cos(theta * Mathf.Deg2Rad) * radius;
        posY = transform.position.y;
        posZ = Mathf.Sin(theta * Mathf.Deg2Rad) * radius;
        results[1] = new Vector3(posX, posY, posZ);

        return results;
    }

    /// <summary>
    /// 시야각이 잘 표현되어 있는지 기즈모로 확인
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3[] sightPos = BossViewingAngle(sightDistance, sightDegree);

        for(int i = 0; i < sightPos.Length; i++)
        {
            Gizmos.DrawLine(transform.position, transform.position + sightPos[i]);
        }
    }

    /// <summary>
    /// 보스의 공격 패턴을 랜덤으로 실행 시키는 함수
    /// </summary>
    void AttackPattern()
    {
        int randomAttack = UnityEngine.Random.Range(0, 4);
        switch (randomAttack)
        {
            case 0:
                // 공격 패턴 0
                StartCoroutine(Attack_00());
                break;
            case 1:
                // 공격 패턴 1
                StartCoroutine(Attack_01());
                break;
            case 2:
                // 공격 패턴 2
                StartCoroutine(Attack_02());
                break;
            case 3:
                // 공격 패턴 3
                StartCoroutine(Attack_03());
                break;
        }
    }

    IEnumerator Attack_00()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_00");
        yield return new WaitForSeconds(2.15f);
        attackmotion = false;
    }

    IEnumerator Attack_01()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_01");
        yield return new WaitForSeconds(3.40f);
        attackmotion = false;
    }

    IEnumerator Attack_02()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_02");
        yield return new WaitForSeconds(3.0f);
        attackmotion = false;
    }

    IEnumerator Attack_03()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_03");
        yield return new WaitForSeconds(3.8f);
        attackmotion = false;
    }

    IEnumerator BossSkillEffect()
    {
        // 박스 콜라이더가 true상태가 되는 시간이 존재 할텐데
        yield return new WaitForSeconds(2f);
        // 스킬 애니메이션 동작을 하는 n초 후에 BossSkillSensor의 BoxCollider를 활성화 시킨다.
        bsc.SetActive(true);

        // 스킬 이펙트를 활성화하고
        for(int i = 0; i < bossSkillEffect.Length; i++)
        {
            bossSkillEffect[i].SetActive(true);
            ParticleSystem particle = bossSkillEffect[i].GetComponentInChildren<ParticleSystem>();
            if(particle != null)
            {
                particle.Stop();
                particle.Play();
            }
        }
        // 파티클을 실행한다.
        // bossSkillParticle.Stop();
        // bossSkillParticle.Play();
        // 스킬 애니메이션이 실행되고 스킬 파티클이 나오기 전까지의 딜레이 시간
    }

    //IEnumerator AnimStopPlay()  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    //{
    //    // 스킬 애니메이션을 플레이 하고
    //    bossAnim.SetTrigger("OnSkill");

    //    // 1.04초를 기다린 후
    //    yield return new WaitForSeconds(1f);

    //    // 애니메이션을 정지시킨다.
    //    bossAnim.StopPlayback();

    //    yield return new WaitForSeconds(1f);
    //    bossAnim.StartPlayback();
    //}
}
