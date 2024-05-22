using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInfo {
    // // 인스턴스 변수 선언
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
    // 정적 변수
    public string Name;
    public int Age;
    public string NpcDaily;
    public string Item;
    public string Hometown;
    public string PassPurpose;
    public bool IsVillain;

    // void OnResetNpcBtnDown() {
    //     // 인스턴스 변수의 값을 정적 변수에 할당
    //     Name = iname;
    //     Age = iage;
    //     NpcDaily = inpcDaily;
    //     Item = iitem;
    //     Hometown = ihometown;
    //     PassPurpose = ipassPurpose;
    // }
}

