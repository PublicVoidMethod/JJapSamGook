using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlink_SSW : MonoBehaviour
{
    //public static SwordBlink_SSW blink;


    Transform player;
    public GameObject landskill;
    public GameObject sword;
    BoxCollider bc;

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
        player = GameObject.Find("Player_SSW1").transform;
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

    public void On()
    {
        bc.enabled = true;
        //print(bc.enabled);
    }

    public void Off()
    {
        bc.enabled = false;
        //print(bc.enabled);
    }

    private void SkillEffect()
    {
        GameObject go = Instantiate(landskill);
        go.transform.position = player.transform.position;
    }

}
