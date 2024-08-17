using RoslynCSharp.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private static GameMgr _instance;

    public static GameMgr Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<GameMgr>();
                singletonObject.name = typeof(GameMgr).ToString() + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // *�߿�) stageNum�� �����Ҷ� �ش� �������� ������ �ٷ� +1 �ϵ��� �������

    public int townHappinessPoint = 40; // ���� �ູ��
    public int bossSatisfaction = 30; // Ž������ ������
    public int money = 0;
    public int stageNum = 1;
    public string PlageVilage = "";
    public int NpcID = 0;

    // �������� ����
    public bool AbleCheckItem = false;
    public bool AbleRunNpc = false; // ���� Npc ����� Stage
    public bool AbleGetMoney = false; // ���� �ر� Stage
    public bool AblePlague = false; //���� �ر� Stage
    public bool AbleChepo = true; //ü�� �ر�
    public bool AbleNextDay = true; // ���� �ر�

    public void SetPlagueVilage()
    {
        int n = Random.Range(0, 8);
        string[] s = {"�Ѿ�", "��⵵", "��û��", "����", "���", "Ȳ�ص�", "��ȵ�", "������", "�԰浵" };
        PlageVilage = s[n];
        Debug.Log(PlageVilage);
    }


    public int GetStageNum()
    {
        return stageNum;
    }

    public void AddStageNum()
    {
        stageNum++;
    }

    public void AddNpcID() 
    {
        NpcID++;
    }
    public void ResetNpcID()
    {
        NpcID = 0;
    }
}