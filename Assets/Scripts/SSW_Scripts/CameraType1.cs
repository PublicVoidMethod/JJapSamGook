using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraType1 : MonoBehaviour
{
    public Transform viewPoint;
    public float followSpeed = 2;
    

    void Start()
    {

    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        // �̵��Ϸ��� �������� ĳ���͸� ȸ����Ų��.
        if (dir != Vector3.zero)
        {
            Vector3 rot = dir;
            rot.y = 0;
            transform.rotation = Quaternion.LookRotation(rot);
        }
       
        transform.position = Vector3.Lerp(transform.position,viewPoint.position,Time.deltaTime * 2);


    }
}
