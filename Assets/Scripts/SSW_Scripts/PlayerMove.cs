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

    //public float yVelocity = 0;
    float dashSpeed;

    public Rigidbody rb;

    Animator anim;


    // public GameObject hitattack;

    void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        dashSpeed = moveSpeed;
        //isJumping = false;
        Move();

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
        if (livenumber == 0)
        {
            // �ݶ��̴��� ��Ȱ��ȭ�Ѵ�.
            GetComponent<CapsuleCollider>().enabled = false;
            rb.useGravity = false;
            anim.SetTrigger("Die");
            this.enabled = false;

        }
    }

    public void Move()
    {


        // 점프      
        if (Input.GetButtonDown("Jump"))
        {
            print(jumpCount + "after");
            if (jumpCount > 0)
            {
                //yVelocity = jumpPower;
                //transform.position +=  * jumpPower * Time.deltaTime;
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                jumpCount--;

                anim.SetTrigger("jumpStart");
                anim.ResetTrigger("JumpLanded");

            }


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
            Quaternion newRotation = Quaternion.LookRotation(rot);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

        }

        //구르기 && dir != Vector3.zero
        curTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && curTime > 0.3f && dir != Vector3.zero)
        {
            curTime = 0;
            anim.SetTrigger("Roll");
        }


        //rb.MovePosition(transform.position + dir * dashSpeed * Time.deltaTime);

        transform.position += dir * dashSpeed * Time.deltaTime;


    }

  




//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Ground"))
//        {
            
//<<<<<<<<< Temporary merge branch 1
//           print("바닥 측정!");
//=========
//           print("占쌕댐옙 占쏙옙占쏙옙!");
//>>>>>>>>> Temporary merge branch 2
//           jumpCount = 1;

//            print(jumpCount);
                    
//        }
//    }

//<<<<<<<<< Temporary merge branch 1

//=========
//>>>>>>>>> Temporary merge branch 2



}
