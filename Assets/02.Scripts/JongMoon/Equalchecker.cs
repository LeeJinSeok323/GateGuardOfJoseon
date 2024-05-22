using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 이벤트 처리를 위해 필요

public class Equalchecker : MonoBehaviour
{
    private static string text1;
    private static string text2;
    public static string inputtext;

    public Text[] clickableTexts; // 클릭 가능한 텍스트 배열

    void Start()
    {
        // 각 텍스트에 클릭 이벤트 리스너 추가
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
                Debug.Log("일치합니다");
            }
            else
            {
                Debug.Log("일치하지 않습니다");
            }

            // 값 초기화
            text1 = null;
            text2 = null;
            inputtext = null;
        }
    }
}
