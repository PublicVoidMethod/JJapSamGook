using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIGHT : MonoBehaviour
{
    public int blinkTime = 10;
    public float ChangeSpeed = 10;
    public GameObject light1;
    Light lg;
    // Start is called before the first frame update
    void Start()
    {
       lg = light1.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
