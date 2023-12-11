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
    private NpcCreateParameter[] _stayNpcParams =
    {
        new NpcCreateParameter(NpcType.Stay, 100, "������", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 101, "���ǽ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 102, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 103, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 104, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 105, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 106, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 107, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 108, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 109, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 110, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 111, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 112, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 113, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 114, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 115, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 116, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 117, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 118, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 119, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 120, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 121, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Stay, 122, "�Դϴ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
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

    private NpcCreateParameter[] _patrolNpcParams =
    {
        new NpcCreateParameter(NpcType.Patrol, 200, "��Ʈ��", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 201, "���ǽ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 202, "�Դϴ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 203, "���ǽ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 204, "���ǽ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 205, "���ǽ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 206, "���ǽ�", 10, "male", "�̻���", "õ��", "�Ѿ�", "�ǸŻ�", 5, 10, true),
        new NpcCreateParameter(NpcType.Patrol, 207, "���ǽ�", 10, "male", "�̻���", "���", "�Ѿ�", "�ǸŻ�", 5, 10, true),
    };
        

    //GateNpc ����Ʈ�� �߰�
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
            Debug.Log("�ش� ID�� ���� �׸��� �����ϴ�.");
        }
    }


    public void Start()
    {
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

        //GateNpc ����Ʈ�� �߰�
        AddGateNpcList(1);
        AddGateNpcList(20);
        AddGateNpcList(40);
        AddGateNpcList(53);
        AddGateNpcList(74);

        //GateNPC ����
        foreach (var i in _actionNpcParams)
        {
            GameObject Gatenpc = NpcManager.Instance.CreateNPC(i);
        }

    }   
}
