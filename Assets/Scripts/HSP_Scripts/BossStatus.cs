using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    public int bossMaxHP = 100;  // 보스의 최대체력

    int bossCurrentHP = 0;  // 보스의 현재 체력

    void Start()
    {
        // 시작할 때 보스의 현재 체력에 최대 체력을 넣어주고 시작한다.
        bossCurrentHP = bossMaxHP;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 피격 당했을 때의 함수
    /// </summary>
    /// <param name="attackPower"></param>
    public void OnDamage(int attackPower)
    {
        // attackPower만큼 현재 체력에서 깍는다
        bossCurrentHP -= attackPower;

        // 현재 체력이 음수값이 나오지 않도록 제한한다.
        bossCurrentHP = Mathf.Max(bossCurrentHP, 0);
    }
}
