using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NpcInfoGenerater : MonoBehaviour
{   
    // �����ϰ��� �ϴ� npc ��
    public int npcNumber = 100;
    // CSV�� �ҷ��� �����͵� ����.   
    private List<string> nameList;
    private List<string> ageList;
    private List<string> genderList;
    private List<string> homeList;
    private List<string> styleList;
    private List<string> statusList;
    private List<string> jobList;
    private List<string> passPurposeList;
    private List<string> npcDailyList;

    // �������� �籸���� NPC ���� ���̺� �� ������ �� �ʿ� ������.
    // �ӽ� CSV ���̺��
    public List<string> nameTable;
    public List<string> ageTable;
    public List<string> genderTable;
    public List<string> homeTable;
    public List<string> styleTable;
    public List<string> statusTable;
    public List<string> jobTable;
    public List<string> passPurposeTable;
    public List<string> npcDailyTable;

    void Start(){
        


        Debug.Log(nameList[1]);
        initTable();
        RandomTableGen();
        //
        for(int i = 0; i < 10 ; i++){
            Debug.Log($"{nameTable[i]}, {ageTable[i]}, {homeTable[i]}");
        }
    }
    void RandomTableGen(){
        

        //Debug.Log($"{nameList[0]}");
        for (int i = 0; i < npcNumber; i++){
            nameTable.Add($"{ReturnRandElement(nameList)}");
            ageTable.Add($"{ReturnRandElement(ageList)}");
            genderTable.Add($"{ReturnRandElement(genderList)}");
            homeTable.Add($"{ReturnRandElement(homeList)}");
            styleTable.Add($"{ReturnRandElement(styleList)}");
            statusTable.Add($"{ReturnRandElement(statusList)}");
            jobTable.Add($"{ReturnRandElement(jobList)}");
            passPurposeTable.Add($"{ReturnRandElement(passPurposeList)}");
            npcDailyTable.Add($"{ReturnRandElement(npcDailyList)}");   
        }
    }

    // �ߺ� ���� ����
    string ReturnRandElement(List<string> list){

        System.Random rnd = new System.Random();
        int idx = rnd.Next(0, list.Count);
        //Debug.Log(list[idx]);
        return list[idx];
    }

    // CSV���� �ε� �� ���̺� �ʱ�ȭ
    void initTable(){
        nameList = CSVLineReader.GetColumn("NPCTable", "Name");
        ageList = CSVLineReader.GetColumn("NPCTable", "Age");
        genderList = CSVLineReader.GetColumn("NPCTable", "Gender");
        homeList = CSVLineReader.GetColumn("NPCTable", "Hometown");
        styleList = CSVLineReader.GetColumn("NPCTable", "Style");
        statusList = CSVLineReader.GetColumn("NPCTable", "Status");
        jobList = CSVLineReader.GetColumn("NPCTable", "Job");
        passPurposeList = CSVLineReader.GetColumn("NPCTable", "PassPurpose");
        npcDailyList = CSVLineReader.GetColumn("NPCTable", "NpcDaily");

        nameTable = new List<string>();
        ageTable = new List<string>();
        genderTable = new List<string>();
        homeTable = new List<string>();
        styleTable = new List<string>();
        statusTable = new List<string>();
        jobTable = new List<string>();
        passPurposeTable = new List<string>();
        npcDailyTable = new List<string>();
    }     
    
}
