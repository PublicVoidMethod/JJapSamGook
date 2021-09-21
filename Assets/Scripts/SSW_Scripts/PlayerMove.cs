using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    //public float gravity = -9.8f;
    //public float yVelocity = 0;
    public float jumpPower = 10;
    
    
    float jumpCount = 2;
    float dashSpeed;

    public Rigidbody rb;
    
    void Start()
    {
        

    }


    void Update()
    {
        dashSpeed = moveSpeed;
        //isJumping = false;
        Move();
       
    }

    //�÷��̾� �̵�
    public void Move()
    {
        

        // ����
        rb = GetComponent<Rigidbody>();
        
        
        if (Input.GetButtonDown("Jump") )
        {
            if (jumpCount > 0)
            {
                
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                jumpCount--;
                
            }
            else
            {
                return;
            }
            
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // moveSpeed ������ ���� 2��� ������Ų��.
            dashSpeed = moveSpeed * 4;
           // print("�뽬");
        }
        // �׷��� �ʰ�, ���� Shift ��ư�� ����...
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // moveSpeed ������ ���� ������� �Ѵ�.
            dashSpeed = moveSpeed;
           // print("�뽬x");
        }

        rb.MovePosition(transform.position + dir * dashSpeed * Time.deltaTime);

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
    }
}
