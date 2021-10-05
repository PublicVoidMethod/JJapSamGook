using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> ���� ���� �˻� �� ����(������ ����) </summary>
public static class InfiniteLoopDetector
{
	private static string prevPoint = "";
	private static int detectionCount = 0;
	private const int DetectionThreshold = 100000;

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void Run(
		[System.Runtime.CompilerServices.CallerMemberName] string mn = "",
		[System.Runtime.CompilerServices.CallerFilePath] string fp = "",
		[System.Runtime.CompilerServices.CallerLineNumber] int ln = 0
	)
	{
		string currentPoint = $"{fp}:{ln}, {mn}()";

		if (prevPoint == currentPoint)
			detectionCount++;
		else
			detectionCount = 0;

		if (detectionCount > DetectionThreshold)
			throw new Exception($"Infinite Loop Detected: \n{currentPoint}\n\n");

		prevPoint = currentPoint;
	}

#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoadMethod]
	private static void Init()
	{
		UnityEditor.EditorApplication.update += () =>
		{
			detectionCount = 0;
		};
	}
#endif
}
public class PlayerAttack2 : MonoBehaviour
{
	Animator anim;
	public PlayerMove pm;
	public string currentState = "Normal";
	float mAttackTime = 0;

	int loopNum = 0;

	IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMove>();
        while (Application.isPlaying)
        {
			loopNum++;
			InfiniteLoopDetector.Run(); // �̷��� �� �� �߰� �ۼ�
			if (loopNum++ > 10000)
				throw new Exception("Infinite Loop");
            yield return StartCoroutine(currentState);
		}
    }
    ////why?
    //public PlayerAttack2(Animator anim, PlayerMove pm, string currentState, float mAttackTime)
    //{
    //    if (string.IsNullOrEmpty(currentState))
    //    {
    //        throw new System.ArgumentException($"'{nameof(currentState)}'��(��) Null�̰ų� ��� �� �� �����ϴ�.", nameof(currentState));
    //    }

    //    this.anim = anim ?? throw new System.ArgumentNullException(nameof(anim));
    //    this.pm = pm ?? throw new System.ArgumentNullException(nameof(pm));
    //    this.currentState = currentState;
    //    this.mAttackTime = mAttackTime;
    //}

    IEnumerator Normal() //�� �ִϸ��̼�
	{
		pm.isAttack = false;
		pm.pState = PlayerMove.PlayerFSM.Normal;
		Debug.Log(pm.pState);
		//anim.Play("Normal"); // �� �ִϸ��̼�(����Ʈ) ���

		//Debug.Log("Run");

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			currentState = "Attack1"; //WŰ�� ������ Attack1�� �̵��ض�
			print(pm.pState);
			loopNum++;
			if (loopNum++ > 10000)
				throw new Exception("Infinite Loop");
			yield return null;

		}

	}


	IEnumerator Attack1()  //1�ܰ���
	{
		anim.SetTrigger("Attack01"); //1�� ���� �ִϸ��̼�
		mAttackTime = Time.time;  //���� �ð��� mAttackTime��

		while (anim.GetCurrentAnimatorStateInfo(3).IsName("Attack_01"))
		{
			loopNum++;
			if (Time.time - mAttackTime <= 1f) // ���� �ִϸ��̼� ��� �� 1�ʰ� ������ �ʾҴٸ�
			{
				if (Input.GetKeyDown(KeyCode.LeftControl))  // W(����)Ű �� �����°�
				{
					currentState = "Attack2";  //Attack2 ��..
					if (loopNum++ > 10000)
						throw new Exception("Infinite Loop");
					InfiniteLoopDetector.Run(); // �̷��� �� �� �߰� �ۼ�
					yield return null;
				}
			}
		}
		currentState = "Normal";  //�ٽ� ������
	}

	IEnumerator Attack2()  //2�ܰ���
	{
		anim.SetTrigger("Attack02"); //2�� ���� �ִϸ��̼�

		mAttackTime = Time.time;  //���� �ð��� mAttackTime��

		while (anim.GetCurrentAnimatorStateInfo(3).IsName("Attack_02"))
		{
			loopNum++;
			if (Time.time - mAttackTime <= 1f) // ���� �ִϸ��̼� ��� �� 1�ʰ� ������ �ʾҴٸ�
			{
				if (Input.GetKeyDown(KeyCode.LeftControl))  // W(����)Ű �� �����°�
				{
					currentState = "Attack_03";  //Attack03 ��..
					if (loopNum++ > 10000)
						throw new Exception("Infinite Loop");
					InfiniteLoopDetector.Run(); // �̷��� �� �� �߰� �ۼ�
					yield return null;
				}
			}
		}
		currentState = "Normal";  //�ٽ� ������
		
	}

	IEnumerator Attack3()  //2�ܰ���
	{
		anim.SetTrigger("Attack03"); //2�� ���� �ִϸ��̼�

		mAttackTime = Time.time;  //���� �ð��� mAttackTime��

		while (anim.GetCurrentAnimatorStateInfo(3).IsName("Attack_03"))
		{
			loopNum++;
			if (Time.time - mAttackTime <= 1f) // ���� �ִϸ��̼� ��� �� 1�ʰ� ������ �ʾҴٸ�
			{
				if (Input.GetKeyDown(KeyCode.LeftControl))  // W(����)Ű �� �����°�
				{
					currentState = "Attack_04";  //Attack04 ��..
					if (loopNum++ > 10000)
						throw new Exception("Infinite Loop");
					InfiniteLoopDetector.Run(); // �̷��� �� �� �߰� �ۼ�
					yield return null;
				}
			}
		}
		currentState = "Normal";  //�ٽ� ������

	}
	IEnumerator Attack4()  //3�ܰ���
	{
		anim.SetTrigger("Attack04"); //3�ܰ��� �ִϸ��̼� ���
		loopNum++;
		currentState = "Normal";  //�ִϸ��̼��� ���Ƹ� �ٽ� ������ ����
		if (loopNum++ > 10000)
			throw new Exception("Infinite Loop");
		InfiniteLoopDetector.Run(); // �̷��� �� �� �߰� �ۼ�
		yield return null;

    }
}
