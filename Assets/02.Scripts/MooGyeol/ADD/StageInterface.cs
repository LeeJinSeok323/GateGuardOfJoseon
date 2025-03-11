using System.Collections.Generic;
using UnityEngine;
using static NpcCreateParameter;

public interface IStageStrategy
{
    void LoadStage();
    void UnloadStage();
    
}

public class StageContext
{
    private IStageStrategy _stageStrategy;

    public void SetStageStrategy(IStageStrategy stageStrategy)
    {
        _stageStrategy = stageStrategy;
    }

    public void LoadStage()
    {
        _stageStrategy?.LoadStage();
    }

    public void UnloadStage()
    {
        _stageStrategy?.UnloadStage();
    }
}

public class ShareFunction
{
    public void CreateGateNpc(int num) //�⺻���� NPC����
    {
        NpcCreateParameter[] _GateNpcParams = null;

        NpcManager.Instance.SetParameters(ref _GateNpcParams, NpcType.Gate, num);
        for (int i = 0; i < _GateNpcParams.Length; i++)
        {
            if(GameMgr.Instance.AblePlague) // �������� �˻�
            {
                // �������� ��� Npc�� Villain�� True�� ����
                if(_GateNpcParams[i].Hometown == GameMgr.Instance.PlageVilage)
                    _GateNpcParams[i].IsVillain = true;
            }

            GameObject Gatenpc = NpcManager.Instance.CreateNPC(_GateNpcParams[i]);
        }
    }

    public void CreateBoolGateNpc(bool vilran) //��,���� NPC����
    {
        NpcCreateParameter npcparam = null;
        NpcManager.Instance.CreateBoolGateNpc(ref npcparam, NpcType.Gate, vilran);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(npcparam);
    }

    public void CreateAlcolNpc(bool isvilran) //���� ���� NPC ����
    {
        NpcCreateParameter Alcoholnpc = null;
        NpcManager.Instance.SetAlcoholParameters(ref Alcoholnpc, NpcType.Gate);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(Alcoholnpc);
    }
    public void CreatePlagueGateNpc(string village) // �ش� ������ ���� Npc ����
    {
        NpcCreateParameter _Plague = null;
        NpcManager.Instance.SetPlagueParameters(ref _Plague, NpcType.Gate, village);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(_Plague);
    }

    public void CreateDangerNpc() //����, ���� ���� NPC ����
    {
        NpcCreateParameter DangerNpc = null;
        NpcManager.Instance.SetDangerParameters(ref DangerNpc, NpcType.Gate);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(DangerNpc);
    }

}

public class Stage1Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {
        // �ʱ�ȭ
        GameMgr.Instance.InitValue();
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //Npc �߰� �۾�
        _shareFunction.CreateBoolGateNpc(false); // �Ϲ� Npc
        _shareFunction.CreateBoolGateNpc(true); // ���� Npc
        _shareFunction.CreateGateNpc(3);

        // UI ��������Ʈ �۾�
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //�������� Num ����
        GameMgr.Instance.AddStageNum(); 
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}

public class Stage2Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {
        // �ʱ�ȭ
        GameMgr.Instance.InitValue();
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //Npc �߰� �۾�
        _shareFunction.CreateGateNpc(5); //�⺻ GateNpc 5��

        // UI ��������Ʈ �۾�
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //�������� Num ����
        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}

public class Stage3Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {
        // �ʱ�ȭ
        GameMgr.Instance.InitValue();
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //���� ����
        GameMgr.Instance.AbleCheckItem = true;

        //Npc �߰� �۾�
        _shareFunction.CreateAlcolNpc(true); // ���� Npc
        _shareFunction.CreateGateNpc(3); // �⺻GateNpc
        _shareFunction.CreateAlcolNpc(true);

        // UI ��������Ʈ �۾�
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //�������� Num ����
        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}

public class Stage4Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {
        // �ʱ�ȭ
        GameMgr.Instance.InitValue();
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //���� ����
        GameMgr.Instance.AbleRunNpc = true;
        GameMgr.Instance.AbleCheckItem = true;

        //Npc �߰� �۾�

        _shareFunction.CreateBoolGateNpc(true);
        _shareFunction.CreateGateNpc(1); // �⺻GateNpc 
        _shareFunction.CreateAlcolNpc(true); // ���� Npc
        _shareFunction.CreateGateNpc(2);  


        // UI ��������Ʈ �۾�
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //�������� Num ����
        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}

public class Stage5Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {
        // �ʱ�ȭ
        GameMgr.Instance.InitValue();
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //��� �ر� ����
        GameMgr.Instance.AbleGetMoney = true;
        GameMgr.Instance.AblePlague = true;
        GameMgr.Instance.AbleCheckItem = true;

        // �������� ����
        GameMgr.Instance.SetPlagueVilage();

        //Npc �߰� �۾�
        _shareFunction.CreatePlagueGateNpc(GameMgr.Instance.PlageVilage); // Ȯ�� ���� Npc
        _shareFunction.CreateGateNpc(1); // �⺻GateNpc 
        _shareFunction.CreateAlcolNpc(false); // ���� Npc (���� �����ƴ�)
        _shareFunction.CreateGateNpc(2);

        // UI ��������Ʈ �۾�
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //�������� Num ����
        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}

public class Stage6Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {

        GameMgr.Instance.AbleGetMoney = true;
        GameMgr.Instance.AblePlague = true;
        GameMgr.Instance.AbleCheckItem = true;

        // �ʱ�ȭ
        GameMgr.Instance.InitValue();
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //Npc �߰� �۾�
        _shareFunction.CreateDangerNpc();
        _shareFunction.CreateAlcolNpc(false);
        _shareFunction.CreateGateNpc(2);
        _shareFunction.CreateDangerNpc();

        // UI ��������Ʈ �۾�
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //�������� Num ����
        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}

public class Stage7Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {
        // �ʱ�ȭ
        GameMgr.Instance.InitValue();
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        // �������� ����
        GameMgr.Instance.SetPlagueVilage();
        GameMgr.Instance.AblePlague= true;
        GameMgr.Instance.AbleCheckItem= true;
        GameMgr.Instance.AbleRunNpc= true;
        GameMgr.Instance.AbleGetMoney= true;
        GameMgr.Instance.AbleChepo= true;

        // Npc �߰� �۾�
        _shareFunction.CreateAlcolNpc(false);
        _shareFunction.CreateGateNpc(2);
        _shareFunction.CreateBoolGateNpc(false);
        _shareFunction.CreateDangerNpc();

        // UI ��������Ʈ �۾�
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //�������� Num ����
        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}