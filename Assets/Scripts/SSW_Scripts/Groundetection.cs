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
        //print("¹Ù´Ú ÃøÁ¤!!!!!");

    }

    // Update is called once per frame
    void Update()
    {
       // print("¹Ù´Ú ÃøÁ¤!!!!!");
        //if (anim.GetBool("jumpStart") == true )
        //{
        //    Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
        //    RaycastHit hitInfo;

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        print("¹Ù´Ú ÃøÁ¤!!!!!");
        //        if (hitInfo.distance - 1 < 1.5f)
        //        {
        //            print("¹Ù´Ú ÃøÁ¤!");
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
}
