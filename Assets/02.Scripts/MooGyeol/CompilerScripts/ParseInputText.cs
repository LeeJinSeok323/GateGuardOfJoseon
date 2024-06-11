using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using UnityEngine;

public class ParseInputText : MonoBehaviour
{
    public void ParseInput(string input)
    {
        Debug.Log(input);

        // �̸� ����
        Match nameMatch = Regex.Match(input, @"�̸���\s(.+?)��");
        if (nameMatch.Success)
        {
            Script_q._Name = nameMatch.Groups[1].Value;
        }
        else
        {
            Debug.LogWarning("Name parsing failed.");
        }

        // ���� ����
        Match roleMatch = Regex.Match(input, @"��\s(.+?)\s�Ǵ�");
        if (roleMatch.Success)
        {
            Script_q._Role = roleMatch.Groups[1].Value;
        }
        else
        {
            Debug.LogWarning("Role parsing failed.");
        }

        // ������Ʈ �̸� �� ���� ����
        Match objMatch = Regex.Match(input, @"(\w+)\s(\d+)��");
        if (objMatch.Success)
        {
            Script_q._Obj = objMatch.Groups[1].Value; // � �ܾ�� ������Ʈ �̸����� ���
            Script_q._Cnt = int.Parse(objMatch.Groups[2].Value);
        }
        else
        {
            Debug.LogWarning("Object and count parsing failed.");
        }

        //Debug.Log("Name; " + Script_q._Name);
        //Debug.Log("_Role; " + Script_q._Role);
        //Debug.Log("_Obj; " + Script_q._Obj);
        //Debug.Log("_Cnt; " + Script_q._Cnt);
    }
}
