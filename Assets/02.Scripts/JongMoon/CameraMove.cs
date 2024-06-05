using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public GameObject Camera;
    public Transform CameraPoint1;
    public Transform CameraPoint2;
    public float moveSpeed = 1.0f;
    public float waitTime = 2.0f;  // 기다리는 시간 (초)
    public SceneTransitionController sceneTransitionController;  // 씬 전환 컨트롤러

    private bool isMoving = false;
    private float journeyLength;
    private float startTime;

    void Start()
    {
        // 카메라를 CameraPoint1의 위치로 설정
        if (Camera != null && CameraPoint1 != null)
        {
            Camera.transform.position = CameraPoint1.position;
            Camera.transform.rotation = CameraPoint1.rotation;
            Debug.Log("Camera initialized at CameraPoint1 position.");
        }
        else
        {
            Debug.LogError("Camera 또는 CameraPoint1이 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        if (isMoving)
        {
            // Lerp를 사용하여 CameraPoint2의 위치로 서서히 이동
            if (Camera != null && CameraPoint2 != null)
            {
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                Camera.transform.position = Vector3.Lerp(CameraPoint1.position, CameraPoint2.position, fractionOfJourney);
                Camera.transform.rotation = Quaternion.Lerp(CameraPoint1.rotation, CameraPoint2.rotation, fractionOfJourney);

                Debug.Log($"Camera is moving. Current position: {Camera.transform.position}, fractionOfJourney: {fractionOfJourney}");

                // CameraPoint2에 도착했는지 확인
                if (fractionOfJourney >= 1.0f)
                {
                    isMoving = false;
                    StartSceneTransition();
                }
            }
        }
    }

    public void StartMoving()
    {
        StartCoroutine(MoveAfterWait());
    }

    private IEnumerator MoveAfterWait()
    {
        yield return new WaitForSeconds(waitTime);  // 기다리는 시간 동안 멈춤

        if (Camera != null && CameraPoint2 != null)
        {
            startTime = Time.time;
            journeyLength = Vector3.Distance(CameraPoint1.position, CameraPoint2.position);
            isMoving = true;
            Debug.Log("Camera started moving.");
        }
        else
        {
            Debug.LogError("Camera 또는 CameraPoint2가 설정되지 않았습니다.");
        }
    }

    private void StartSceneTransition()
    {
        if (sceneTransitionController != null)
        {
            sceneTransitionController.FadeOutAndLoadScene("Main_Stage1");
        }
        else
        {
            Debug.LogError("SceneTransitionController 인스턴스가 설정되지 않았습니다.");
        }
    }
}
