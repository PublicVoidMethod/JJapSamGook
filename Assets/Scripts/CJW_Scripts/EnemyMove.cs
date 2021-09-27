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
        Attack,
            Die
    }

    public EnemyState eState;
    public float attackDistance = 6;
    public float attackRange = 1.5f;
    GameObject player;
    public float moveSpeed = 5;
    Animator anim;
    float currentTime = 0;
    public float delayTime = 1;
    bool isNum = false;
    [HideInInspector] public int maxHP = 10;
    [SerializeField] int curHP = 0;
    int liveNum = 1;

    void Start()
    {
        currentTime = delayTime;
        eState = EnemyState.Idle;
        player = GameObject.FindGameObjectWithTag("Player"); // �ɽ¿찡 ��        
        anim = GetComponentInChildren<Animator>();
        curHP = maxHP;

        
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
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }
        
        if(curHP == 0)
        {
            liveNum--;
            eState =  EnemyState.Die;
        }
        currentTime += Time.deltaTime;


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
        isNum = false;
        currentTime = 0;
    }



    private void Move()
    {
        
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0;
        float distance = dir.magnitude;
        if (distance <= attackRange)
        {
            
            eState = EnemyState.Attack;
           
            //CancelInvoke();

            //enemyAnim.SetTrigger("MoveToAttack");

            //���ǿ� �´ٸ� �Լ��� �����Ѵ�.
            //������ ������ �Ʒ� �Լ��� ���� �ʰ� �����Ѵ�.
            return;
        }

        
        dir.Normalize();
        anim.SetTrigger("Run");
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public virtual void Attack()
    {
         
        //print(currentTime);
        int AttackNum = 4;
        if (currentTime >= delayTime)

        { 
            if (isNum == false)
        {
            AttackNum = UnityEngine.Random.Range(0, 4);
            //print(AttackNum);
            isNum = true;
        }
        }
        Vector3 dir = player.transform.position - transform.position;
        float distance = dir.magnitude;
        currentTime += Time.deltaTime;
        //�����Ÿ� �ȿ� �־�� �ϰ�)
        if (distance < attackRange)
        {
            
            //print("�������� ������ �����ϰڴ�!");
            if (AttackNum == 0)
            {
           // print("0�� ���� �Խ�!");
                anim.SetTrigger("Attack0");
                
                SetMoveState();
            }
            if(AttackNum == 1)
            {
           // print("1�� ���� �Խ�!");
                anim.SetTrigger("Attack1");
                
                SetMoveState();
            }
            if (AttackNum == 2)
            {
           //print("2�� ���� �Խ�!");
                anim.SetTrigger("Attack2");
                
                SetMoveState();
            }
            if (AttackNum == 3)
            {
            //print("3�� ���� �Խ�!");

                anim.SetTrigger("Attack3");
             
                SetMoveState();
            }

         
          
        }
        //���� ���� ���̸�
        else
        {
            SetMoveState();

        }
        
    }

    public void OnHit(int damage)
    {
        curHP = Mathf.Max(curHP - damage, 0);
        print(curHP);
    }

    public void Die()
    {
      if(liveNum ==0)
        { 
        anim.SetTrigger("Die");

        // �ݶ��̴��� ��Ȱ��ȭ�Ѵ�.
        //GetComponent<CapsuleCollider>().enabled = false;
        Invoke("EnemyDestroy", 3.0f);
        }
    }

    public void EnemyDestroy()
    {
        Destroy(gameObject);
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