using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAction_SSW_1 : MonoBehaviour
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
        PlayerMove_1 player = other.GetComponent<PlayerMove_1>();

        if (player != null && attackDamage !=0)
        {
            print(attackDamage);
            // �÷��̾�� �ڽ��� ���ݷ¸�ŭ�� �������� �ο��Ѵ�.
            player.DamageProcess(attackDamage);
        }
    }

    //
}
