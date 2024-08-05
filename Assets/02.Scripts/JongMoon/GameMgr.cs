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
        // ���� �ν��Ͻ��� �̹� �����ϰ� ���� �ν��Ͻ��� �ٸ��ٸ�, �ߺ��� �ν��Ͻ��� �ı�
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

    public int townHappinessPoint = 40; // ���� �ູ��
    public int bossSatisfaction = 30; // Ž������ ������

    static void UpdatetownHappinessPointCode(int townHappinessPoint)
    {
        if (townHappinessPoint <= 100 && townHappinessPoint >= 60)
        {

        }
        else if (townHappinessPoint < 60 && townHappinessPoint >= 30)
        {

        }
        else
        {

        }
    }

    static void UpdatebossSatisfactionCode(int bossSatisfaction)
    {
        if (bossSatisfaction <= 100 && bossSatisfaction >= 60)
        {

        }
        else if (bossSatisfaction < 60 && bossSatisfaction >= 30)
        {
   
        }
        else
        {
 
        }
    }
}