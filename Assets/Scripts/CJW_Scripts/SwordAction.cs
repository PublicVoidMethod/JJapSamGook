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
            // Ʈ���� �ڽ��� ��Ȱ��ȭ�Ѵ�.
            //triggerBox.enabled = false;

            // �÷��̾�� �ڽ��� ���ݷ¸�ŭ�� �������� �ش�.
            enemy.OnHit(attackPower);
        }

        if (other.gameObject.CompareTag("Boss"))  // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            BossStatus.instance.OnDamage(attackPower);  // @@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }
    }

   
}
