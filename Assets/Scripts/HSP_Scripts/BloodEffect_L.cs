using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect_L : MonoBehaviour
{
    public GameObject bloodEffect_L;  // 피 흘리는 파티클을 넣어줄 변수

    ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        // 만약 부딪힌 대상의 이름이 AttackRange를 포함하고 있다면
        if (other.gameObject.name.Contains("AttackRange"))
        {
            // 피흘리는 이펙트를 생성하고
            GameObject go = Instantiate(bloodEffect_L);
            // 이펙트의 위치를 콜라이더가 부딪힌 곳으로 지정하고
            go.transform.position = other.transform.position;
            // 이펙트를 회전한다.
            go.transform.Rotate(0, 90, 0);

            // 파티클을 실행한다.
            ps = go.GetComponent<ParticleSystem>();
            ps.Stop();
            ps.Play();
        }
    }
}
