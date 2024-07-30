using System.Collections;
using System.Collections.Generic;
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

    public void LoadStage()
    {
        Debug.Log("Stage 1 Loaded");
        NpcManager.Instance.AddNum();
        _shareFunction.CreateGateNpc(5);
    }

    public void UnloadStage()
    {
        Debug.Log("Stage 1 Unloaded");
    }
}

public class Stage2Strategy : MonoBehaviour, IStageStrategy
{
    ShareFunction _shareFunction = new ShareFunction();

    public void LoadStage()
    {
        Debug.Log("Stage 2 Loaded");
        NpcManager.Instance.AddNum();
        _shareFunction.CreateGateNpc(3);

    }

    public void UnloadStage()
    {
        
        Debug.Log("Stage 2 Unloaded");
 
    }
}

