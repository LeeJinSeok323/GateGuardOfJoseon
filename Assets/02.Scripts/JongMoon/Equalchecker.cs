using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Equalchecker : MonoBehaviour
{
    private static string text1;
    private static string text2;
    public static string inputtext;
    public Text[] clickableTexts; // 클릭 가능한 텍스트 배열
    public float blinkDuration = 0.25f;

    private static List<Text> highlightedTexts = new List<Text>();
    private static Color originalColor = Color.black; // 기본 색상 저장
    private static Color blinkColor = Color.red;

    void Update()
    {
        if (text1 == null && inputtext != null)
        {
            text1 = inputtext;
            inputtext = null;
        }
        else if (text1 != null && text2 == null && inputtext != null)
        {
            text2 = inputtext;
            inputtext = null;
        }
        else if (text1 != null && text2 != null)
        {
            if (text1 == text2)
            {
                StartCoroutine(GreenBlinkText(highlightedTexts[0]));
                StartCoroutine(GreenBlinkText(highlightedTexts[1]));
            }
            else
            {
                StartCoroutine(RedBlinkText(highlightedTexts[0]));
                StartCoroutine(RedBlinkText(highlightedTexts[1]));
            }

            // 값 초기화 및 하이라이트 초기화
            ResetAllHighlights();
            text1 = null;
            text2 = null;
            inputtext = null;
        }
    }
    
    public static void HighlightText(Text text)
    {
        // 기본 색상을 저장
        originalColor = text.color;

        // 색상을 변경하여 하이라이트
        text.color = Color.yellow;

        // 하이라이트된 텍스트를 목록에 추가
        highlightedTexts.Add(text);
    }

    public static void ResetHighlight(Text text)
    {
        // 색상을 원래 색으로 변경하여 하이라이트 초기화
        text.color = originalColor;
    }

    void ResetAllHighlights()
    {
        foreach (Text text in highlightedTexts)
        {
            ResetHighlight(text);
        }
        highlightedTexts.Clear();
    }

    

    IEnumerator RedBlinkText(Text targetText){
        targetText.color = blinkColor; // 깜빡거림 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = originalColor; // 원래 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = blinkColor; // 깜빡거림 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = originalColor; // 원래 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = blinkColor; // 깜빡거림 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = originalColor; // 원래 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
    }
    
    IEnumerator GreenBlinkText(Text targetText){
        targetText.color = Color.green; // 깜빡거림 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = originalColor; // 원래 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = Color.green; // 깜빡거림 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = originalColor; // 원래 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = Color.green; // 깜빡거림 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
        targetText.color = originalColor; // 원래 색상으로 변경
        yield return new WaitForSeconds(blinkDuration); // 지정된 시간 대기
    }

}
