using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2AnimationController : MonoBehaviour
{
    public Animator door2Animator;
    public DoorAnimationManager animationManager;

    public void AnimStart()
    {
        // 애니메이션을 시작합니다.
        door2Animator.Play("door2open");
        Debug.Log("Door 2 animation started.");
    }

    // door2 애니메이션이 끝날 때 호출되는 함수
    public void OnDoor2AnimationEnd()
    {
        Debug.Log("Door 2 animation ended.");
        animationManager.SetDoor2Finished(true);
    }
}