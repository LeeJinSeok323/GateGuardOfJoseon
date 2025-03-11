using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Equalchecker : MonoBehaviour
{
    private static string text1;
    private static string text2;
    public static string inputtext;
    public Text[] clickableTexts; // Ŭ�� ������ �ؽ�Ʈ �迭
    public float blinkDuration = 0.25f;

    private static List<Text> highlightedTexts = new List<Text>();
    private static Color originalColor = Color.black; // �⺻ ���� ����
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

            // �� �ʱ�ȭ �� ���̶���Ʈ �ʱ�ȭ
            ResetAllHighlights();
            text1 = null;
            text2 = null;
            inputtext = null;
        }
    }
    
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

    

    IEnumerator RedBlinkText(Text targetText){
        targetText.color = blinkColor; // �����Ÿ� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = originalColor; // ���� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = blinkColor; // �����Ÿ� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = originalColor; // ���� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = blinkColor; // �����Ÿ� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = originalColor; // ���� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
    }
    
    IEnumerator GreenBlinkText(Text targetText){
        targetText.color = Color.green; // �����Ÿ� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = originalColor; // ���� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = Color.green; // �����Ÿ� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = originalColor; // ���� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = Color.green; // �����Ÿ� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
        targetText.color = originalColor; // ���� �������� ����
        yield return new WaitForSeconds(blinkDuration); // ������ �ð� ���
    }

}
