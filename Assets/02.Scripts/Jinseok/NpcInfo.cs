using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInfo {
    // // �ν��Ͻ� ���� ����
    // [SerializeField] private string iname;
    // [SerializeField] private int iage;
    // [SerializeField] private string inpcDaily;
    // [SerializeField] private string iitem;
    // [SerializeField] private string ihometown;
    // [SerializeField] private string ipassPurpose;

    NpcInfo(string name, int age, string npcDaily, string item, string hometown, string passPurpose, bool isVillain ){
        Name = name;
        Age = age;
        NpcDaily = npcDaily;
        Item = item;
        Hometown = hometown;
        PassPurpose = passPurpose;
        IsVillain = isVillain;
    }
    // ���� ����
    public string Name;
    public int Age;
    public string NpcDaily;
    public string Item;
    public string Hometown;
    public string PassPurpose;
    public bool IsVillain;

    // void OnResetNpcBtnDown() {
    //     // �ν��Ͻ� ������ ���� ���� ������ �Ҵ�
    //     Name = iname;
    //     Age = iage;
    //     NpcDaily = inpcDaily;
    //     Item = iitem;
    //     Hometown = ihometown;
    //     PassPurpose = ipassPurpose;
    // }
}

