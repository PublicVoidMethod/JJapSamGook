using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillSensor : MonoBehaviour
{
    public BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BossStatus.instance.bossAttackPower = BossStatus.instance.bossAttackPower * 3;
            //print(BossStatus.instance.bossAttackPower);
        }
        else if(other.gameObject == null)
        {
            // 블릿타임 실행
        }
    }
}
