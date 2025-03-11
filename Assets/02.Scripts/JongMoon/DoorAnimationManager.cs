using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationManager : MonoBehaviour
{
    public CameraMove cameraMove;
    private bool door1Finished = false;
    private bool door2Finished = false;

    public void SetDoor1Finished(bool isFinished)
    {
        door1Finished = isFinished;
        CheckAnimationsEnd();
    }

    public void SetDoor2Finished(bool isFinished)
    {
        door2Finished = isFinished;
        CheckAnimationsEnd();
    }

    private void CheckAnimationsEnd()
    {
        Debug.Log($"Checking animations end. door1Finished: {door1Finished}, door2Finished: {door2Finished}");
        if (door1Finished && door2Finished)
        {
            Debug.Log("Both animations ended, Call OnBothAnimationsEnd.");
            OnBothAnimationsEnd();
        }
    }

    private void OnBothAnimationsEnd()
    {
        Debug.Log("Both animations ended. Starting camera move.");
        if (cameraMove != null)
        {
            cameraMove.StartMoving();
        }
        else
        {
            Debug.LogError("CameraMove 인스턴스가 설정되지 않았습니다.");
        }
    }
}
