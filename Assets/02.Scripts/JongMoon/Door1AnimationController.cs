using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1AnimationController : MonoBehaviour
{
    public Animator door1Animator;
    public DoorAnimationManager animationManager;

    public void AnimStart()
    {
        // �ִϸ��̼��� �����մϴ�.
        door1Animator.Play("door1open");
        Debug.Log("Door 1 animation started.");
    }

    // door1 �ִϸ��̼��� ���� �� ȣ��Ǵ� �Լ�
    public void OnDoor1AnimationEnd()
    {
        Debug.Log("Door 1 animation ended.");
        animationManager.SetDoor1Finished(true);
    }
}
