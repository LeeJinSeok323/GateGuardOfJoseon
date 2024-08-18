using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NpcCreateParameter;

public class NpcManager : MonoBehaviour
{
    public List<Dictionary<string, object>> data_Dialog;
    private static NpcManager _instance;

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

    private List<GameObject> npcs = new List<GameObject>();

    private enum VilranType
    {
        Name, //�̸�
        Age, //����
        Home //����
    }

    public Vector3 PassPoint;
    public Vector3 DeninedPoint;
    public Vector3 RunPoint;

    public Transform GatePoint;

    public static NpcManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NpcManager>();
                if(_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<NpcManager>();
                    singletonObject.name = typeof(NpcManager).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // ���� �ν��Ͻ��� �̹� �����ϰ� ���� �ν��Ͻ��� �ٸ��ٸ�, �ߺ��� �ν��Ͻ��� �ı�
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        GatePoint = GameObject.FindGameObjectWithTag("Point").transform;
    }

    public GameObject CreateNPC(NpcCreateParameter parm)
    {
        //�ε�� ���̺����� Type�� NpcNumber ����
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

                    case "상민":
                        npc = GameObject.Instantiate(StayNpc2);
                    break;

                    case "중인":
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

                    case "상민":
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

                    case "양민":
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
            npcs.Add(npc);
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
            npcComponent.IsVillain = parm.IsVillain;
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
        DeactivateGameObjectWithDelay(npc);
    }

