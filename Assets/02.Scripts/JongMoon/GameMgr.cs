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

    // *중요) stageNum은 시작할때 해당 스테이지 생성후 +1 하도록 만들었음
    // 따라서 스테이지로 if문으로 분기 만들 때 +1 해서 생각해야 해

    public int townHappinessPoint = 40; // 마을 행복도
    public int bossSatisfaction = 30; // 탐관오리 만족도
    public int money = 0;
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