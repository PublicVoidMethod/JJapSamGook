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

    Transform player;  // "Player" 태그를 찾기위한 변수
    Rigidbody bossRB;  // 보스의 리지드 바디 변수
    

    // 보스의 열거형 상수
    public enum BossState
    {
        Idle,
        Move,
        Attack,
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
            case BossState.Damaged:
                Damaged();
                break;
            case BossState.Die:
                Die();
                break;
            default:
                break;
        }

        // 보스의 정면 벡터를 플레이어를 향하도록 회전한다.
        Vector3 dir = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void Idle()
    {
        // 플레이어의 포지션과 나(보스)의 포지션의 거리가 시야 범위보다 작다면
        // (=플레이어가 보스의 시야 안으로 들어갔다면)
        if(Vector3.Distance(player.position, transform.position) < sightDistance)
        {
            Vector3 lookPlayer = (player.position - transform.position).normalized;  // ???????????????? why noramlized?

            // 두 벡터를 내적(기준 벡터, 비교할 벡터)
            float cosValue = Vector3.Dot(transform.forward, lookPlayer);

            // 내적의 결과가 양수라면(즉, 플레이어가 앞에 있다면)
            if(cosValue > 0)
            {
                // 나의 정면 벡터와 플레이어를 바라보는 벡터와의 시앗각을 구한다.  ??????????????????????
                float degree = Mathf.Acos(cosValue) * Mathf.Rad2Deg;

                // 만약 보스의 시야각 안으로 들어왔다면
                if(degree < sightDegree)
                {
                    //// 보스의 정면 벡터를 플레이어를 향하도록 회전한다.
                    //Vector3 dir = (player.position - transform.position).normalized;
                    //transform.rotation = Quaternion.LookRotation(dir);

                    // 보스의 상태를 Move 상태로 전환한다.
                    bState = BossState.Move;
                }
            }
        }
    }

    private void Move()
    {
        // 나는(보스) 플레이어의 방향을 가지고
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        // 움직이고 싶다.
        transform.position += dir * bMoveSpeed * Time.deltaTime;

        // 나(보스)와 플레이어의 거리가 평타 공격 범위 안에 들어왔다면(평타 공격 범위 > 나와 플레이어의 거리)
        //if (attackRange > Vector3.Distance(player.position, transform.position))
        if (attackRange > (player.position - transform.position).magnitude)
        {
            // 이동을 멈추고
            bossRB.velocity = transform.position;
            // 어택 상태로 전환한다.
            bState = BossState.Attack;
        }
    }

    private void Attack()
    {

    }

    private void Damaged()
    {

    }

    private void Die()
    {

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
}
