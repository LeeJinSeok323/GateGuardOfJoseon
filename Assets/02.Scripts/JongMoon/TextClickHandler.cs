using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭된 텍스트를 전역 텍스트 관리자에 저장
        Equalchecker.inputtext = GetComponent<Text>().text;
    }
}
