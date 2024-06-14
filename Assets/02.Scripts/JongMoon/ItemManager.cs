using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public Npc npc; // NPC ��ũ��Ʈ ����

    void Start()
    {
        if (npc != null)
        {
            SetItems(npc.Item);
        }
    }

    void SetItems(string itemData)
    {
        // ������ ���ڿ��� ��ǥ�� �и��Ͽ� ���� ������ �̸��� ����
        string[] items = itemData.Split(',');

        // ������ GameObject ����Ʈ�� ����
        List<GameObject> itemObjects = new List<GameObject> { Item1, Item2, Item3 };

        // �� �������� �ݺ��ϸ� GameObject�� ����
        for (int i = 0; i < itemObjects.Count; i++)
        {
            if (i < items.Length && !string.IsNullOrEmpty(items[i].Trim()))
            {
                // ������ �̸��� �̹����� GameObject�� �����ϴ� �޼��带 ȣ��
                SetItemDetails(itemObjects[i], items[i].Trim());
                itemObjects[i].SetActive(true);
            }
            else
            {
                itemObjects[i].SetActive(false);
            }
        }
    }

    void SetItemDetails(GameObject itemObject, string itemName)
    {
        // ������ �̸��� �̹����� ����
        ItemDetails itemDetails = itemObject.GetComponent<ItemDetails>();
        if (itemDetails != null)
        {
            itemDetails.SetName(itemName);
            itemDetails.SetImage(itemName); // ������ �̸��� �´� �̹����� �����ϴ� �޼��� ȣ��
        }
    }
}
