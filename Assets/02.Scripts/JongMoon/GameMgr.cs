using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static int townHappinessPoint = 40; // ���� �ູ��
    public static int bossSatisfaction = 30; // Ž������ ������
    public static int HCode { get; private set; } // �ູ�� �ڵ�
    public static int Scode { get; private set; } // ������ �ڵ�

    void Update()
    {
        UpdatetownHappinessPointCode();
        UpdatebossSatisfactionCode();

        // �α׸� ���� ���� ��ȭ Ȯ��
        Debug.Log("Town Happiness: " + townHappinessPoint);
        Debug.Log("Boss Satisfaction: " + bossSatisfaction);
    }



    static void UpdatetownHappinessPointCode()
    {
        if (townHappinessPoint <= 100 && townHappinessPoint >= 60)
        {
            HCode = 1; // ����
        }
        else if (townHappinessPoint < 60 && townHappinessPoint >= 30)
        {
            HCode = 2; // ����
        }
        else
        {
            HCode = 3; // ����
        }
    }

    static void UpdatebossSatisfactionCode()
    {
        if (bossSatisfaction <= 100 && bossSatisfaction >= 60)
        {
            Scode = 3; // ���� Ž�� ��ġ
        }
        else if (bossSatisfaction < 60 && bossSatisfaction >= 30)
        {
            Scode = 2; // �߰� Ž�� ��ġ
        }
        else
        {
            Scode = 1; // ���� Ž�� ��ġ
        }
    }
}