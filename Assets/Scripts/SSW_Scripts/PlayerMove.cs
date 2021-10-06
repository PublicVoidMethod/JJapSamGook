using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float currentHp = 0;
    public float maxHp = 20;
    //public float gravity = -9.8f;
    //public float yVelocity = 0;
    public float jumpPower = 10;
    public int livenumber = 1;
    float curTime = 0;
    //���Ӱ��� ���� ����
    //public int noOfClicks = 0;
    //float lastClickedTime = 0;
    //public float maxComboDelay = 0.9f;
    public float rotationSpeed = 8;
    public float jumpCount = 1;
    bool isMove = false;


    //public float yVelocity = 0;
    public float dashSpeed;

    public Rigidbody rb;
    public PlayerAttack pAttck;
    public bool isAttack = false;
    Animator anim;
    
   // public GameObject hitattack;

    public enum PlayerFSM
    {
        Normal,
        Idle,
        Damaged,
        Attack,
        Die
    }

    public PlayerFSM pState;
    void Start()
    {
        dashSpeed = moveSpeed;
        currentHp = maxHp;
        rb = GetComponent<Rigidbody>();       
        anim = GetComponentInChildren<Animator>();
        pState = PlayerFSM.Normal;
        print(livenumber);
        pAttck = GetComponentInChildren<PlayerAttack>();
    }


    void Update()
    {
        switch (pState)
        {
            case PlayerFSM.Normal:
                Normal();
                break;
            case PlayerFSM.Idle:
                Idle();
                break;
            case PlayerFSM.Damaged:
                Damaged();
                break;
            case PlayerFSM.Attack:
                Attack();
                break;
            case PlayerFSM.Die:
                Die();
                break;
        }


        //dashSpeed = moveSpeed;
        ////isJumping = false;
        //Move();
       
        

    }
    private void Normal()
    {
       // StopAllCoroutines();
        Move();
        //print(pState);
        if (isAttack == true)
        {
            pState = PlayerFSM.Attack;
            print(pState);
        }
    }

    private void Idle()
    {
        throw new NotImplementedException();
    }

    private void Damaged()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        //moveSpeed = 3;
        pAttck.attack1();
        if(isAttack == false)
        {
            pState = PlayerFSM.Normal;
        }
        print(pState);

    }

    private void Die()
    {
        if (currentHp == 0)
        {
            livenumber--;
            DiePlayer();
        }
    }


    // �ǰ� ������ + �ִϸ��̼�
    public void DamageProcess(float damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);
        print(currentHp);
        print("������" + damage);

        anim.SetTrigger("take_Damage");
    }

    // ���� �ִϸ��̼�
    public void DiePlayer()
    {
        if(livenumber == 0)
        {
            // �ݶ��̴��� ��Ȱ��ȭ�Ѵ�.
            GetComponent<CapsuleCollider>().enabled = false;
            rb.useGravity = false;
            anim.SetTrigger("Die");
            this.enabled = false;

        }
    }
    //�÷��̾� �̵�
    public void Move()
    {
        isMove = true;
      if (isMove)
      {

        // ����      
        if (Input.GetButtonDown("Jump") )
        {
            print(jumpCount + "after");
            if (jumpCount > 0)
            {
                //yVelocity = jumpPower;
                //transform.position +=  * jumpPower * Time.deltaTime;
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);             
                jumpCount--;
                
                anim.SetTrigger("jumpStart");
                //anim.ResetTrigger("JumpLanded");

            }

            
        }

;       float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);

        // �̵��Ϸ��� �������� ĳ���͸� ȸ����Ų��.
        if (dir != Vector3.zero)
        {
            Vector3 rot = dir;
            rot.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(rot);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);                                                                                                              

        }

        //������ && dir != Vector3.zero && dir != Vector3.zero && curTime > 0.2f
        curTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            print("roll");
            curTime = 0;
            anim.SetTrigger("Roll");
            if(jumpCount  <= 0)
            {
                print("jump");
                anim.SetTrigger("JumpRoll");
            }
        }

        //rb.MovePosition(transform.position + dir * dashSpeed * Time.deltaTime);

        transform.position += dir * dashSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Z))
        {
            isAttack = true;
            isMove = false;
           // pAttck.noOfClicks++;
        }
      }
    }



}
