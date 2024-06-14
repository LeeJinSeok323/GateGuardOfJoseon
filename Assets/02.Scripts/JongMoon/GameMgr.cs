using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static int townHappinessPoint = 40; // 마을 행복도
    public static int bossSatisfaction = 30; // 탐관오리 만족도
    public static int HCode { get; private set; } // 행복도 코드
    public static int Scode { get; private set; } // 만족도 코드

    void Update()
    {
        UpdatetownHappinessPointCode();
        UpdatebossSatisfactionCode();

        // 로그를 통해 값의 변화 확인
        Debug.Log("Town Happiness: " + townHappinessPoint);
        Debug.Log("Boss Satisfaction: " + bossSatisfaction);
    }



    static void UpdatetownHappinessPointCode()
    {
        if (townHappinessPoint <= 100 && townHappinessPoint >= 60)
        {
            HCode = 1; // 좋음
        }
        else if (townHappinessPoint < 60 && townHappinessPoint >= 30)
        {
            HCode = 2; // 보통
        }
        else
        {
            HCode = 3; // 나쁨
        }
    }

    static void UpdatebossSatisfactionCode()
    {
        if (bossSatisfaction <= 100 && bossSatisfaction >= 60)
        {
            Scode = 3; // 높은 탐욕 수치
        }
        else if (bossSatisfaction < 60 && bossSatisfaction >= 30)
        {
            Scode = 2; // 중간 탐욕 수치
        }
        else
        {
            Scode = 1; // 낮은 탐욕 수치
        }
    }
}