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
        //print("¹Ù´Ú ÃøÁ¤!!!!!");

    }


    // Update is called once per frame
    void Update()
    {
        //curTime += Time.deltaTime;
        //// print("¹Ù´Ú ÃøÁ¤!!!!!");
        //if (anim.GetBool("jumpStart") == true && pm.rb.velocity.y < 0 && curTime > 0.5f )
        //{
        //    //curTime = 0;
        //    Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
        //    RaycastHit hitInfo;

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        if (hitInfo.distance  < 2.6f)
        //        {
        //            print(hitInfo.distance);
        //            anim.SetBool("jumpStart", false);
        //            pm.jumpCount = 1;
        //            // pm.jumpCount = 0;
                   

                
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

            print("¹Ù´Ú!!!!!");

            anim.SetTrigger("JumpLanded");
            pm.jumpCount = 1;

            //print(jumpCount);

        }
    }


}
