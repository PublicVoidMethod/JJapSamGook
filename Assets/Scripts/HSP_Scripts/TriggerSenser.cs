using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSenser : MonoBehaviour
{
    CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }
}
