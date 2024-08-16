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
            case 1: // D-7 (Ʃ�丮��, GateNpc �� 1��, ���� 1��)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage1Strategy>());
                break;
            case 2: // D-6 (GateNpc 5��)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage2Strategy>());
                break;
            case 3: // D-5 (����ǰ �˻� ���� �ر�, ���ַ� ����, GateNpc 3��, AlcholNpc 2��)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage3Strategy>());
                break;
            case 4: // D-4 (���� ���, ������ ���� ��� �ر�, GateNpc 4��, AlcholNpc 1��)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage4Strategy>());
                break;
            case 5:// D-3 (�������� �ر� ,���ַ� ����, ���༼ ���� ��� �ر�, PlagueNpc 1��, GateNpc 2�� ,AlcholNpc 1��)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage5Strategy>());
                break;
            case 6: // D-2 (������ ���� ���� ����, GateNpc 2��, DangerNpc 2�� PlagueNpc 1��)
                _stageContext.SetStageStrategy(gameObject.AddComponent<Stage6Strategy>());
                break;
            case 7: // D-1 (�������� ����, GateNpc 3��, AlcholNpc 1��, DangerNpc 1��)
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
