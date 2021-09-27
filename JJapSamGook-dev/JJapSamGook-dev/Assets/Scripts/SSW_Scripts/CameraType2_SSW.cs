using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraType2_SSW : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = Vector3.zero;
    public Vector3 camOffset = Vector3.zero;

    Vector3 target;
    float followSpeed = 4;

    void Start()
    {

    }

    void Update()
    {
        //target = player.transform.forward * offset.z + player.transform.up * offset.y;
        target = player.position + player.forward + player.up + offset;

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * followSpeed);

        // 카메라가 플레이어를 계속 바라본다.
        Vector3 lookDir = (player.position + camOffset - transform.position).normalized;
        transform.forward = lookDir;
    }
}
