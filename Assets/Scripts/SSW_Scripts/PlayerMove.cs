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
    
    public float jumpCount = 1;
   // float attackCount = 4;
    float dashSpeed;

    public Rigidbody rb;

    Animator anim;


   // public GameObject hitattack;

    void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody>();
        //hitattack = GameObject.FindGameObjectWithTag("Enemy");
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        dashSpeed = moveSpeed;
        //isJumping = false;
        Move();
        //GeneralAttack();
        if(currentHp == 0)
        {
            livenumber--;
            DiePlayer();
        }
       
    }

    // 피격 데미지 + 애니메이션
    public void DamageProcess(float damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);
        print(currentHp);
        print("데미지" + damage);

        anim.SetTrigger("take_Damage");
    }

    // 죽음 애니메이션
    public void DiePlayer()
    {
        if(livenumber == 0)
        {
            // 콜라이더를 비활성화한다.
            GetComponent<CapsuleCollider>().enabled = false;
            rb.useGravity = false;
            anim.SetTrigger("Die");
            

        }
    }
    //플레이어 이동
    public void Move()
    {
        

        // 점프      
        if (Input.GetButtonDown("Jump") )
        {
            if (jumpCount > 0)
            {
                
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                jumpCount--;
                

                if (anim.GetBool("jumpStart") == false)
                {

                    anim.SetBool("jumpStart", true);
                }
                else
                {
                    return;
                }
                




            }
            //else
            //{
            //    return;
            //}
            
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);

        // 이동하려는 방향으로 캐릭터를 회전시킨다.
        if (dir != Vector3.zero)
        {
            Vector3 rot = dir;
            rot.y = 0;
            transform.rotation = Quaternion.LookRotation(rot);
        }

        curTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && curTime > 0.3f )
        {
            curTime = 0;
            anim.SetTrigger("Roll");
        }
        

        //rb.MovePosition(transform.position + dir * dashSpeed * Time.deltaTime);

        transform.position += dir * dashSpeed * Time.deltaTime;


        // 평타 
        if (Input.GetMouseButtonDown(0))
        {
            //SwordBlink_SSW.blink.On();
            //if(hitattack != null)
            //{

                //HitAction_SSW attack = hitattack.GetComponentInChildren<HitAction_SSW>();// @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                //attack.attackDamage = 0;  // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                anim.SetTrigger("Attack01");
                // attackCount--;
                // print(attackCount);

                
            //}
            //else
            //{

            //    anim.SetTrigger("Attack01");
            //}


        }
            //SwordBlink_SSW.blink.Off();



    }

    //public void GeneralAttack()
    //{
    //    if (Input.GetMouseButton(0) && attackCount > 0)
    //    {
    //        anim.SetTrigger("Attack01");
    //        attackCount--;
    //        print(attackCount);
    //    }
    //    else if (attackCount < 3)
    //    {
    //        anim.SetTrigger("Attack02");
    //        attackCount--;
    //        print("공격1");
    //    }
    //    else if (attackCount < 2)
    //    {
    //        anim.SetTrigger("Attack03");
    //        attackCount--;
    //        print("공격2");
    //    }
    //    else if (attackCount < 1)
    //    {
    //        anim.SetTrigger("Attack04");
    //        attackCount = 4;
    //        print("공격3");
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {

    //        if (anim.GetBool("jumpStart") == true)
    //        {
    //            Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
    //            RaycastHit hitInfo;

    //            if (Physics.Raycast(ray, out hitInfo))
    //            {
    //                print("바닥 측정!!!!!");
    //                if (hitInfo.distance - 1 < 0.2f)
    //                {
    //                    print("바닥 측정!");
    //                    anim.SetBool("jumpStart", false);
    //                    jumpCount = 1;
    //                }
    //            }
    //        }

    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {

    //        if (anim.GetBool("jumpStart") == true)
    //        {
    //            Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
    //            RaycastHit hitInfo;

    //            if (Physics.Raycast(ray, out hitInfo))
    //            {
    //                print("바닥 측정!!!!!");
    //                if (hitInfo.distance - 1 < 0.2f)
    //                {
    //                    print("바닥 측정!");
    //                    anim.SetBool("jumpStart", false);
    //                    jumpCount = 1;
    //                }
    //            }
    //        }

    //    }


    //}



}
