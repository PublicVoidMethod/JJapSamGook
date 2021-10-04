using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public float bMoveSpeed = 7.0f;  // ������ �̵��ӵ� ����
    public float sightDistance = 10.0f;  // ���� �þ��� ����
    public float sightDegree = 30.0f;  // ������ �þ� ������ ũ��
    public float attackRange = 2.0f;  // ������ ��Ÿ ���� ����

    public GameObject bossHPBar;  // ���� HP�ٸ� SetActive �� �� �ְ� ���� ����
    public GameObject[] bossSkillEffect = new GameObject[3];  // ���� ��ų ����Ʈ
    public GameObject bossSword;  // ������ ���� ������ �� ����

    Transform player;  // "Player" �±׸� ã������ ����
    Rigidbody bossRB;  // ������ ������ �ٵ� ����
    Animator bossAnim;  // ������ �ִϸ����� ����
    BossSkillSensor bossSkill;  // BossSkillSensor ��ũ��Ʈ�� ������ ����
    GameObject bsc;  // BossSkillCollider�� ���� ������Ʈ�� ���� ����
    CapsuleCollider capsulcollier;
    //ParticleSystem bossSkillParticle;  // ��ƼŬ �ý����� ���� ����

    bool isbooked = false;
    bool attackmotion = false;  // ������ ���� ���·� ��ȯ�Ǵ� ����. true(���ݻ���) false(����ݻ���)
    bool isSkillCollider = false;  // ������ ��ų�� �ݶ��̴��� �������� �������� üũ�ϴ� ����.

    // ������ ������ ���
    public enum BossState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Damaged,
        Die
    }
    public BossState bState;

    void Start()
    {
        // ������ �ٵ� ĳ��
        bossRB = GetComponent<Rigidbody>();

        // ������ ���´� Idle(���) ���·� �����Ѵ�.
        bState = BossState.Idle;

        // Player��� �±׸� ã�´�.
        player = GameObject.FindWithTag("Player").transform;

        capsulcollier = GetComponent<CapsuleCollider>();

        // �ڽ� ������Ʈ�κ��� Animator ������Ʈ�� �����´�.
        bossAnim = GetComponentInChildren<Animator>();

        // BossSkillSensor ��ũ��Ʈ�� �����´�.
        bossSkill = GetComponentInChildren<BossSkillSensor>();

        // �ڽ����� �ִ� BossSkillCollider�� ��Ȱ��ȭ ��Ų��.
        bsc = transform.Find("BossSkillColliders").gameObject;
        bsc.SetActive(false);
    }

    void Update()
    {
        switch (bState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Skill:
                Skill();
                break;
            case BossState.Damaged:
                Damaged();
                break;
            default:
                break;
        }

        // ������ ���� ���͸� �÷��̾ ���ϵ��� ȸ���Ѵ�.
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir);

        // ������ ü���� 0�� �Ǹ�
        if(BossStatus.instance.bossCurrentHP <= 0)
        {
            // ��ũ��Ʈ�� ��Ȱ��ȭ ��Ų��.
            this.enabled = false;
            capsulcollier.isTrigger = true;
        }
    }

    private void Idle()
    {
        // �÷��̾��� �����ǰ� ��(����)�� �������� �Ÿ��� �þ� �������� �۴ٸ�
        // (=�÷��̾ ������ �þ� ������ ���ٸ�)
        if (Vector3.Distance(player.position, transform.position) < sightDistance)
        {
            Vector3 lookPlayer = (player.position - transform.position).normalized;  // ???????????????? why noramlized?

            // �� ���͸� ����(���� ����, ���� ����)
            float cosValue = Vector3.Dot(transform.forward, lookPlayer);

            // ������ ����� ������(��, �÷��̾ �տ� �ִٸ�)
            if (cosValue > 0)
            {
                // ���� ���� ���Ϳ� �÷��̾ �ٶ󺸴� ���Ϳ��� ���հ��� ���Ѵ�.  ??????????????????????
                float degree = Mathf.Acos(cosValue) * Mathf.Rad2Deg;

                // ���� ������ �þ߰� ������ ���Դٸ�
                if (degree < sightDegree)
                {
                    bossHPBar.SetActive(true);

                    //// ������ ���� ���͸� �÷��̾ ���ϵ��� ȸ���Ѵ�.
                    //Vector3 dir = (player.position - transform.position).normalized;
                    //transform.rotation = Quaternion.LookRotation(dir);

                    SetMoveState();
                }
            }
        }
    }

    private void Move()
    {
        // ����(����) �÷��̾��� ������ ������
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        dir.Normalize();

        // �����̰� �ʹ�.
        transform.position += dir * bMoveSpeed * Time.deltaTime;

        // ��(����)�� �÷��̾��� �Ÿ��� ��Ÿ ���� ���� �ȿ� ���Դٸ�(��Ÿ ���� ���� > ���� �÷��̾��� �Ÿ�)
        //if (attackRange > Vector3.Distance(player.position, transform.position))
        if (attackRange > (player.position - transform.position).magnitude)
        {
            // ���� �ִϸ��̼� ȣ��
            bossAnim.SetTrigger("RunToAttack");
            print("������");
            // �̵��� ���߰�
            bossRB.velocity = Vector3.zero;
            
            // ���� ���·� ��ȯ�Ѵ�.
            bState = BossState.Attack;

            //// ���� �ִϸ��̼��� �����Ѵ�.  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //bossAnim.SetTrigger("MoveToAttack");  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        }
    }

    private void Attack()
    {
        if (attackmotion == false)
        {
            // ���� ��(����)�� �÷��̾���� �Ÿ��� ��Ÿ ���� �������� �־�����(��Ÿ ���� ���� < ���� �÷��̾��� �Ÿ�)
            if (attackRange < (player.position - transform.position).magnitude)
            {
                bossAnim.SetTrigger("AttackToIdle");
                if (!isbooked)
                {
                    // Move���·� 1.5�� �Ŀ� ��ȯ�Ѵ�.
                    Invoke("SetMoveState", 0.5f);
                    //return;

                    //bossAnim.SetTrigger("IdleToMove");
                }
            }
            else
            {
                AttackPattern();
            }
        }
    }

    private void Skill()
    {
        if(isSkillCollider == false)
        {
            isSkillCollider = true;
            // ��ų ������ �����ϴ� �ִϸ��̼��� �����ϰ�
            bossAnim.SetTrigger("OnSkill");

            //StartCoroutine(AnimStopPlay()); @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            //// ������ �� ���� �ٴ��� ���ϵ��� ȸ���Ѵ�.
            //bossSword.transform.Rotate(102, 0, 0);

            // n�� �� ��ƼŬ�� �����Ѵ�.
            StartCoroutine(BossSkillEffect());
        }
        bState = BossState.Idle;
    }

    private void Damaged()
    {

    }

    void SetMoveState()
    {
        // ������ ���¸� Move ���·� ��ȯ�Ѵ�.
        bState = BossState.Move;

        // �̵� �ִϸ��̼��� �����Ѵ�.
        bossAnim.SetTrigger("IdleToMove");

        isbooked = false;
    }

    /// <summary>
    /// ������ �þ߰��� ���� �Լ�
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    Vector3[] BossViewingAngle(float radius, float angle)
    {
        Vector3[] results = new Vector3[2];

        // ���� �� ���� ��ǥ�� ���Ѵ�.
        float theta = 90 - angle - transform.eulerAngles.y;
        float posX = Mathf.Cos(theta * Mathf.Deg2Rad) * radius;
        float posY = transform.position.y;
        float posZ = Mathf.Sin(theta * Mathf.Deg2Rad) * radius;
        results[0] = new Vector3(posX, posY, posZ);

        // ���� �� ���� ��ǥ�� ���Ѵ�.
        theta = 90 + angle - transform.eulerAngles.y;
        posX = Mathf.Cos(theta * Mathf.Deg2Rad) * radius;
        posY = transform.position.y;
        posZ = Mathf.Sin(theta * Mathf.Deg2Rad) * radius;
        results[1] = new Vector3(posX, posY, posZ);

        return results;
    }

    /// <summary>
    /// �þ߰��� �� ǥ���Ǿ� �ִ��� ������ Ȯ��
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3[] sightPos = BossViewingAngle(sightDistance, sightDegree);

        for(int i = 0; i < sightPos.Length; i++)
        {
            Gizmos.DrawLine(transform.position, transform.position + sightPos[i]);
        }
    }

    /// <summary>
    /// ������ ���� ������ �������� ���� ��Ű�� �Լ�
    /// </summary>
    void AttackPattern()
    {
        int randomAttack = UnityEngine.Random.Range(0, 4);
        switch (randomAttack)
        {
            case 0:
                // ���� ���� 0
                StartCoroutine(Attack_00());
                break;
            case 1:
                // ���� ���� 1
                StartCoroutine(Attack_01());
                break;
            case 2:
                // ���� ���� 2
                StartCoroutine(Attack_02());
                break;
            case 3:
                // ���� ���� 3
                StartCoroutine(Attack_03());
                break;
        }
    }

    IEnumerator Attack_00()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_00");
        yield return new WaitForSeconds(2.15f);
        attackmotion = false;
    }

    IEnumerator Attack_01()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_01");
        yield return new WaitForSeconds(3.40f);
        attackmotion = false;
    }

    IEnumerator Attack_02()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_02");
        yield return new WaitForSeconds(3.0f);
        attackmotion = false;
    }

    IEnumerator Attack_03()
    {
        attackmotion = true;
        bossAnim.SetTrigger("Pattern_03");
        yield return new WaitForSeconds(3.8f);
        attackmotion = false;
    }

    IEnumerator BossSkillEffect()
    {
        // �ڽ� �ݶ��̴��� true���°� �Ǵ� �ð��� ���� ���ٵ�
        yield return new WaitForSeconds(2f);
        // ��ų �ִϸ��̼� ������ �ϴ� n�� �Ŀ� BossSkillSensor�� BoxCollider�� Ȱ��ȭ ��Ų��.
        bsc.SetActive(true);

        // ��ų ����Ʈ�� Ȱ��ȭ�ϰ�
        for(int i = 0; i < bossSkillEffect.Length; i++)
        {
            bossSkillEffect[i].SetActive(true);
            ParticleSystem particle = bossSkillEffect[i].GetComponentInChildren<ParticleSystem>();
            if(particle != null)
            {
                particle.Stop();
                particle.Play();
            }
        }
        // ��ƼŬ�� �����Ѵ�.
        // bossSkillParticle.Stop();
        // bossSkillParticle.Play();
        // ��ų �ִϸ��̼��� ����ǰ� ��ų ��ƼŬ�� ������ �������� ������ �ð�
    }

    //IEnumerator AnimStopPlay()  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    //{
    //    // ��ų �ִϸ��̼��� �÷��� �ϰ�
    //    bossAnim.SetTrigger("OnSkill");

    //    // 1.04�ʸ� ��ٸ� ��
    //    yield return new WaitForSeconds(1f);

    //    // �ִϸ��̼��� ������Ų��.
    //    bossAnim.StopPlayback();

    //    yield return new WaitForSeconds(1f);
    //    bossAnim.StartPlayback();
    //}
}
