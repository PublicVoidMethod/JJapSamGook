using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_CJW : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", y);


        Vector3 dir = new Vector3(x, 0, y);
        dir.Normalize();
        transform.position += dir * moveSpeed * Time.deltaTime;
    


    if(Input.GetMouseButton(0))
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetMouseButton(1))
        {
            anim.SetTrigger("Attack1");
        }
    }
}
