using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private StageContext _stageContext;

    private void Start()
    {
        _stageContext = new StageContext();
        SetStage(GameMgr.Instance.GetStageNum());
    }

    public void SetStage(int stageNumber)
    {
        switch (stageNumber) 
        { 
            case 1:
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage1Strategy>());
                break;
            case 2:
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage2Strategy>());
                break;
        }

        _stageContext.LoadStage();
    }

    public void OnChangeScene()
    {
        _stageContext.UnloadStage();
        NpcManager.Instance.ClearNPCs();
        SceneManager.LoadScene("Calculate_Scene");
    }

}
