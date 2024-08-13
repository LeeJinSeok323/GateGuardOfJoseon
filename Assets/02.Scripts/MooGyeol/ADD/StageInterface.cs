using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Playables;
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
    public void CreateGateNpc(int num) //기본적인 NPC생성
    {
        NpcCreateParameter[] _GateNpcParams = null;

        NpcManager.Instance.SetParameters(ref _GateNpcParams, NpcType.Gate, num);
        for (int i = 0; i < _GateNpcParams.Length; i++)
        {
            GameObject Gatenpc = NpcManager.Instance.CreateNPC(_GateNpcParams[i]);
        }
    }
    public void CreateAlcolNpc() //술을 가진 NPC 생성
    {
        NpcCreateParameter Alcoholnpc = null;
        NpcManager.Instance.SetAlcoholParameters(ref Alcoholnpc, NpcType.Gate);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(Alcoholnpc);
    }
    
    public void CreateDangerNpc() //아편, 도검 소지 NPC 생성
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
        Debug.Log("Stage 1 Loaded");

        //Npc 추가 함수들
        _shareFunction.CreateDangerNpc();
        //_shareFunction.CreateAlcolNpc();
        _shareFunction.CreateDangerNpc();
        _shareFunction.CreateDangerNpc();
        //_shareFunction.CreateGateNpc(3);

        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs); // npc에게 델리게이트 추가


        GameMgr.Instance.AddStageNum(); //스테이지 Num 증가
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
    }
}

public class Stage2Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();
    List<GameObject> npcs;
    SystemUIManager UI;

    public void LoadStage()
    {
        Debug.Log("Stage 2 Loaded");

        _shareFunction.CreateAlcolNpc();
        _shareFunction.CreateGateNpc(2);

        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
    }
}