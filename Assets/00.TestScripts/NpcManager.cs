using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NpcCreateParameter;

public class NpcManager : MonoBehaviour
{
    public List<Dictionary<string, object>> data_Dialog;

    private static NpcManager instance;
    public GameObject GateNpc;
    public GameObject StayNpc;
    public GameObject PatrolNpc;
    public GameObject RunNpc;

    public Vector3 PassPoint = new Vector3(11, 0, 22);
    public Vector3 DeninedPoint = new Vector3(13, 0, 5);

    public static NpcManager Instance
    {
        get 
        { 
            if (instance == null)
                return null;

            return instance; 
        }
    }

    private void Awake()
    {
       if( instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        InitTable();
    }

    public void InitTable()
    {
        // NPC ���̺� �ε�
        data_Dialog = CSVReader.Read("NPCTable");
    }

    public Dictionary<string, object> GetEntryById(int id)
    {
        if (data_Dialog == null)
        {
            Debug.LogError("NPC ���̺��� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return null;
        }

        string strId = id.ToString();

        foreach (var entry in data_Dialog)
        {
            if (entry.ContainsKey("ID") && entry["ID"].ToString() == strId)
            {
                return entry;
            }
        }

        return null; 
    }


    public GameObject CreateNPC(NpcCreateParameter parm)
    {
        //�ε�� ���̺��� Type�� NpcNumber ����
        GameObject npc = null;

        switch (parm.npcType)
        {
            case NpcType.Stay:
                npc = GameObject.Instantiate(StayNpc);
                npc.transform.parent = this.transform;
                AddParameters(npc, parm);
                break;
            case NpcType.Patrol:
                npc = GameObject.Instantiate(PatrolNpc);
                npc.transform.parent = this.transform;
                npc = GameObject.Instantiate(GateNpc);
                break;
            case NpcType.Gate:
                npc = GameObject.Instantiate(GateNpc);
                npc.transform.parent = this.transform;
                AddParameters(npc, parm);
                break;
            case NpcType.Run:
                npc = GameObject.Instantiate(RunNpc);
                npc.transform.parent = this.transform;
                AddParameters(npc, parm);
                break;
        }

        return npc;

    }

    private void AddParameters(GameObject npc, NpcCreateParameter parm)
    {
        Npc npcComponent = npc.GetComponent<Npc>();

        if (npcComponent != null)
        {
 
            npcComponent.npcType = parm.npcType;
            npcComponent.ID = parm.Number;
            npcComponent.Name = parm.Name;
            npcComponent.Age = parm.Age;
            npcComponent.Gender = parm.Gender;
            npcComponent.Style = parm.Style;
            npcComponent.Status = parm.Status;
            npcComponent.Hometown = parm.Hometown;
            npcComponent.Job = parm.Job;

            npcComponent.WalkSpeed = parm.WalkSpeed;
            npcComponent.RunSpeed = parm.RunSpeed;
            npcComponent.IsRunning = parm.IsRunning;
        }
        else
        {
            Debug.LogWarning("GameObject���� Npc ������Ʈ�� ã�� �� �����ϴ�: " + npc.name);
        }
    }

    public int GetIdByObject(GameObject Object)
    {
        Npc npc = Object.GetComponent<Npc>();
        if (npc != null)
        {
            int id = npc.ID;
            return id;
        }
        else 
        { 
            return 9999; 
        }
    }

    private GameObject FindNpcGameObjectById(int npcId)
    {
        GameObject[] allNpcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in allNpcs)
        {
            Npc npcComponent = npc.GetComponent<Npc>();
            if (npcComponent != null && npcComponent.ID == npcId)
            {
                return npc;
            }
        }

        return null;
    }

    public void Remove(int id)
    {
        GameObject npc  = FindNpcGameObjectById(id);
        Destroy(npc, 10.0f);
    }

    public void ChangeToWalk(int id, Vector3 position)
    {
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate != null)
        {
            gate.currentTarget = position;

            if (gate.state == NpcBehavior_Gate.State.IDLE)
            {
                gate.state = NpcBehavior_Gate.State.WALK;
            }

        }
    }

    public void ChangeToTalk(int id)
    {
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate != null)
        {
            if (gate.state == NpcBehavior_Gate.State.IDLE)
            {
                gate.state = NpcBehavior_Gate.State.TALK;
            }

        }

        NpcBehavior_Stay stay = npc.GetComponent<NpcBehavior_Stay>();
        if(stay != null)
        {
            if (stay.state == NpcBehavior_Stay.State.IDLE)
            {
                stay.state = NpcBehavior_Stay.State.TALK;
            }
        }

    }

    public void PassGate(int id)
    {
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate != null)
        {
            gate.currentTarget = PassPoint;

            if (gate.state == NpcBehavior_Gate.State.IDLE)
            {
                gate.state = NpcBehavior_Gate.State.WALK;
            }

        }
        else
        {
            return;
        }
    }

    public void DeninedGate(int id)
    {
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate == null) {
            return;
        }
        
        gate.currentTarget = DeninedPoint;

        if (gate.
            state != NpcBehavior_Gate.State.IDLE)
        {
            return;
        }

        gate.state = NpcBehavior_Gate.State.WALK;
    }


}
