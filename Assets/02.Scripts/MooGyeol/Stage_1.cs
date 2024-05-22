using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;
using static NpcCreateParameter;

public class Stage_1 : MonoBehaviour
{
    NpcInfoGenerater n;
    private NpcCreateParameter[] _patrolNpcParams; // NpcCreateParameter 배열 선언
    private NpcCreateParameter[] _stayNpcParams; // 또 다른 NpcCreateParameter 배열 선언
    private static NpcCreateParameter[] _actionNpcParams; 

    private int temp = 0; // npc id 중복방지 저장 변수

    // Dictionary<string, object> entry;

    
    #region NPC 스폰 위치 Vector3 값들
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
    #endregion
    #region 이전 방식 주석처리
    //GateNpc 리스트에 추가
    public void AddGateNpcList(int id)
    {   

        
        // entry = NpcManager.Instance.GetEntryById(id);
        // if (entry != null)
        // {
        //     _actionNpcParams.Add(
        //         new NpcCreateParameter(
        //             NpcType.Gate,
        //             (int)entry["ID"],
        //             (string)entry["Name"],
        //             (int)entry["Age"],
        //             (string)entry["Gender"],
        //             (string)entry["Style"], 
        //             (string)entry["Status"], 
        //             (string)entry["Hometown"],
        //             (string)entry["Job"], 10, 20, false)
        //         );
        // }
        // else
        // {
        //     Debug.Log("해당 ID를 가진 항목이 없습니다.");
        // }

    }
    #endregion


    public void Start()
    {
        // NpcInfoGenerator.cs에서 List<string> Table들에 접근하기 위함.
        n = GetComponent<NpcInfoGenerater>();
        //NPC 파라미터 생성
        SetParameters(ref _stayNpcParams, NpcType.Stay, 20);
        SetParameters(ref _patrolNpcParams, NpcType.Patrol, 8);
        SetParameters(ref _actionNpcParams, NpcType.Gate, 5);

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

        //GateNPC 스폰
        for (int i=0; i < _actionNpcParams.Length; i++)
        {
            GameObject Gatenpc = NpcManager.Instance.CreateNPC(_actionNpcParams[i]);
        }

        //GateNpc 리스트에 추가
        // AddGateNpcList(1);
        // AddGateNpcList(20);
        // AddGateNpcList(40);
        // AddGateNpcList(53);
        // AddGateNpcList(74);

        // //GateNPC 스폰
        // foreach (var i in _actionNpcParams)
        // {
        //     GameObject Gatenpc = NpcManager.Instance.CreateNPC(i);
        // }

    }   

    // npcNumber만큼 npc 생성
    void SetParameters(ref NpcCreateParameter[] npcArray, NpcType type, int npcNumber)
    {    
        npcArray = new NpcCreateParameter[npcNumber];

        for (int i = temp; i < temp + npcNumber; i++){
            //ToDo isVillain 등 빌런 관련 변수 NpcCreateParameter에 추가하기
            npcArray[i] = new NpcCreateParameter(
                type,
                i,
                n.nameTable[i],
                n.ageTable[i],
                n.genderTable[i],
                n.styleTable[i],
                n.homeTable[i],
                n.statusTable[i],
                n.jobTable[i],
                n.passPurposeTable[i],
                "아이템",
                n.npcDailyTable[i]
            );

            if (i == temp + npcNumber){
                temp = i;
            }
        }
    }
}
