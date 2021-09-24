using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : MonoBehaviour
{
    public int attackPower = 10;

    BoxCollider triggerBox;

    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    
    }

    void Update()
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyMove enemy = other.GetComponent<EnemyMove>();

        if (enemy != null)
        {
            // 트리거 박스를 비활성화한다.
            //triggerBox.enabled = false;

            // 플레이어에게 자신의 공격력만큼의 데미지를 준다.
            enemy.OnHit(attackPower);
        }
    }
    // 트리거를 비활성화한다
   
}
