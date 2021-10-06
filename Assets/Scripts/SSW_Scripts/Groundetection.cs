using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundetection : MonoBehaviour
{
    //float jumpCount = 1;
    public Animator anim;
    public PlayerMove pm;
    float curTime = 0;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        pm = GetComponentInParent<PlayerMove>();
        //print("�ٴ� ����!!!!!");

    }


    // Update is called once per frame
    void Update()
    {
       // print("�ٴ� ����!!!!!");
        //if (anim.GetBool("jumpStart") == true )
        //{
        //    Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
        //    RaycastHit hitInfo;

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        print("�ٴ� ����!!!!!");
        //        if (hitInfo.distance - 1 < 1.5f)
        //        {
        //            print("�ٴ� ����!");
        //            anim.SetBool("jumpStart", false);
        //            pm.jumpCount = 1;
        //            // pm.jumpCount = 0;
        //            anim.SetTrigger("JumpLanded");
                   

                
        //        }
        //    }
        //}
        //else
        //{
        //    return;
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetTrigger("JumpLanded");
            pm.jumpCount = 1;

        }
    }
<<<<<<< HEAD
=======

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {

            print("�ٴ�!!!!!");

            anim.SetTrigger("JumpLanded");
            pm.jumpCount = 1;

            //print(jumpCount);

        }
    }


>>>>>>> HSP
}
