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
       
            transform.position = Vector3.Lerp(transform.position,viewPoint.position,Time.deltaTime * 2);

        
    }
}
