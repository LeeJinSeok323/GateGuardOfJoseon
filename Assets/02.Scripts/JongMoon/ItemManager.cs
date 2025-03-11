using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    private Npc npc; // NPC 스크립트 참조
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

        // 아이템 문자열을 쉼표로 분리하여 개별 아이템 이름을 얻음
        string[] items = itemData.Split(',');

        // 아이템 GameObject 리스트를 생성
        List<GameObject> itemObjects = new List<GameObject> { Item1, Item2, Item3 };

        // 각 아이템을 반복하며 GameObject에 설정
        for (int i = 0; i < itemObjects.Count; i++)
        {
            if (i < items.Length && !string.IsNullOrEmpty(items[i].Trim()))
            {
                // 아이템 이름과 이미지를 GameObject에 설정하는 메서드를 호출
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
        // 아이템 이름과 이미지를 설정
        ItemDetails itemDetails = itemObject.GetComponent<ItemDetails>();
        if (itemDetails != null)
        {
            itemDetails.SetName(itemName);
            itemDetails.SetImage(itemName); // 아이템 이름에 맞는 이미지를 설정하는 메서드 호출
        }
    }
}
