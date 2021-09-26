using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    // 0.5�� �� ����Ʈ�� �ı�
    float currentTime = 0;
    float elapsedTime = 0.5f;

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > elapsedTime)
        {
            Destroy(gameObject);
        }
    }
}
