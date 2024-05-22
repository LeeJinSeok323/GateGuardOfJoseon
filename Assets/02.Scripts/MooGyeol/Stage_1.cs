using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;
using static NpcCreateParameter;

public class Stage_1 : MonoBehaviour
{
    NpcInfoGenerater n;
    private NpcCreateParameter[] _patrolNpcParams; // NpcCreateParameter �迭 ����
    private NpcCreateParameter[] _stayNpcParams; // �� �ٸ� NpcCreateParameter �迭 ����
    private static NpcCreateParameter[] _actionNpcParams; 

    private int temp = 0; // npc id �ߺ����� ���� ����

    // Dictionary<string, object> entry;

    
    #region NPC ���� ��ġ Vector3 ����
    //StayNPC ����Ʈ
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

    //PatrolNPC ����Ʈ
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
    #region ���� ��� �ּ�ó��
    //GateNpc ����Ʈ�� �߰�
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
        //     Debug.Log("�ش� ID�� ���� �׸��� �����ϴ�.");
        // }

    }
    #endregion


    public void Start()
    {
        // NpcInfoGenerator.cs���� List<string> Table�鿡 �����ϱ� ����.
        n = GetComponent<NpcInfoGenerater>();
        //NPC �Ķ���� ����
        SetParameters(ref _stayNpcParams, NpcType.Stay, 20);
        SetParameters(ref _patrolNpcParams, NpcType.Patrol, 8);
        SetParameters(ref _actionNpcParams, NpcType.Gate, 5);

        //StayNPC ����
        for (int i=0; i < _stayNpcParams.Length; i++)
        {
            GameObject Staynpc = NpcManager.Instance.CreateNPC(_stayNpcParams[i]);
            Staynpc.transform.position = _stayNpcPosition[i];
        }

        //PartolNPC ����
        for (int i=0; i < _patrolNpcParams.Length; i++)
        {
            GameObject Partrolnpc = NpcManager.Instance.CreateNPC(_patrolNpcParams[i]);
            Partrolnpc.transform.position = _patrolNpcPosition[i];
        }

        //GateNPC ����
        for (int i=0; i < _actionNpcParams.Length; i++)
        {
            GameObject Gatenpc = NpcManager.Instance.CreateNPC(_actionNpcParams[i]);
        }

        //GateNpc ����Ʈ�� �߰�
        // AddGateNpcList(1);
        // AddGateNpcList(20);
        // AddGateNpcList(40);
        // AddGateNpcList(53);
        // AddGateNpcList(74);

        // //GateNPC ����
        // foreach (var i in _actionNpcParams)
        // {
        //     GameObject Gatenpc = NpcManager.Instance.CreateNPC(i);
        // }

    }   

    // npcNumber��ŭ npc ����
    void SetParameters(ref NpcCreateParameter[] npcArray, NpcType type, int npcNumber)
    {    
        npcArray = new NpcCreateParameter[npcNumber];

        for (int i = temp; i < temp + npcNumber; i++){
            //ToDo isVillain �� ���� ���� ���� NpcCreateParameter�� �߰��ϱ�
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
                "������",
                n.npcDailyTable[i]
            );

            if (i == temp + npcNumber){
                temp = i;
            }
        }
    }
}
