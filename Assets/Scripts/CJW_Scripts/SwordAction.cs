using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : MonoBehaviour
{
    public int attackPower = 10;
    public Animator anim;
    BoxCollider triggerBox;
    
    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
        anim = GetComponentInParent<Animator>();

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

      
        if (other.gameObject.tag == "Sword")
        {
            print("Į���� �¾Ҵ�");
            // Ʈ���� �ڽ��� ��Ȱ��ȭ�Ѵ�.
            //triggerBox.enabled = false;

            // �÷��̾�� �ڽ��� ���ݷ¸�ŭ�� �������� �ش�.
            anim.SetTrigger("Parry");
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            BossStatus.instance.OnDamage(attackPower);
        }
    }

   
}
