using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NpcInfoGenerater : MonoBehaviour
{
    // CSV로 불러온 데이터들 저장.   
    private List<string> nameList;
    private List<string> ageList;
    private List<string> homeList;
    private List<string> styleList;
    //private List<string> List;
  //  private List<string> nameList;

    // 랜덤으로 구성된 npc 테이블 2차원 리스트로 하면 복잡할거같아서 1차원으로 구성함.
    // 불러올때는 npc id를 리스트 인덱스로 사용
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
            Debug.Log("실행");
        }
    }

    string ReturnRandElement(List<string> list){

        System.Random rnd = new System.Random();
        int idx = rnd.Next(0, list.Count);
        Debug.Log(list[idx]);
        return list[idx];
    }
}
