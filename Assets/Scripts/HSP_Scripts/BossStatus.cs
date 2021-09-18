using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    public int bossMaxHP = 100;  // ������ �ִ�ü��

    int bossCurrentHP = 0;  // ������ ���� ü��

    void Start()
    {
        // ������ �� ������ ���� ü�¿� �ִ� ü���� �־��ְ� �����Ѵ�.
        bossCurrentHP = bossMaxHP;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �ǰ� ������ ���� �Լ�
    /// </summary>
    /// <param name="attackPower"></param>
    public void OnDamage(int attackPower)
    {
        // attackPower��ŭ ���� ü�¿��� ��´�
        bossCurrentHP -= attackPower;

        // ���� ü���� �������� ������ �ʵ��� �����Ѵ�.
        bossCurrentHP = Mathf.Max(bossCurrentHP, 0);
    }
}
