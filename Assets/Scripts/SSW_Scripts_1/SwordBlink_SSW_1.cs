using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlink_SSW_1 : MonoBehaviour
{
    Transform player;
    public GameObject sword;
    public GameObject landskill;
    Animator anim;
    int effectNum = 1;
    BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = sword.GetComponent<BoxCollider>();
        bc.enabled = false;
        player = GameObject.Find("Player_SSW1").transform;
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("SkillEffect") == true && anim.GetInteger("EffectNum") == 1)
        {
            GameObject go = Instantiate(landskill);
            go.transform.position = player.transform.position;
            anim.SetInteger("EffectNum", 0);
        }

    }

    private void On()
    {
        bc.enabled = true;
    }

    private void Off()
    {
        bc.enabled = false;
    }

    
}

