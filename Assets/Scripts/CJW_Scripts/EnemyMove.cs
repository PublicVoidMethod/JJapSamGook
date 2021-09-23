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
    public float delayTime = 2;
    bool isNum = false;
    [HideInInspector] public int maxHP = 10;
    [SerializeField] int curHP = 0;
    int liveNum = 1;

    void Start()
    {
        currentTime = delayTime;
        eState = EnemyState.Idle;
        player = GameObject.FindGameObjectWithTag("Player"); // 심승우가 씀        
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
        float distance = dir.magnitude;
        if (distance <= attackRange)
        {
            
            eState = EnemyState.Attack;
           
            //CancelInvoke();

            //enemyAnim.SetTrigger("MoveToAttack");

            //조건에 맞다면 함수를 종료한다.
            //리턴이 나오면 아래 함수는 보지 않고 종료한다.
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
        //사정거리 안에 있어야 하고)
        if (distance < attackRange)
        {
            
            //print("이제부터 공격을 시작하겠다!");
            if (AttackNum == 0)
            {
           // print("0번 공격 게시!");
                anim.SetTrigger("Attack0");
                
                SetMoveState();
            }
            if(AttackNum == 1)
            {
           // print("1번 공격 게시!");
                anim.SetTrigger("Attack1");
                
                SetMoveState();
            }
            if (AttackNum == 2)
            {
           //print("2번 공격 게시!");
                anim.SetTrigger("Attack2");
                
                SetMoveState();
            }
            if (AttackNum == 3)
            {
            //print("3번 공격 게시!");

                anim.SetTrigger("Attack3");
             
                SetMoveState();
            }
          
        }
        //공격 범위 밖이면
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

        // 콜라이더를 비활성화한다.
        GetComponent<CapsuleCollider>().enabled = false;
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

    //    // 시야 범위의 양쪽 끝 지점을 구한다.
    //    Vector3[] sightPos = CalculateSightPoint(sightDistance, sightDegree);

    //    for (int i = 0; i < sightPos.Length; i++)
    //    {
    //        Gizmos.DrawLine(transform.position, transform.position + sightPos[i]);
    //    }
    //}
}