using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSensor : MonoBehaviour
{
    CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
}
