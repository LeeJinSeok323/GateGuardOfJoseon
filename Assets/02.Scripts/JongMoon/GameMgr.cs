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

    // *중요) stageNum은 시작할때 해당 스테이지 생성후 바로 +1 하도록 만들었음

    public int townHappinessPoint = 40; // 마을 행복도
    public int bossSatisfaction = 30; // 탐관오리 만족도
    public int money = 0;
    public int stageNum = 1;
    public string PlageVilage = "";
    public int NpcID = 0;

    // 스테이지 설정
    public bool AbleCheckItem = false;
    public bool AbleRunNpc = false; // 도주 Npc 허락할 Stage
    public bool AbleGetMoney = false; // 수금 해금 Stage
    public bool AblePlague = false; //역병 해금 Stage
    public bool AbleChepo = true; //체포 해금
    public bool AbleNextDay = true; // 정산 해금

    public void SetPlagueVilage()
    {
        int n = Random.Range(0, 8);
        string[] s = {"한양", "경기도", "충청도", "전라도", "경상도", "황해도", "평안도", "강원도", "함경도" };
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