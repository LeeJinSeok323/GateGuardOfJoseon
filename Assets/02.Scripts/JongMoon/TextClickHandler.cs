using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ŭ���� �ؽ�Ʈ�� ���� �ؽ�Ʈ �����ڿ� ����
        Equalchecker.inputtext = GetComponent<Text>().text;
    }
}
