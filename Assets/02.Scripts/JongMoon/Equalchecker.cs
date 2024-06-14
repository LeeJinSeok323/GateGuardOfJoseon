using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equalchecker : MonoBehaviour
{
    private static string text1;
    private static string text2;
    public static string inputtext;
    public Text[] clickableTexts; // 클릭 가능한 텍스트 배열

    private static List<Text> highlightedTexts = new List<Text>();
    private static Color originalColor = Color.black; // 기본 색상 저장

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
                Debug.Log("일치합니다");
            }
            else
            {
                Debug.Log("일치하지 않습니다");
            }

            // 값 초기화 및 하이라이트 초기화
            ResetAllHighlights();
            text1 = null;
            text2 = null;
            inputtext = null;
        }
    }
}
