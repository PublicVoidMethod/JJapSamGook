using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //연속공격 관련 변수
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 0.9f;
    PlayerMove playermove;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        playermove = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // 평타 
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                anim.SetBool("Attack01", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 4);

        }

        if (Input.GetKeyDown(KeyCode.R))
            {
           
            playermove.enabled = false;
            anim.SetTrigger("Special");
            Invoke("playerMoveOff", 1.0f);
        }
        
    }
    
    void playerMoveOff()
    {
        playermove.enabled = true;
    }

    public void return1()
    {
        if (noOfClicks >= 2)
        {
            anim.SetBool("Attack02", true);
        }
        else
        {
            anim.SetBool("Attack01", false);
            noOfClicks = 0;
        }
    }

    public void return2()
    {
        if (noOfClicks >= 3)
        {
            anim.SetBool("Attack03", true);
        }
        else
        {
            anim.SetBool("Attack02", false);
            noOfClicks = 0;
        }
    }

    public void return3()
    {
        if (noOfClicks >= 4)
        {
            anim.SetBool("Attack04", true);
        }
        else
        {
            anim.SetBool("Attack03", false);
            noOfClicks = 0;
        }
    }

    public void return4()
    {
        anim.SetBool("Attack01", false);
        anim.SetBool("Attack02", false);
        anim.SetBool("Attack03", false);
        anim.SetBool("Attack04", false);
        noOfClicks = 0;

    }

   
}
