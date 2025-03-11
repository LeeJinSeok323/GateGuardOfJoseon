using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    private Npc npc; // NPC ��ũ��Ʈ ����
    private Transform Player;

    private static ItemManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnItemBoutton()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        npc = NpcManager.Instance.CheckRadiusNPCObject(Player.position);

        if (npc == null) return;
        SetItems(npc.Item);
    }

    void SetItems(string itemData)
    {
        Debug.Log(itemData);

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

        if (items.Length == 5) //5 //3
        {
            SetItemDetails(itemObjects[0], items[3].Trim());
            SetItemDetails(itemObjects[2], items[4].Trim());
        }
        else if(items.Length == 4)
        {
            SetItemDetails(itemObjects[2], items[3].Trim());
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
