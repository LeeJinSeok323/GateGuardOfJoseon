using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NpcInfoGenerater : MonoBehaviour
{
    // CSV�� �ҷ��� �����͵� ����.   
    private List<string> nameList;
    private List<string> ageList;
    private List<string> homeList;
    private List<string> styleList;
    //private List<string> List;
  //  private List<string> nameList;

    // �������� ������ npc ���̺� 2���� ����Ʈ�� �ϸ� �����ҰŰ��Ƽ� 1�������� ������.
    // �ҷ��ö��� npc id�� ����Ʈ �ε����� ���
    private List<string> nameTable;
    private List<string> ageTable;
    private List<string> homeTable;
    private List<string> styleTable;
    private List<string> List;
    private List<string> a;

    void Start(){
        nameList = CSVLineReader.GetColumn("NPCTable", "Name");
        ageList = CSVLineReader.GetColumn("NPCTable", "Age");
        homeList = CSVLineReader.GetColumn("NPCTable", "Hometown");
        styleList = CSVLineReader.GetColumn("NPCTable", "Style");

        
        NpcInfoGen();
        for(int i = 0; i < 10 ; i++){
            Debug.Log($"{nameTable[i]}, {ageTable[i]}, {homeTable[i]}");
        }
    }
    void NpcInfoGen(){
        Debug.Log($"{nameList[0]}");
        for (int i = 0; i < 10; i++){
            nameTable.Add($"{ReturnRandElement(nameList)}");
            ageTable.Add($"{ReturnRandElement(nameList)}");
            homeTable.Add($"{ReturnRandElement(nameList)}");
            //nameTable[i] = ReturnRandElement(styleList); 
            //nameTable[i] = ReturnRandElement(nameList, i);
            Debug.Log("����");
        }
    }

    string ReturnRandElement(List<string> list){

        System.Random rnd = new System.Random();
        int idx = rnd.Next(0, list.Count);
        Debug.Log(list[idx]);
        return list[idx];
    }
}
