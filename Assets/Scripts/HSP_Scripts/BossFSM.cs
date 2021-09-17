using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public float bMoveSpeed = 7.0f;  // ������ �̵��ӵ� ����
    public float sightDistance = 10.0f;  // ���� �þ��� ����
    public float sightDegree = 30.0f;  // ������ �þ� ������ ũ��

    Transform player;

    // ������ ������ ���
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
        // ������ ���´� Idle(���) ���·� �����Ѵ�.
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
    /// ������ �þ߰��� ���� �Լ�
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    Vector3[] BossViewingAngle(float radius, float angle)
    {
        Vector3[] results = new Vector3[2];

        // ���� �� ���� ��ǥ�� ���Ѵ�.
        float theta = 90 - angle - transform.eulerAngles.y;
        float posX = Mathf.Cos(theta * Mathf.Deg2Rad) * radius;
        float posY = transform.position.y;
        float posZ = Mathf.Sin(theta * Mathf.Deg2Rad) * radius;
        results[0] = new Vector3(posX, posY, posZ);

        // ���� �� ���� ��ǥ�� ���Ѵ�.
        theta = 90 + angle - transform.eulerAngles.y;
        posX = Mathf.Cos(theta * Mathf.Deg2Rad) * radius;
        posY = transform.position.y;
        posZ = Mathf.Sin(theta * Mathf.Deg2Rad) * radius;
        results[1] = new Vector3(posX, posY, posZ);

        return results;
    }

    /// <summary>
    /// �þ߰��� �� ǥ���Ǿ� �ִ��� Ȯ��
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
