using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    public Text itemNameText;
    public Image itemImage;

    public void SetName(string name)
    {
        itemNameText.text = name;
    }

    public void SetImage(string name)
    {
        // Resources �������� ������ �̸��� �ش��ϴ� �̹����� �ҷ���
        Sprite itemSprite = Resources.Load<Sprite>($"Images/{name}");

        if (itemSprite != null)
        {
            itemImage.sprite = itemSprite;
        }
        else
        {
            Debug.LogWarning($"�̹����� ã�� �� �����ϴ�: {name}");
        }
    }
}
