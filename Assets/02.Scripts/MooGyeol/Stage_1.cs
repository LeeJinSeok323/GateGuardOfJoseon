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
    private NpcCreateParameter[] _actionNpcParams; 
    private int NpcCnt = 0;

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
        new Vector3(-26, 0, -143)
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
        NpcCnt = 0;
        // NpcInfoGenerator.cs���� List<string> Table�鿡 �����ϱ� ����.
        n = NpcInfoGenerater.Instance;
        //NPC �Ķ���� ����
        SetParameters(ref _actionNpcParams, NpcType.Gate, 5);

        SetParameters(ref _stayNpcParams, NpcType.Stay, 20);
        SetParameters(ref _patrolNpcParams, NpcType.Patrol, 8);

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
    }   

    // npcNumber��ŭ npc ����
    void SetParameters(ref NpcCreateParameter[] npcArray, NpcType type, int npcNumber)
    {    
        
        npcArray = new NpcCreateParameter[npcNumber];

        for (int i = 0; i <  npcNumber; i++){

            bool vilran = GetRandomBool();
            // Debug.Log($"id{NpcCnt}�� �����Դϱ�? {vilran}");
            if (!vilran) 
            {
                npcArray[i] = new NpcCreateParameter(
               type,
               NpcCnt,
               n.nameTable[NpcCnt],
               n.ageTable[NpcCnt],
               n.genderTable[NpcCnt],
               //n.styleTable[i],
               n.statusTable[NpcCnt],
               n.homeTable[NpcCnt],
               n.jobTable[NpcCnt],
               n.passPurposeTable[NpcCnt],
               n.itemTable[NpcCnt],
               n.npcDailyTable[NpcCnt],
               vilran
                 );
            //    Debug.Log($"����x {NpcCnt}�� �̸���{n.nameTable[NpcCnt]}, {NpcInfoGenerater.Instance.nameTable[NpcCnt]}");

            }
            else 
            {
               npcArray[i] = new NpcCreateParameter(
               type,
               NpcCnt,
               n.nameTable[NpcCnt + 20],
               n.ageTable[NpcCnt + 20],
               n.genderTable[NpcCnt],
               //n.styleTable[i],
               n.statusTable[NpcCnt],
               n.homeTable[NpcCnt + 20],
               n.jobTable[NpcCnt + 20],
               n.passPurposeTable[NpcCnt],
               n.itemTable[NpcCnt],
               n.npcDailyTable[NpcCnt],
               vilran
                 );
            //    Debug.Log($"����o {NpcCnt}�� �̸���{n.nameTable[NpcCnt+20]},{NpcInfoGenerater.Instance.nameTable[NpcCnt]}");
            }
            NpcCnt++;
        }

        bool GetRandomBool()
        {
            return Random.Range(0, 2) == 1;
        }

    }
}
