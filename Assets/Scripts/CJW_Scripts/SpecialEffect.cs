using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    public GameObject sword;
    BoxCollider bc;
    public GameObject swordCube;
    SwordAction sa;
    Animator anim;

    public GameObject effect;
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    public GameObject effect4;

    //private void Awake()
    //{
    //    if (blink == null)
    //    {
    //        blink = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        bc = sword.GetComponent<BoxCollider>();
        bc.enabled = false;
        sa = GetComponentInChildren<SwordAction>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public IEnumerator SwordOnOff()
    //{
    //    bc.enabled = true;
    //    yield return null;
    //}

    public void SpecialOn()
    {
        bc.enabled = true;

        bc.size = new Vector3(3, 3, 4);
        
    }

    public void SpecialOff()
    {
        bc.enabled = false;
        bc.size = new Vector3(1, 1, 1);
        print(bc.enabled);
    }
    public void LandAttackOn()
    {
        bc.enabled = true;

        bc.size = new Vector3(100, 100, 100);
        sa.isThrow = true;
        anim.SetBool("SkillEffect", true);

    }

    public void LandAttackOff()
    {
        bc.enabled = false;
        anim.SetBool("SkillEffect", false);
        bc.size = new Vector3(1, 1, 1);
        //print(bc.enabled);
        sa.isThrow = true;
    }


    public void LMoveOn()
    {
       //swordCube.transform.localPosition = new Vector3(-0.148f, -0.042f, 0.25f);
       //swordCube.transform.localEulerAngles = new Vector3(-11.42f, -207.769f, 139.679f);

    }

    public void LMoveOff()
    {
        //swordCube.transform.localPosition = new Vector3(0.007637517f, 0.01312986f, -0.3659949f);
        //swordCube.transform.localEulerAngles = new Vector3(0, 0, 0);

    }

    public void OnEffect()
    {
        GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
    }
}
