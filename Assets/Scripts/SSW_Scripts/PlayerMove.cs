using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public float gravity = -9.8f;
    public float yVelocity = 0;
    public float jumpPower = 10;
    float dashSpeed;

    public Rigidbody rb;
    
    void Start()
    {
        
    }

    
    void Update()
    {
       
        Move();
    }

    //플레이어 이동
    public void Move()
    {
        dashSpeed = moveSpeed;

        // 점프
        rb = GetComponent<Rigidbody>();
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // moveSpeed 변수의 값을 2배로 증가시킨다.
            dashSpeed = moveSpeed * 2;
            
        }
        // 그렇지 않고, 좌측 Shift 버튼을 떼면...
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // moveSpeed 변수의 값을 원래대로 한다.
            dashSpeed = moveSpeed;
        }

        transform.position += dir * Time.deltaTime;
    }
}
