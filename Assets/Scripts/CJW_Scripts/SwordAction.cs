using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : MonoBehaviour
{
    public int attackPower = 10;
    public Animator anim;
    BoxCollider triggerBox;
    public Camera cam;
   public float camx = 0.1f;
   public float camy = 0.1f;
    Vector3 dir;
    public float ShakeTime = 0.01f;
    public float delayTime = 0.01f;
    float currentTime = 0;
    public EnemyMove enemy;
    public bool isThrow = false;
    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
        anim = GetComponentInParent<Animator>();

    }

    void Update()
    {
        dir = cam.transform.position;
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
            cameraShaking();
            if (anim.GetBool("SpecialSkill") == true)
            {
                enemy.eState = EnemyMove.EnemyState.ComboDamaged;

            }
            if (isThrow)
            {
                enemy.eState = EnemyMove.EnemyState.Throw;
            }
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

    void cameraShaking()
    {
        Invoke("cameraUpX", ShakeTime);
        Invoke("cameraUpY", ShakeTime+delayTime);
        Invoke("cameraDownX", ShakeTime + 2* delayTime);
        Invoke("cameraDownY", ShakeTime + 3 * delayTime);
        Invoke("cameraUpX", ShakeTime + 4 * delayTime);
        Invoke("cameraUpY", ShakeTime + 5 * delayTime);
        Invoke("cameraDownX", ShakeTime + 6 * delayTime);
        Invoke("cameraDownY", ShakeTime + 7 * delayTime);
    }
   
    void cameraUpX()
    {
        cam.transform.position = new Vector3(dir.x + camx, dir.y, dir.z);
    }
    void cameraUpY()
    {
        cam.transform.position = new Vector3(dir.x, dir.y + camy, dir.z);

    }
    void cameraDownX()
    {
        cam.transform.position = new Vector3(dir.x - camx, dir.y, dir.z);

    }
    void cameraDownY()
    {
        cam.transform.position = new Vector3(dir.x, dir.y - camy, dir.z);

    }
}
