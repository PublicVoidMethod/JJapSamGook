using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 무한 루프 검사 및 방지(에디터 전용) </summary>
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
			InfiniteLoopDetector.Run(); // 이렇게 한 줄 추가 작성
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
    //        throw new System.ArgumentException($"'{nameof(currentState)}'은(는) Null이거나 비워 둘 수 없습니다.", nameof(currentState));
    //    }

    //    this.anim = anim ?? throw new System.ArgumentNullException(nameof(anim));
    //    this.pm = pm ?? throw new System.ArgumentNullException(nameof(pm));
    //    this.currentState = currentState;
    //    this.mAttackTime = mAttackTime;
    //}

    IEnumerator Normal() //런 애니메이션
	{
		pm.isAttack = false;
		pm.pState = PlayerMove.PlayerFSM.Normal;
		Debug.Log(pm.pState);
		//anim.Play("Normal"); // 런 애니메이션(디폴트) 재생

		//Debug.Log("Run");

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			currentState = "Attack1"; //W키를 누르면 Attack1로 이동해라
			print(pm.pState);
			loopNum++;
			if (loopNum++ > 10000)
				throw new Exception("Infinite Loop");
			yield return null;

		}

	}


	IEnumerator Attack1()  //1단공격
	{
		anim.SetTrigger("Attack01"); //1단 공격 애니메이션
		mAttackTime = Time.time;  //현재 시간을 mAttackTime에

		while (anim.GetCurrentAnimatorStateInfo(3).IsName("Attack_01"))
		{
			loopNum++;
			if (Time.time - mAttackTime <= 1f) // 공격 애니메이션 재생 후 1초가 지나지 않았다면
			{
				if (Input.GetKeyDown(KeyCode.LeftControl))  // W(공격)키 를 눌렀는가
				{
					currentState = "Attack2";  //Attack2 로..
					if (loopNum++ > 10000)
						throw new Exception("Infinite Loop");
					InfiniteLoopDetector.Run(); // 이렇게 한 줄 추가 작성
					yield return null;
				}
			}
		}
		currentState = "Normal";  //다시 런으로
	}

	IEnumerator Attack2()  //2단공격
	{
		anim.SetTrigger("Attack02"); //2단 공격 애니메이션

		mAttackTime = Time.time;  //현재 시간을 mAttackTime에

		while (anim.GetCurrentAnimatorStateInfo(3).IsName("Attack_02"))
		{
			loopNum++;
			if (Time.time - mAttackTime <= 1f) // 공격 애니메이션 재생 후 1초가 지나지 않았다면
			{
				if (Input.GetKeyDown(KeyCode.LeftControl))  // W(공격)키 를 눌렀는가
				{
					currentState = "Attack_03";  //Attack03 로..
					if (loopNum++ > 10000)
						throw new Exception("Infinite Loop");
					InfiniteLoopDetector.Run(); // 이렇게 한 줄 추가 작성
					yield return null;
				}
			}
		}
		currentState = "Normal";  //다시 런으로
		
	}

	IEnumerator Attack3()  //2단공격
	{
		anim.SetTrigger("Attack03"); //2단 공격 애니메이션

		mAttackTime = Time.time;  //현재 시간을 mAttackTime에

		while (anim.GetCurrentAnimatorStateInfo(3).IsName("Attack_03"))
		{
			loopNum++;
			if (Time.time - mAttackTime <= 1f) // 공격 애니메이션 재생 후 1초가 지나지 않았다면
			{
				if (Input.GetKeyDown(KeyCode.LeftControl))  // W(공격)키 를 눌렀는가
				{
					currentState = "Attack_04";  //Attack04 로..
					if (loopNum++ > 10000)
						throw new Exception("Infinite Loop");
					InfiniteLoopDetector.Run(); // 이렇게 한 줄 추가 작성
					yield return null;
				}
			}
		}
		currentState = "Normal";  //다시 런으로

	}
	IEnumerator Attack4()  //3단공격
	{
		anim.SetTrigger("Attack04"); //3단공격 애니메이션 재생
		loopNum++;
		currentState = "Normal";  //애니메이션이 끝아면 다시 런으로 복귀
		if (loopNum++ > 10000)
			throw new Exception("Infinite Loop");
		InfiniteLoopDetector.Run(); // 이렇게 한 줄 추가 작성
		yield return null;

    }
}
