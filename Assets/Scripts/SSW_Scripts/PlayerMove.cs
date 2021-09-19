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

    //플레이어 이동
    public void Move()
    {
        

        // 점프
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
            // moveSpeed 변수의 값을 2배로 증가시킨다.
            dashSpeed = moveSpeed * 4;
           // print("대쉬");
        }
        // 그렇지 않고, 좌측 Shift 버튼을 떼면...
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // moveSpeed 변수의 값을 원래대로 한다.
            dashSpeed = moveSpeed;
           // print("대쉬x");
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
