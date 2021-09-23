using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlink_SSW : MonoBehaviour
{
    public GameObject sword;
    BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = sword.GetComponent<BoxCollider>();
        //bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void On()
    {
        bc.enabled = true;
        print("¿Â");
    }

    private void Off()
    {
        bc.enabled = false;
        print("¿ÀÇÁ");
    }

    
}