    IEnumerator DeactivateGameObjectWithDelay(GameObject npc)
    {
        yield return new WaitForSeconds(10f); // 3�� ���
        if(npc != null)
        {
            npc.gameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
        }
        
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
        if (id == 9999) return;
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate != null)
        {
            if (gate.state == NpcBehavior_Gate.State.IDLE)
            {
                gate.state = NpcBehavior_Gate.State.TALK;
            }

        }
    }

    public void ChangeToHt(int id)
    {
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate != null)
        {
            if (gate.state == NpcBehavior_Gate.State.IDLE)
            {
                gate.state = NpcBehavior_Gate.State.HIT;
            }
        }
    }

    public void ChangeToRun(int id)
    {
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate != null)
        {
            if (gate.state == NpcBehavior_Gate.State.IDLE)
            {
                gate.state = NpcBehavior_Gate.State.RUN;
            }
        }
    }

    public void PassGate(int id)
    {   
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        if (gate == null) return;

        if (gate.state == NpcBehavior_Gate.State.IDLE)
        {
            gate.currentTarget = PassPoint;
            gate.state = NpcBehavior_Gate.State.WALK;
            StartCoroutine(DeactivateGameObjectWithDelay(npc));
        }
    }

    public void DeninedGate(int id)
    {
        GameObject npc = FindNpcGameObjectById(id);
        NpcBehavior_Gate gate = npc.GetComponent<NpcBehavior_Gate>();
        Npc n = npc.GetComponent<Npc>();
        bool villain = n.IsVillain;
        bool isRun = Random.Range(0, 4) >= 1;

        if (gate == null) return;
        if (gate.state != NpcBehavior_Gate.State.IDLE) return;
        
        if(villain && GameMgr.Instance.AbleRunNpc && isRun) //75% Ȯ���� ���������� �� �پ��
        {
            gate.currentTarget = RunPoint;
            gate.state = NpcBehavior_Gate.State.RUN;
            StartCoroutine(DeactivateGameObjectWithDelay(npc));
            return;
        }

        gate.currentTarget = DeninedPoint;
        gate.state = NpcBehavior_Gate.State.WALK;
        StartCoroutine(DeactivateGameObjectWithDelay(npc));
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

    public void ClearNPCs()
    {
        // ����Ʈ�� �ִ� NPC���� �����մϴ�.
        foreach (GameObject npc in npcs)
        {
            if (npc != null)
            {
                Destroy(npc);
            }
        }
        npcs.Clear();
    }

    public void SetParameters(ref NpcCreateParameter[] npcArray, NpcType type, int npcNumber)
    {
        NpcInfoGenerater n = NpcInfoGenerater.Instance;
        int npcCnt = GameMgr.Instance.NpcID;

        npcArray = new NpcCreateParameter[npcNumber];

        for (int i = 0; i < npcNumber; i++)
        {
            bool isVilran = Random.Range(0, 2) == 1;
            string name = n.nameTable[npcCnt];
            string age = n.ageTable[npcCnt];
            string gender = n.genderTable[npcCnt];
            string status = n.statusTable[npcCnt];
            string home = n.homeTable[npcCnt];
            string job = n.jobTable[npcCnt];
            string passPurpose = n.passPurposeTable[npcCnt];
            string item = n.itemTable[npcCnt];
            string dailyRoutine = n.npcDailyTable[npcCnt];

            if (isVilran)
            {
                ApplyVilranModifications(ref name, ref age, ref home, ref item, n, npcCnt);
            }

            npcArray[i] = new NpcCreateParameter(
                type,
                npcCnt,
                name,
                age,
                gender,
                status,
                home,
                job,
                passPurpose,
                item,
                dailyRoutine,
                isVilran
            );

            npcCnt++;
        }

        GameMgr.Instance.NpcID = npcCnt;
    }

    public void CreateBoolGateNpc(ref NpcCreateParameter npc, NpcType type, bool vilran)
    {
        // ������ ��, ���� Npc ����
        NpcInfoGenerater n = NpcInfoGenerater.Instance;
        int npcCnt = GameMgr.Instance.NpcID;

        bool isVilran = vilran;
        string name = n.nameTable[npcCnt];
        string age = n.ageTable[npcCnt];
        string gender = n.genderTable[npcCnt];
        string status = n.statusTable[npcCnt];
        string home = n.homeTable[npcCnt];
        string job = n.jobTable[npcCnt];
        string passPurpose = n.passPurposeTable[npcCnt];
        string item = n.itemTable[npcCnt];
        string dailyRoutine = n.npcDailyTable[npcCnt];

        if (isVilran)
        {
            ApplyVilranModifications(ref name, ref age, ref home, ref item, n, npcCnt);
        }

        npc = new NpcCreateParameter(
            type,
            npcCnt,
            name,
            age,
            gender,
            status,
            home,
            job,
            passPurpose,
            item,
            dailyRoutine,
            isVilran
        );

        GameMgr.Instance.AddNpcID();
    }

    public void SetPlagueParameters(ref NpcCreateParameter npc, NpcType type, string vilage)
    {
        NpcInfoGenerater n = NpcInfoGenerater.Instance;
        int npcCnt = GameMgr.Instance.NpcID;
        n.homeTable[npcCnt] = vilage;

        bool isVilran = true;
        string name = n.nameTable[npcCnt];
        string age = n.ageTable[npcCnt];
        string gender = n.genderTable[npcCnt];
        string status = n.statusTable[npcCnt];
        string home = vilage;
        string job = n.jobTable[npcCnt];
        string passPurpose = n.passPurposeTable[npcCnt];
        string item = n.itemTable[npcCnt];
        string dailyRoutine = n.npcDailyTable[npcCnt];

        npc = new NpcCreateParameter(
            type,
            npcCnt,
            name,
            age,
            gender,
            status,
            home,
            job,
            passPurpose,
            item,
            dailyRoutine,
            isVilran
            );

        GameMgr.Instance.AddNpcID();
    }


    private void ApplyVilranModifications(ref string name, ref string age, ref string home, ref string item, NpcInfoGenerater n, int npcCnt)
    {
        int vilranType = Random.Range(0, 3);

        switch ((VilranType)vilranType)
        {
            case VilranType.Name:
                name = n.nameTable[npcCnt + 10];
                break;
            case VilranType.Age:
                age = n.ageTable[npcCnt + 10];
                break;
            case VilranType.Home:
                home = n.homeTable[npcCnt + 10];
                break;
        }
    }

    public void SetAlcoholParameters(ref NpcCreateParameter Alcoholnpc, NpcType type)
    {
        NpcInfoGenerater n = NpcInfoGenerater.Instance;
        int npcCnt = GameMgr.Instance.NpcID;

        bool isVilran = true;
        string name = n.nameTable[npcCnt];
        string age = n.ageTable[npcCnt];
        string gender = n.genderTable[npcCnt];
        string status = n.statusTable[npcCnt];
        string home = n.homeTable[npcCnt];
        string job = n.jobTable[npcCnt];
        string passPurpose = n.passPurposeTable[npcCnt];
        string item = n.itemTable[npcCnt] + ", 술";
        string dailyRoutine = n.npcDailyTable[npcCnt];


        Alcoholnpc = new NpcCreateParameter(
            type,
            npcCnt,
            name,
            age,
            gender,
            status,
            home,
            job,
            passPurpose,
            item,
            dailyRoutine,
            isVilran
            );

        GameMgr.Instance.AddNpcID();
    }

    public void SetDangerParameters(ref NpcCreateParameter DangerNpc, NpcType type)
    {
        NpcInfoGenerater n = NpcInfoGenerater.Instance;
        int npcCnt = GameMgr.Instance.NpcID;

        int vilran = Random.Range(0, 3);

        bool isVilran = true;
        string name = n.nameTable[npcCnt];
        string age = n.ageTable[npcCnt];
        string gender = n.genderTable[npcCnt];
        string status = n.statusTable[npcCnt];
        string home = n.homeTable[npcCnt];
        string job = n.jobTable[npcCnt];
        string passPurpose = n.passPurposeTable[npcCnt];
        string item = n.itemTable[npcCnt];
        string dailyRoutine = n.npcDailyTable[npcCnt];

        switch (vilran)
        {
            case 0:
                item += ", 아편";
                break;
            case 1:
                item += ", 도검";
                break;
            case 2:
                item += ", 아편, 도검";
                break;
        }

        DangerNpc = new NpcCreateParameter(
            type,
            npcCnt,
            name,
            age,
            gender,
            status,
            home,
            job,
            passPurpose,
            item,
            dailyRoutine,
            isVilran
            );

        GameMgr.Instance.AddNpcID();
    }

    public List<GameObject> GetNpc()
    {
        return npcs;
    }

    public int CheckRadiusNPC(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 2.0f);
        float closestDistance = Mathf.Infinity;
        int id = 9999;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NPC"))
            {
                float distance = Vector3.Distance(position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    id = GetIdByObject(collider.gameObject);
                }
            }
        }
        return id;
    }

    public Npc CheckRadiusNPCObject(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 2.0f);
        float closestDistance = Mathf.Infinity;
        GameObject closestNPC = null;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NPC"))
            {
                float distance = Vector3.Distance(position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNPC = collider.gameObject;
                }
            }
        }

        return closestNPC.GetComponent<Npc>();

    }

    public bool CheckGate(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 8.0f);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Npc npcComponent = colliders[i].GetComponent<Npc>();
                if (npcComponent != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
