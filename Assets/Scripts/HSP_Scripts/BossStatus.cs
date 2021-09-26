using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
    public static BossStatus instance;  // 보스 스테이터스 스크립트를 싱글턴으로 만든다
    
    public int bossMaxHP = 100;  // 보스의 최대체력 변수

    public Slider bossHPSlider;  // 보스 체력바 슬라이더 변수

    [SerializeField]
    int bossCurrentHP = 0;  // 보스의 현재 체력 변수

    BossFSM bossFSM;
    Animator bossAnim;
    CapsuleCollider bossCollider;

    // 보스 스테이터스 스크립트의 싱글턴 패턴
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
        // 시작할 때 보스의 현재 체력에 최대 체력을 넣어주고 시작한다.
        bossCurrentHP = bossMaxHP;

        // 보스의 자식에 붙어있는 애니메이터 컴포넌트를 가져온다.
        bossAnim = GetComponentInChildren<Animator>();

        // BossFSM 스크립트 컴포넌트를 가져온다.
        bossFSM = GetComponent<BossFSM>();
    }

    void Update()
    {

    }

    /// <summary>
    /// 피격 당했을 때의 함수
    /// </summary>
    /// <param name="attackPower"></param>
    public void OnDamage(int bAttackPower)
    {

        // attackPower만큼 현재 체력에서 깍는다
        bossCurrentHP -= bAttackPower;

        // 현재 체력이 음수값이 나오지 않도록 제한한다.
        bossCurrentHP = Mathf.Max(bossCurrentHP, 0);

        // UI 슬라이더의 value값에 보스의 현재 체력을 넣는다.
        bossHPSlider.value = (float)bossCurrentHP / bossMaxHP;

        // 피격 애니메이션을 호출한다.
        bossAnim.SetTrigger("OnHit");

        // 보스의 상태를 Idle로 전환한다.  ?????????????????????

        // 보스의 현재 체력이 0이하가 되면
        if(bossCurrentHP <= 0)
        {
            // 보스의 캡슐 콜라이더를 가져온다.
            bossCollider = GetComponent<CapsuleCollider>();

            // Die 애니메이션을 호출하고
            bossAnim.SetTrigger("OnDie");

            // 보스의 콜라이더를 false로 만든다.
            bossCollider.enabled = false;
        }
    }
}
