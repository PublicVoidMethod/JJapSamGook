using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundetection : MonoBehaviour
{
    //float jumpCount = 1;
    public Animator anim;
    public PlayerMove pm;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        pm = GetComponentInParent<PlayerMove>();
        print("�ٴ� ����!!!!!");

    }

    // Update is called once per frame
    void Update()
    {
        print("�ٴ� ����!!!!!");
        if (anim.GetBool("jumpStart") == true)
        {
            Ray ray = new Ray(transform.position, new Vector3(0, -3, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                print("�ٴ� ����!!!!!");
                if (hitInfo.distance - 1 < 0.3f)
                {
                    print("�ٴ� ����!");
                    anim.SetBool("jumpStart", false);
                    pm.jumpCount = 1;
                }
            }
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    print("�ٴ� ����!!!!!!!!!!!");

    //    if (other.gameObject.CompareTag("Ground"))
    //    {

    //        if (anim.GetBool("jumpStart") == true)
    //        {
    //            Ray ray = new Ray(transform.position, new Vector3(0, -3, 0));
    //            RaycastHit hitInfo;

    //            if (Physics.Raycast(ray, out hitInfo))
    //            {
    //                print("�ٴ� ����!!!!!");
    //                if (hitInfo.distance-1 < 0.3f)
    //                {
    //                    print("�ٴ� ����!");
    //                    anim.SetBool("jumpStart", false);
    //                    pm.jumpCount = 1;
    //                }
    //            }
    //        }

    //    }
    //}
}
