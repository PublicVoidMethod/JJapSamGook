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
            // 플레이어에게 자신의 공격력만큼의 데미지를 부여한다.
            player.DamageProcess(attackDamage);
        }
    }
}
