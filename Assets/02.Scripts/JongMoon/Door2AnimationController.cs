using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2AnimationController : MonoBehaviour
{
    public Animator door2Animator;
    public DoorAnimationManager animationManager;

    public void AnimStart()
    {
        // �ִϸ��̼��� �����մϴ�.
        door2Animator.Play("door2open");
        Debug.Log("Door 2 animation started.");
    }

    // door2 �ִϸ��̼��� ���� �� ȣ��Ǵ� �Լ�
    public void OnDoor2AnimationEnd()
    {
        Debug.Log("Door 2 animation ended.");
        animationManager.SetDoor2Finished(true);
    }
}