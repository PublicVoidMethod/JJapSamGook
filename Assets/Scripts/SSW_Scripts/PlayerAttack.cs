using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    //public static PlayerAttack pAttack;

    //private void Awake()
    //{
    //    if (pAttack = null)
    //    {
    //        pAttack = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    

    //연속공격 관련 변수
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 0.9f;
    public PlayerMove pm;
    Animator anim;
    public int attackSpeed = 8;

    void Start()
    {
        anim = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // 평타 
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
            //pm.pState = PlayerMove.PlayerFSM.Normal;
            //float h = Input.GetAxisRaw("Horizontal");
            //float v = Input.GetAxisRaw("Vertical");
            //Vector3 dir = new Vector3(h, 0, v);
            //dir.Normalize();
        }


    }

    public void attack1()
    {
        
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {


            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks >= 1 )// && dir != Vector3.zero)
            {
                anim.SetTrigger("Attack01");
               // pm.rb.AddForce(dir * attackSpeed , ForceMode.Impulse);
                //pm.rb.MovePosition(pm.transform.position + dir * attackSpeed * Time.deltaTime);              
            }
            else
            {
               // anim.SetBool("Attack01", false);
                pm.isAttack = false;
                pm.pState = PlayerMove.PlayerFSM.Normal;
                print(pm.pState);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 4);
             

        }
    }

    public void return1()
    {
        if (noOfClicks >= 2)
        {
            anim.SetTrigger("Attack02");
            //anim.SetBool("Attack01", false);
        }
        //else if (noOfClicks < 2)
        //{
        //    //anim.SetBool("Attack01", false);
        //    pm.isAttack = false;
        //    pm.pState = PlayerMove.PlayerFSM.Normal;
        //    noOfClicks = 0;
        //}
    }

    public void return2()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        if (noOfClicks >= 3)
        {
            anim.SetTrigger("Attack03");
            pm.rb.AddForce(dir * attackSpeed, ForceMode.Impulse);
            //anim.SetBool("Attack02", false);
        }
        //else if (noOfClicks < 3)
        //{
        //    //anim.SetBool("Attack02", false);
        //    pm.isAttack = false;
        //    pm.pState = PlayerMove.PlayerFSM.Normal;
        //    noOfClicks = 0;
        //}
    }

    public void return3()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        if (noOfClicks >= 4)
        {
            anim.SetTrigger("Attack04");
            pm.rb.AddForce(dir * attackSpeed, ForceMode.Impulse);
            //anim.SetBool("Attack03", false);
        }
        //else
        //{
        //    //anim.SetBool("Attack03", false);
        //    pm.isAttack = false;
        //    pm.pState = PlayerMove.PlayerFSM.Normal;
        //    noOfClicks = 0;
        //}
    }

    public void return4()
    {
        if(noOfClicks == 0)
           pm.isAttack = false;
           pm.pState = PlayerMove.PlayerFSM.Normal;
           //noOfClicks = 0;

    }
}
