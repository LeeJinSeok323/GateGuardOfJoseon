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
        // Resources 폴더에서 아이템 이름에 해당하는 이미지를 불러옴
        Sprite itemSprite = Resources.Load<Sprite>($"Images/{name}");

        if (itemSprite != null)
        {
            itemImage.sprite = itemSprite;
        }
        else
        {
            Debug.LogWarning($"이미지를 찾을 수 없습니다: {name}");
        }
    }
}
