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

    // z 어택 포지션
    public GameObject zattackPos;
   // public PlayerAttack pAttack;

    void Start()
    {

    }

    private void LateUpdate()
    {
        //target = player.transform.forward * offset.z + player.transform.up * offset.y;
        target = player.position + player.forward + player.up + offset;

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * followSpeed);

        // 카메라가 플레이어를 계속 바라본다.
        Vector3 lookDir = (player.position + camOffset - transform.position).normalized;
        transform.forward = lookDir;
    }

    // 마무리 공격시 카메라를 이동시킨다
    public void ZAttackTime()
    {
        transform.position = zattackPos.transform.position;
        //StartCoroutine(CameraShake(0.5f, 1.5f));
    }

    public IEnumerator CameraShake(float shakeTime, float shakePower)
    {
        float t = 0;
        float p = -Mathf.Clamp(shakePower, 0f, 1.5f) / 10f;
        while (t < shakeTime)
        {
            zattackPos.transform.position += new Vector3(0, p, 0);
            p *= -1f * 0.95f;
            t += Time.deltaTime;
            yield return null;
        }
    }
    public void BackPosition()
    {
        transform.position = Vector3.Lerp(zattackPos.transform.position, target, Time.deltaTime * followSpeed);
    }
    
}
