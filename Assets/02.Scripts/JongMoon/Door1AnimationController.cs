using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1AnimationController : MonoBehaviour
{
    public Animator door1Animator;
    public DoorAnimationManager animationManager;

    public void AnimStart()
    {
        // 애니메이션을 시작합니다.
        door1Animator.Play("door1open");
        Debug.Log("Door 1 animation started.");
    }

    // door1 애니메이션이 끝날 때 호출되는 함수
    public void OnDoor1AnimationEnd()
    {
        Debug.Log("Door 1 animation ended.");
        animationManager.SetDoor1Finished(true);
    }
}
