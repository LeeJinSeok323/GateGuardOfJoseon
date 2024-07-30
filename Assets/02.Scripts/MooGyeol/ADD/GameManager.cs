using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private StageContext _stageContext;

    private void Start()
    {
        _stageContext = new StageContext();
        SetStage(NpcManager.Instance.GetNum());
    }

    public void SetStage(int stageNumber)
    {
        _stageContext.UnloadStage();

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

}
