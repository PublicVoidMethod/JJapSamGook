using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
 public enum EnemyState
    {
        Idle,
        Move,
        Attack
    }

    public EnemyState eState;
    public float attackDistance = 6;
    public float attackRange = 1.5f;
    GameObject player;
    public float moveSpeed = 5;

    float currentTime = 0;
    float delayTime = 2;
    
    void Start()
    {
        eState = EnemyState.Idle;
        player = GameObject.Find("Player");
    }

    protected void Update()
    {
        switch (eState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }
    
    }

    private void Idle()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        if (attackDistance >= distance)
        {

            SetMoveState();
          
        }
    }

    private void SetMoveState()
    {
        eState = EnemyState.Move;
    }



    private void Move()
    {
        Vector3 dir = player.transform.position - transform.position;
        float distance = dir.magnitude;
        if (distance <= attackRange)
        {
            eState = EnemyState.Attack;
            currentTime = 0;
            //CancelInvoke();

            //enemyAnim.SetTrigger("MoveToAttack");

            //���ǿ� �´ٸ� �Լ��� �����Ѵ�.
            //������ ������ �Ʒ� �Լ��� ���� �ʰ� �����Ѵ�.
            return;
        }


        dir.Normalize();
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public virtual void Attack()
    {
        print("�������� ������ �����ϰڴ�!");
        Vector3 dir = player.transform.position - transform.position;
        float distance = dir.magnitude;
        currentTime += Time.deltaTime;
        //�����Ÿ� �ȿ� �־�� �ϰ�)
        if (distance < attackRange)
        {
           
        }
        //���� ���� ���̸�
        else
        {
            SetMoveState();

        }
    }

    public void Die()
    {

    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.white;

    //    // �þ� ������ ���� �� ������ ���Ѵ�.
    //    Vector3[] sightPos = CalculateSightPoint(sightDistance, sightDegree);

    //    for (int i = 0; i < sightPos.Length; i++)
    //    {
    //        Gizmos.DrawLine(transform.position, transform.position + sightPos[i]);
    //    }
    //}
}