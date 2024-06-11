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

        // 이름 추출
        Match nameMatch = Regex.Match(input, @"이름이\s(.+?)인");
        if (nameMatch.Success)
        {
            Script_q._Name = nameMatch.Groups[1].Value;
        }
        else
        {
            Debug.LogWarning("Name parsing failed.");
        }

        // 역할 추출
        Match roleMatch = Regex.Match(input, @"인\s(.+?)\s되는");
        if (roleMatch.Success)
        {
            Script_q._Role = roleMatch.Groups[1].Value;
        }
        else
        {
            Debug.LogWarning("Role parsing failed.");
        }

        // 오브젝트 이름 및 개수 추출
        Match objMatch = Regex.Match(input, @"(\w+)\s(\d+)개");
        if (objMatch.Success)
        {
            Script_q._Obj = objMatch.Groups[1].Value; // 어떤 단어든 오브젝트 이름으로 사용
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
