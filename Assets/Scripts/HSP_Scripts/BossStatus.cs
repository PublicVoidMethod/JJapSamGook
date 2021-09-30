using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
    public static BossStatus instance;  // ���� �������ͽ� ��ũ��Ʈ�� �̱������� �����
    
    public int bossMaxHP = 100;  // ������ �ִ�ü�� ����
    public int bossCurrentHP = 0;  // ������ ���� ü�� ����

    public Slider bossHPSlider;  // ���� ü�¹� �����̴� ����
    public GameObject bloodEffect;  // ������ ���� �� �ǰ� �ݶ��̴��� ���� ������Ʈ�� ��Ȱ��ȭ ��ų ����

    BossFSM bossFSM;
    Animator bossAnim;
    CapsuleCollider bossCollider;

    // ���� �������ͽ� ��ũ��Ʈ�� �̱��� ����
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        // ������ �� ������ ���� ü�¿� �ִ� ü���� �־��ְ� �����Ѵ�.
        bossCurrentHP = bossMaxHP;

        // ������ �ڽĿ� �پ��ִ� �ִϸ����� ������Ʈ�� �����´�.
        bossAnim = GetComponentInChildren<Animator>();

        // BossFSM ��ũ��Ʈ ������Ʈ�� �����´�.
        bossFSM = GetComponent<BossFSM>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �ǰ� ������ ���� �Լ�
    /// </summary>
    /// <param name="attackPower"></param>
    public void OnDamage(int bAttackPower)
    {

        // attackPower��ŭ ���� ü�¿��� ��´�
        bossCurrentHP -= bAttackPower;

        // ���� ü���� �������� ������ �ʵ��� �����Ѵ�.
        bossCurrentHP = Mathf.Max(bossCurrentHP, 0);

        // UI �����̴��� value���� ������ ���� ü���� �ִ´�.
        bossHPSlider.value = (float)bossCurrentHP / bossMaxHP;

        // �ǰ� �ִϸ��̼��� ȣ���Ѵ�.
        bossAnim.SetTrigger("OnHit");

        // ������ ���¸� Idle�� ��ȯ�Ѵ�.  ?????????????????????

        // ������ ���� ü���� 0���ϰ� �Ǹ�
        if(bossCurrentHP <= 0)
        {
            // ������ ĸ�� �ݶ��̴��� �����´�.
            bossCollider = GetComponent<CapsuleCollider>();

            // Die �ִϸ��̼��� ȣ���ϰ�
            bossAnim.SetTrigger("OnDie");

            // ������ �ݶ��̴��� false�� �����.
            bossCollider.enabled = false;

            // ��ũ��Ʈ�� ��Ȱ��ȭ ��Ų��.
            this.enabled = false;

            // �ǰ� ����Ʈ�� ��Ȱ��ȭ �Ѵ�.
            bloodEffect.SetActive(false);
        }
        // ������ ���� ü���� 30% ~50%�϶�
        else if ((float)bossCurrentHP / (float)bossMaxHP > 0.3 && (float)bossCurrentHP / (float)bossMaxHP < 0.5)
        {
            print("�� ��ų�� ������");
            bossFSM.bState = BossFSM.BossState.Skill;
        }
    }
}
