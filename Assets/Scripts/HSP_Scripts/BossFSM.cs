using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public float bMoveSpeed = 7.0f;  // 보스의 이동속도 변수
    public float sightDistance = 10.0f;  // 보스 시야의 길이
    public float sightDegree = 30.0f;  // 보스의 시야 각도의 크기

    Transform player;

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
        // 최초의 상태는 Idle(대기) 상태로 시작한다.
        bState = BossState.Idle;
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
        }
    }

    private void Idle()
    {

    }

    private void Move()
    {
        //if()
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
    /// 시야각이 잘 표현되어 있는지 확인
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
