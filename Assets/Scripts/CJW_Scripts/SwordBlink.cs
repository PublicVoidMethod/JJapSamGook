using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlink : MonoBehaviour
{
    Transform player;
    public GameObject sword;
    public GameObject landskill;

    BoxCollider bc;

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

    private void On()
    {
        bc.enabled = true;
    }

    private void Off()
    {
        bc.enabled = false;
    }

    private void SkillEffect()
    {
        GameObject go = Instantiate(landskill);
        go.transform.position = player.transform.position;
    }
}
