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

    //�÷��̾� �̵�
    public void Move()
    {
        dashSpeed = moveSpeed;

        // ����
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
            // moveSpeed ������ ���� 2��� ������Ų��.
            dashSpeed = moveSpeed * 2;
            
        }
        // �׷��� �ʰ�, ���� Shift ��ư�� ����...
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // moveSpeed ������ ���� ������� �Ѵ�.
            dashSpeed = moveSpeed;
        }

        transform.position += dir * Time.deltaTime;
    }
}
