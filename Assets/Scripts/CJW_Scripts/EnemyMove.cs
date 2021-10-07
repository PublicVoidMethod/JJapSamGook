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
        Die,
        ComboDamaged,
        Throw
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
    [HideInInspector] public int maxHP = 1000;
    [SerializeField] int curHP = 0;
    int liveNum = 1;
    Rigidbody rb;
    public float hitUp= 5.0f;
    public int specialNum;
    public int throwNum;
    GameObject sword;
    void Start()
    {
        currentTime = delayTime;
        eState = EnemyState.Idle;
        player = GameObject.FindGameObjectWithTag("Player"); // �ɽ¿찡 ��        
        anim = GetComponentInChildren<Animator>();
        curHP = maxHP;
        rb = GetComponent<Rigidbody>();
        specialNum = 1;
        throwNum = 1;
        sword = transform.GetComponentInChildren<SwordItem>().gameObject;
    }

    public void Update()
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
            case EnemyState.ComboDamaged:
                ComboDamaged();
                break;
            case EnemyState.Throw:
                Throw();
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

    public void Throw()
    {
        //캐릭터 뒷방향으로 설정한다.
        
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        rb.AddForce(-transform.forward * 50);
        rb.useGravity = true;
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        if(throwNum == 1)
        {
            anim.SetTrigger("SpecialThrow");
            throwNum = 0;
        }
        Invoke("skillUp", 2.0f);
    }

    public void ComboDamaged()
    {
        print("콤보데미지 먹었다.");
        //콤보 데미지, 필살기 데미지를 입으면, 다 맞을 때까지 공중에 떠있는다.
        curHP = 100000;
        rb.useGravity = false;
        Vector3 ePos = transform.position;

        rb.constraints = RigidbodyConstraints.FreezeAll;
        
        sword.transform.parent = null;
        sword.GetComponent<BoxCollider>().isTrigger = false;
        if(sword.GetComponent<Rigidbody>() == null)
        { 
        sword.AddComponent<Rigidbody>();
        }
        if (specialNum ==1)
        { 
        anim.SetTrigger("SpecialHit");
           
            specialNum = 0;
            
        }
      
    }
    public void specialNumUp()
    {
        specialNum = 1;
    }

    private void skillUp()
    {
        specialNum = 1;
        throwNum = 1;
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

    public void DirStop()
    {
      
    }


    public void OnHit(int damage)
    {
        curHP = Mathf.Max(curHP - damage, 0);
        print("현재 체력" + curHP);
    }

    public void Die()
    {
        print("죽었다.");
      if(liveNum ==0)
        { 
        anim.SetTrigger("Die");

            // �ݶ��̴��� ��Ȱ��ȭ�Ѵ�.
            //GetComponent<CapsuleCollider>().enabled = false;

            //sword = transform.GetComponentInChildren<SwordItem>().gameObject;
            sword.transform.parent = null;
            sword.GetComponent<BoxCollider>().isTrigger = false;
            sword.AddComponent<Rigidbody>();

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

