using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private StageContext _stageContext;
    private GameObject Player;

    private void Start()
    {
        _stageContext = new StageContext();
        SetStage(GameMgr.Instance.GetStageNum());
        Player = GameObject.FindWithTag("Player");
    }

    public void SetStage(int stageNumber)
    {
        switch (stageNumber) 
        { 
            case 1: // D-7 (튜토리얼, GateNpc 참 1명, 거짓 1명)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage1Strategy>());
                break;
            case 2: // D-6 (GateNpc 5명)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage2Strategy>());
                break;
            case 3: // D-5 (소지품 검사 가능 해금, 금주령 시작, GateNpc 3명, AlcholNpc 2명)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage3Strategy>());
                break;
            case 4: // D-4 (도주 기능, 도주자 구금 기능 해금, GateNpc 4명, AlcholNpc 1명)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage4Strategy>());
                break;
            case 5:// D-3 (역병지역 해금 ,금주령 해제, 통행세 수금 기능 해금, PlagueNpc 1명, GateNpc 2명 ,AlcholNpc 1명)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage5Strategy>());
                break;
            case 6: // D-2 (위험한 물건 소지 구금, GateNpc 2명, DangerNpc 2명 PlagueNpc 1명)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage6Strategy>());
                break;
            case 7: // D-1 (역병지역 변경, GateNpc 3명, AlcholNpc 1명, DangerNpc 1명)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage7Strategy>());
                break;
        }

        _stageContext.LoadStage();
    }

    public void OnChangeScene()
    {
        _stageContext.UnloadStage();
        NpcManager.Instance.ClearNPCs();
        Destroy(Player);
        SceneManager.LoadScene("Calculate_Scene");
    }

}
