using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;
using static NpcCreateParameter;

public class Stage_1 : MonoBehaviour
{
    Dictionary<string, object> entry;
    private static List<NpcCreateParameter> _actionNpcParams = new List<NpcCreateParameter>();

    //StayNPC 리스트
    private static Vector3[] _stayNpcPosition =
    {
        new Vector3(-13, 0, -59),
        new Vector3(-20, 0, -67),
        new Vector3(-14, 0, -54),
        new Vector3(-19, 0, -45),
        new Vector3(-16, 0, -73),
        new Vector3(-24, 0, -76),
        new Vector3(-24, 0, -88),
        new Vector3(-19, 0, -86),
        new Vector3(-25, 0, -97),
        new Vector3(-19, 0,-97),
        new Vector3(-25, 0, -110),
        new Vector3(-25, 0, -123),
        new Vector3(-18, 0, -113),
        new Vector3(-11, 0, -127),
        new Vector3(-7, 0, -157),
        new Vector3(-24, 0, -158),
        new Vector3(7, 0, -146),
        new Vector3(0, 0, -139),
        new Vector3(13, 0, -139),
        new Vector3(19, 0, -147),
        new Vector3(29, 0, -149),
        new Vector3(35, 0, -145),
        new Vector3(-26, 0, -143),
        
    };
    private NpcCreateParameter[] _stayNpcParams =
    {
        new NpcCreateParameter(NpcType.Stay, 100, "스테이", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 101, "엔피시", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 102, "입니다", 10, "male", "이상함", "상민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 103, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 104, "입니다", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 105, "입니다", 10, "male", "이상함", "상민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 106, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 107, "입니다", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 108, "입니다", 10, "male", "이상함", "상민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 109, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 110, "입니다", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 111, "입니다", 10, "male", "이상함", "상민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 112, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 113, "입니다", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 114, "입니다", 10, "male", "이상함", "상민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 115, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 116, "입니다", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 117, "입니다", 10, "male", "이상함", "상민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 118, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 119, "입니다", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 120, "입니다", 10, "male", "이상함", "상민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 121, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 122, "입니다", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
    };


    //PatrolNPC 리스트
    private Vector3[] _patrolNpcPosition =
    {
        new Vector3(-14, 1, -160),
        new Vector3(-14, 1, -160),
        new Vector3(-33, 1, -172),
        new Vector3(5, 1, -45),
        new Vector3(48, 1, -151),
        new Vector3(48, 1, -132),
        new Vector3(-20, 1, -77),
        new Vector3(-20, 1, -77),

    };

    private NpcCreateParameter[] _patrolNpcParams =
    {
        new NpcCreateParameter(NpcType.Patrol, 200, "패트롤", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 201, "엔피시", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 202, "입니다", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 203, "엔피시", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 204, "엔피시", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 205, "엔피시", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 206, "엔피시", 10, "male", "이상함", "천민", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 207, "엔피시", 10, "male", "이상함", "양민", "한양", "판매상", 5, 10, true),
    };
        

    //GateNpc 리스트에 추가
    public void AddGateNpcList(int id)
    {
        entry = NpcManager.Instance.GetEntryById(id);
        if (entry != null)
        {
            _actionNpcParams.Add(
                new NpcCreateParameter(
                    NpcType.Gate,
                    (int)entry["ID"],
                    (string)entry["Name"],
                    (int)entry["Age"],
                    (string)entry["Gender"],
                    (string)entry["Style"], 
                    (string)entry["Status"], 
                    (string)entry["Hometown"],
                    (string)entry["Job"], 10, 20, false)
                );
        }
        else
        {
            Debug.Log("해당 ID를 가진 항목이 없습니다.");
        }
    }


    public void Start()
    {
        //StayNPC 스폰
        for (int i=0; i < _stayNpcParams.Length; i++)
        {
            GameObject Staynpc = NpcManager.Instance.CreateNPC(_stayNpcParams[i]);
            Staynpc.transform.position = _stayNpcPosition[i];
        }

        //PartolNPC 스폰
        for (int i=0; i < _patrolNpcParams.Length; i++)
        {
            GameObject Partrolnpc = NpcManager.Instance.CreateNPC(_patrolNpcParams[i]);
            Partrolnpc.transform.position = _patrolNpcPosition[i];
        }

        //GateNpc 리스트에 추가
        AddGateNpcList(1);
        AddGateNpcList(20);
        AddGateNpcList(40);
        AddGateNpcList(53);
        AddGateNpcList(74);

        //GateNPC 스폰
        foreach (var i in _actionNpcParams)
        {
            GameObject Gatenpc = NpcManager.Instance.CreateNPC(i);
        }

    }   
}
