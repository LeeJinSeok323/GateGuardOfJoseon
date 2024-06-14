using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equalchecker : MonoBehaviour
{
    private static string text1;
    private static string text2;
    public static string inputtext;
    public Text[] clickableTexts; // Ŭ�� ������ �ؽ�Ʈ �迭

    private static List<Text> highlightedTexts = new List<Text>();
    private static Color originalColor = Color.black; // �⺻ ���� ����

    public static void HighlightText(Text text)
    {
        // �⺻ ������ ����
        originalColor = text.color;

        // ������ �����Ͽ� ���̶���Ʈ
        text.color = Color.yellow;

        // ���̶���Ʈ�� �ؽ�Ʈ�� ��Ͽ� �߰�
        highlightedTexts.Add(text);
    }

    public static void ResetHighlight(Text text)
    {
        // ������ ���� ������ �����Ͽ� ���̶���Ʈ �ʱ�ȭ
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
                Debug.Log("��ġ�մϴ�");
            }
            else
            {
                Debug.Log("��ġ���� �ʽ��ϴ�");
            }

            // �� �ʱ�ȭ �� ���̶���Ʈ �ʱ�ȭ
            ResetAllHighlights();
            text1 = null;
            text2 = null;
            inputtext = null;
        }
    }
}
