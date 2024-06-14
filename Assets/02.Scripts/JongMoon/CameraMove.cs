using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public GameObject Camera;
    public Transform CameraPoint1;
    public Transform CameraPoint2;
    public float moveSpeed = 1.0f;
    public float waitTime = 2.0f;  // ��ٸ��� �ð� (��)
    public SceneTransitionController sceneTransitionController;  // �� ��ȯ ��Ʈ�ѷ�

    private bool isMoving = false;
    private float journeyLength;
    private float startTime;

    void Start()
    {
        // ī�޶� CameraPoint1�� ��ġ�� ����
        if (Camera != null && CameraPoint1 != null)
        {
            Camera.transform.position = CameraPoint1.position;
            Camera.transform.rotation = CameraPoint1.rotation;
            Debug.Log("Camera initialized at CameraPoint1 position.");
        }
        else
        {
            Debug.LogError("Camera �Ǵ� CameraPoint1�� �������� �ʾҽ��ϴ�.");
        }
    }

    void Update()
    {
        if (isMoving)
        {
            // Lerp�� ����Ͽ� CameraPoint2�� ��ġ�� ������ �̵�
            if (Camera != null && CameraPoint2 != null)
            {
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                Camera.transform.position = Vector3.Lerp(CameraPoint1.position, CameraPoint2.position, fractionOfJourney);
                Camera.transform.rotation = Quaternion.Lerp(CameraPoint1.rotation, CameraPoint2.rotation, fractionOfJourney);

                Debug.Log($"Camera is moving. Current position: {Camera.transform.position}, fractionOfJourney: {fractionOfJourney}");

                // CameraPoint2�� �����ߴ��� Ȯ��
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
        yield return new WaitForSeconds(waitTime);  // ��ٸ��� �ð� ���� ����

        if (Camera != null && CameraPoint2 != null)
        {
            startTime = Time.time;
            journeyLength = Vector3.Distance(CameraPoint1.position, CameraPoint2.position);
            isMoving = true;
            Debug.Log("Camera started moving.");
        }
        else
        {
            Debug.LogError("Camera �Ǵ� CameraPoint2�� �������� �ʾҽ��ϴ�.");
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
            Debug.LogError("SceneTransitionController �ν��Ͻ��� �������� �ʾҽ��ϴ�.");
        }
    }
}
