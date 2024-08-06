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
        // 만약 인스턴스가 이미 존재하고 현재 인스턴스와 다르다면, 중복된 인스턴스를 파괴
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

    public int townHappinessPoint = 40; // 마을 행복도
    public int bossSatisfaction = 30; // 탐관오리 만족도
    public int money = 0;
    public bool isTarget = false;
    public int stageNum = 1;

    public int GetStageNum()
    {
        return stageNum;
    }

    public void AddStageNum()
    {
        stageNum++;
    }
}