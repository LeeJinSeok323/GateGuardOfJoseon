using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NpcCreateParameter;

public class NpcManager : MonoBehaviour
{
    public List<Dictionary<string, object>> data_Dialog;
    private static NpcManager instance;

    public GameObject StayNpc1;
    public GameObject StayNpc2;
    public GameObject StayNpc3;

    public GameObject PatrolNpc1;
    public GameObject PatrolNpc2;

    public GameObject GateNpc1;
    public GameObject GateNpc2;
    public GameObject GateNpc3;
    public GameObject GateNpc4;
    public GameObject GateNpc5;

    //public GameObject RunNpc;

    public Vector3 PassPoint;
    public Vector3 DeninedPoint;

    public static NpcManager Instance
    {
        get 
        { 
            if (instance == null)
            {
                instance = FindObjectOfType<NpcManager>();

                if (instance == null)
                {
                    // 찾을 수 없으면 새로운 GameObject에 추가하여 생성
                    GameObject obj = new GameObject("NpcManager");
                    instance = obj.AddComponent<NpcManager>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }
                
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
        //InitTable();
        //Debug.Log("테이블 완");
    }
    /*
#region NPC 테이브 로드
    public void InitTable()
    {
        // NPC 테이블 로드
        data_Dialog = CSVReader.Read("NPCTable");
    }

    public Dictionary<string, object> GetEntryById(int id)
    {
        if (data_Dialog == null)
        {
            Debug.LogError("NPC 테이블이 초기화되지 않았습니다.");
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
#endregion
*/
    

    public GameObject CreateNPC(NpcCreateParameter parm)
    {
        //로드된 테이블에서 Type와 NpcNumber 지정
        GameObject npc = null;
        
        //Debug.Log($"NpcManager pram.Status = {parm.Status}");
        switch (parm.npcType)
        {
            case NpcType.Stay:
                switch (parm.Status)
                {   
                    case "천민":
                        npc = GameObject.Instantiate(StayNpc1);
                    break;

                    case "양인":
                        npc = GameObject.Instantiate(StayNpc2);
                    break;

                    case "상민":
                        npc = GameObject.Instantiate(StayNpc3);
                    break;
                    default:
                        npc = GameObject.Instantiate(StayNpc3);
                    break;
                }
                break;
            case NpcType.Patrol:
                switch (parm.Status)
                {
                    case "천민":
                        npc = GameObject.Instantiate(PatrolNpc1);
                    break;

                    case "양인":
                        npc = GameObject.Instantiate(PatrolNpc2);
                    break;
                    default:
                        npc = GameObject.Instantiate(PatrolNpc1);
                    break;
                }
                break;
            case NpcType.Gate:
                switch (parm.Status)
                {
                    case "천민":
                        npc = GameObject.Instantiate(GateNpc1);
                    break;

                    case "양인":
                        npc = GameObject.Instantiate(GateNpc2);
                    break;

                    case "상민":
                        npc = GameObject.Instantiate(GateNpc3);
                    break;

                    case "중인":
                        npc = GameObject.Instantiate(GateNpc4);
                    break;

                    case "양반":
                        npc = GameObject.Instantiate(GateNpc5);
                    break;
                    default:
                        npc = GameObject.Instantiate(GateNpc2);
                    break;
                }
                break;
            case NpcType.Run:
            break;
        }

        if(npc != null)
        {
            npc.transform.parent = this.transform;
            AddParameters(npc, parm);
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
            //npcComponent.Style = parm.Style;
            npcComponent.Hometown = parm.Hometown;
            npcComponent.Status = parm.Status;
            npcComponent.Job = parm.Job;
            npcComponent.PassPurpose = parm.PassPurpose;
            npcComponent.Item = parm.Item;
            npcComponent.NpcDaily = parm.NpcDaily;

        }
        else
        {
            Debug.LogWarning("GameObject에서 Npc 컴포넌트를 찾을 수 없습니다: " + npc.name);
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

    public NpcCreateParameter GetParmNPC(int id)
    {
        Npc npc = FindNpcGameObjectById(id).GetComponent<Npc>();

        NpcCreateParameter pram = new NpcCreateParameter(
            npc.npcType,
            npc.ID,
            npc.Name,
            npc.Age,
            npc.Gender,
            //npc.Style,
            npc.Hometown,
            npc.Status,
            npc.Job,
            npc.PassPurpose,
            npc.Item,
            npc.NpcDaily,
            npc.IsVillain
            );

        return pram;


    }
}
