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

    // *�߿�) stageNum�� �����Ҷ� �ش� �������� ������ +1 �ϵ��� �������
    // ���� ���������� if������ �б� ���� �� +1 �ؼ� �����ؾ� ��

    public int townHappinessPoint = 40; // ���� �ູ��
    public int bossSatisfaction = 30; // Ž������ ������
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