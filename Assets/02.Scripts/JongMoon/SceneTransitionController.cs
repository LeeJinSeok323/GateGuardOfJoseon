using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionController : MonoBehaviour
{
    public Image transitionImage;  // UI 캔버스에 있는 Image 컴포넌트
    public float fadeDuration = 1.0f;  // 페이드 아웃 지속 시간

    private void Start()
    {
        if (transitionImage != null)
        {
            // 처음에 화면을 투명하게 설정
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
        // 페이드 아웃 (화면 어두워짐)
        yield return StartCoroutine(FadeOut());

        // 씬 로드
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