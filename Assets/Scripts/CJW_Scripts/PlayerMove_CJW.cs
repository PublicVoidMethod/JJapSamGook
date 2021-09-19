using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_CJW : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, y);
        dir.Normalize();
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
