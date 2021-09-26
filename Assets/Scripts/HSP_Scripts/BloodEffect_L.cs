using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect_L : MonoBehaviour
{
    public GameObject bloodEffect_L;  // �� �긮�� ��ƼŬ�� �־��� ����

    ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        // ���� �ε��� ����� �̸��� AttackRange�� �����ϰ� �ִٸ�
        if (other.gameObject.name.Contains("AttackRange"))
        {
            // ���긮�� ����Ʈ�� �����ϰ�
            GameObject go = Instantiate(bloodEffect_L);
            // ����Ʈ�� ��ġ�� �ݶ��̴��� �ε��� ������ �����ϰ�
            go.transform.position = other.transform.position;
            // ����Ʈ�� ȸ���Ѵ�.
            go.transform.Rotate(0, 90, 0);

            // ��ƼŬ�� �����Ѵ�.
            ps = go.GetComponent<ParticleSystem>();
            ps.Stop();
            ps.Play();
        }
    }
}
