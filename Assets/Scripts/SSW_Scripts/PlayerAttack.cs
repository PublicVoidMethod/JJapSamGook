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

    // 카메라 강조 공격 변수
    //public int gauge = 90;
    //public GameObject attackRange;
    public CameraType2_SSW camera;

    void Start()
    {
        anim = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Zattack());


    }

    public void attack1()
    {
        // 평타 
        // 콤보 딜레이 시간이 지나면 클릭횟수를 0으로 만든다
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
            //    //pm.pState = PlayerMove.PlayerFSM.Normal;
            //    //float h = Input.GetAxisRaw("Horizontal");
            //    //float v = Input.GetAxisRaw("Vertical");
            //    //Vector3 dir = new Vector3(h, 0, v);
            //    //dir.Normalize();
        }

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
                noOfClicks = 0;
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
        else if (noOfClicks < 2)
        {
            //anim.SetBool("Attack01", false);
            pm.isAttack = false;
            pm.pState = PlayerMove.PlayerFSM.Normal;
            noOfClicks = 0;
        }
    }

    public void return2()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        if (noOfClicks >= 3 && dir != Vector3.zero)
        {
            attackSpeed = 15;
            anim.SetTrigger("Attack03");
            pm.rb.AddForce(dir * attackSpeed, ForceMode.Impulse);
            //anim.SetBool("Attack02", false);
        }
        else if (noOfClicks < 3)
        {
            //anim.SetBool("Attack02", false);
            pm.isAttack = false;
            pm.pState = PlayerMove.PlayerFSM.Normal;
            noOfClicks = 0;
        }
    }

    public void return3()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        if (noOfClicks >= 4 && dir != Vector3.zero)
        {
            attackSpeed = 8;
            anim.SetTrigger("Attack04");
            pm.rb.AddForce(dir * attackSpeed, ForceMode.Impulse);
            //anim.SetBool("Attack03", false);
        }
        else
        {
            //anim.SetBool("Attack03", false);
            pm.isAttack = false;
            pm.pState = PlayerMove.PlayerFSM.Normal;
            noOfClicks = 0;
        }
    }

    public void return4()
    {
        if(noOfClicks == 4)
           pm.isAttack = false;
           pm.pState = PlayerMove.PlayerFSM.Normal;
           noOfClicks = 0;

    }

    public IEnumerator Zattack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {   
            anim.SetTrigger("ZAttack");
        }

        while (anim.GetCurrentAnimatorStateInfo(3).IsName("maintenance"))
        {
            camera.ZAttackTime();
            yield return new WaitForSeconds(2);
            //camera.BackPosition();
        }
    }
}
