using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAction_SSW : MonoBehaviour
{

    public int attackDamage = 10;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMove player = other.GetComponent<PlayerMove>();

        if (player != null && attackDamage !=0)
        {
            print(attackDamage);
            // �÷��̾�� �ڽ��� ���ݷ¸�ŭ�� �������� �ο��Ѵ�.
            player.DamageProcess(attackDamage);
        }
    }
}
