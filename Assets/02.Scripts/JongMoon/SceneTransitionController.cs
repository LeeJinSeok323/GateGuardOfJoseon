using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionController : MonoBehaviour
{
    public Image transitionImage;  // UI ĵ������ �ִ� Image ������Ʈ
    public float fadeDuration = 1.0f;  // ���̵� �ƿ� ���� �ð�

    private void Start()
    {
        if (transitionImage != null)
        {
            // ó���� ȭ���� �����ϰ� ����
            transitionImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            Debug.LogError("Transition Image is not set.");
        }
    }

    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutRoutine(sceneName));
    }

    private IEnumerator FadeOutRoutine(string sceneName)
    {
        // ���̵� �ƿ� (ȭ�� ��ο���)
        yield return StartCoroutine(FadeOut());

        // �� �ε�
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            transitionImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}