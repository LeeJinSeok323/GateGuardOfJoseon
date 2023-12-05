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
    private static Vector3[] _spawnNormalPosition =
    {
        new Vector3(10, 0, 7),
        new Vector3(2, 0, 2),
        new Vector3(10, 0, 17),
    };
    private NpcCreateParameter[] _StayNpcParams =
    {
        new NpcCreateParameter(NpcType.Stay, 100, "스테이", 10, "male", "이상함", "상인", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 101, "엔피시", 10, "male", "이상함", "상인", "한양", "판매상", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 102, "입니다", 10, "male", "이상함", "상인", "한양", "판매상", 5, 10, true),
    };


    //PatrolNPC 리스트
    NpcCreateParameter PatrolNpcParm 
        = new NpcCreateParameter(NpcType.Patrol, 200, "패트롤", 10, "male", "이상함", "중인", "한양", "판매상", 5, 10, true);

    private Vector3[] _patrolNpcPosition =
    {
        new Vector3(-20, 0, 10),
        new Vector3(5, 0, 20),
        new Vector3(-10, 0, 0),
    };


    // GateNpc 리스트
    //private NpcCreateParameter[] _actionNpcParams =
    //{
    //    new NpcCreateParameter(NpcType.Gate, 0 , 5 , 20 , false),
    //    new NpcCreateParameter(NpcType.Gate, 1 , 15 , 30 , false),
    //    new NpcCreateParameter(NpcType.Gate, 2 , 15 , 30 , false),
    //    new NpcCreateParameter(NpcType.Gate, 3 , 15 , 30 , false),
    //    new NpcCreateParameter(NpcType.Gate, 4 , 15 , 30 , false),
    //};
    
    

    public void Awake()
    {
        AddGateNpcParam(1);
        AddGateNpcParam(2);
        AddGateNpcParam(3);
        AddGateNpcParam(4);

        //StayNPC 스폰
        for (int i=0; i < _StayNpcParams.Length; i++)
        {
            GameObject Staynpc = NpcManager.Instance.CreateNPC(_StayNpcParams[i]);
            Staynpc.transform.position = _spawnNormalPosition[i];
        }

        //PartolNPC 스폰
        foreach (var position in _patrolNpcPosition)
        {
            GameObject Partrolnpc = NpcManager.Instance.CreateNPC(PatrolNpcParm);
            Partrolnpc.transform.position = position;
        }



        //GateNPC 스폰
        foreach (var i in _actionNpcParams)
        {
            GameObject Gatenpc = NpcManager.Instance.CreateNPC(i);
        }

    }
    

    public void AddGateNpcParam(int id)
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
                (string)entry["Style"], (string)entry["Status"], (string)entry["Hometown"],
                (string)entry["Job"], 10, 20, false)
                );
        }
        else
        {
            Debug.Log("해당 ID를 가진 항목이 없습니다.");
        }
    }
}
