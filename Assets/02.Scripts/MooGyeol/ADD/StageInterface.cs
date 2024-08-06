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
    
    public void CreateGateNpc(int num)
    {
        NpcCreateParameter[] _GateNpcParams = null;

        NpcManager.Instance.SetParameters(ref _GateNpcParams, NpcType.Gate, num);
        for (int i = 0; i < _GateNpcParams.Length; i++)
        {
            GameObject Gatenpc = NpcManager.Instance.CreateNPC(_GateNpcParams[i]);
        }
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
        GameMgr.Instance.AddStageNum();
        _shareFunction.CreateGateNpc(5);

        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);
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
        GameMgr.Instance.AddStageNum();
        _shareFunction.CreateGateNpc(3);

        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
    }
}