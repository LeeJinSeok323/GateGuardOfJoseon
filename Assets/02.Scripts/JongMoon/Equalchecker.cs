using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // �̺�Ʈ ó���� ���� �ʿ�

public class Equalchecker : MonoBehaviour
{
    private static string text1;
    private static string text2;
    public static string inputtext;

    public Text[] clickableTexts; // Ŭ�� ������ �ؽ�Ʈ �迭

    void Start()
    {
        // �� �ؽ�Ʈ�� Ŭ�� �̺�Ʈ ������ �߰�
        foreach (Text text in clickableTexts)
        {
            EventTrigger trigger = text.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnTextClick((PointerEventData)data, text); });
            trigger.triggers.Add(entry);
        }
    }

    public void OnTextClick(PointerEventData data, Text clickedText)
    {
        if (inputtext == null)
        {
            inputtext = clickedText.text;
        }
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

            // �� �ʱ�ȭ
            text1 = null;
            text2 = null;
            inputtext = null;
        }
    }
}
