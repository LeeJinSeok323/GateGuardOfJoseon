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
    public void CreateGateNpc(int num) //기본적인 NPC생성
    {
        NpcCreateParameter[] _GateNpcParams = null;

        NpcManager.Instance.SetParameters(ref _GateNpcParams, NpcType.Gate, num);
        for (int i = 0; i < _GateNpcParams.Length; i++)
        {
            if(GameMgr.Instance.AblePlague) // 역병지역 검사
            {
                // 역병지역 출신 Npc의 Villain을 True로 변경
                if(_GateNpcParams[i].Hometown == GameMgr.Instance.PlageVilage)
                    _GateNpcParams[i].IsVillain = true;
            }

            GameObject Gatenpc = NpcManager.Instance.CreateNPC(_GateNpcParams[i]);
        }
    }

    public void CreateBoolGateNpc(bool vilran) //참,거짓 NPC생성
    {
        NpcCreateParameter npcparam = null;
        NpcManager.Instance.CreateBoolGateNpc(ref npcparam, NpcType.Gate, vilran);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(npcparam);
    }

    public void CreateAlcolNpc(bool isvilran) //술을 가진 NPC 생성
    {
        NpcCreateParameter Alcoholnpc = null;
        NpcManager.Instance.SetAlcoholParameters(ref Alcoholnpc, NpcType.Gate);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(Alcoholnpc);
    }
    public void CreatePlagueGateNpc(string village) // 해당 마을이 역병 Npc 생성
    {
        NpcCreateParameter _Plague = null;
        NpcManager.Instance.SetPlagueParameters(ref _Plague, NpcType.Gate, village);
        GameObject Gatenpc = NpcManager.Instance.CreateNPC(_Plague);
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
        // 초기화
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //Npc 추가 작업
        _shareFunction.CreateBoolGateNpc(false); // 일반 Npc
        _shareFunction.CreateBoolGateNpc(true); // 빌런 Npc
        _shareFunction.CreateGateNpc(3);

        // UI 델리게이트 작업
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //스테이지 Num 증가
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
        // 초기화
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //Npc 추가 작업
        _shareFunction.CreateGateNpc(5); //기본 GateNpc 5명

        // UI 델리게이트 작업
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //스테이지 Num 증가
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
        // 초기화
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //게임 설정
        GameMgr.Instance.AbleCheckItem = true;

        //Npc 추가 작업
        _shareFunction.CreateAlcolNpc(true); // 알콜 Npc
        _shareFunction.CreateGateNpc(3); // 기본GateNpc
        _shareFunction.CreateAlcolNpc(true);

        // UI 델리게이트 작업
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //스테이지 Num 증가
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
        // 초기화
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //게임 설정
        GameMgr.Instance.AbleRunNpc = true;

        //Npc 추가 작업

        _shareFunction.CreateBoolGateNpc(true);
        _shareFunction.CreateGateNpc(1); // 기본GateNpc 
        _shareFunction.CreateAlcolNpc(true); // 알콜 Npc
        _shareFunction.CreateGateNpc(2);  


        // UI 델리게이트 작업
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //스테이지 Num 증가
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
        // 초기화
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //기능 해금 설정
        GameMgr.Instance.AbleGetMoney = true;
        GameMgr.Instance.AblePlague = true;

        // 역병지역 설정
        GameMgr.Instance.SetPlagueVilage();

        //Npc 추가 작업
        _shareFunction.CreatePlagueGateNpc(GameMgr.Instance.PlageVilage); // 확정 역병 Npc
        _shareFunction.CreateGateNpc(1); // 기본GateNpc 
        _shareFunction.CreateAlcolNpc(false); // 알콜 Npc (이제 빌런아님)
        _shareFunction.CreateGateNpc(2);

        // UI 델리게이트 작업
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //스테이지 Num 증가
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
        // 초기화
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        //Npc 추가 작업
        _shareFunction.CreateDangerNpc();
        _shareFunction.CreateAlcolNpc(false);
        _shareFunction.CreateGateNpc(2);
        _shareFunction.CreateDangerNpc();

        // UI 델리게이트 작업
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //스테이지 Num 증가
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
        // 초기화
        GameMgr.Instance.InitUI();
        GameMgr.Instance.InitLight();

        // 역병지역 변경
        GameMgr.Instance.SetPlagueVilage();

        // Npc 추가 작업
        _shareFunction.CreateAlcolNpc(false);
        _shareFunction.CreateGateNpc(2);
        _shareFunction.CreateBoolGateNpc(false);
        _shareFunction.CreateDangerNpc();

        // UI 델리게이트 작업
        UI = GameObject.FindWithTag("UIManager").GetComponent<SystemUIManager>();
        npcs = NpcManager.Instance.GetNpc();
        UI.OnDelegate(ref npcs);

        //스테이지 Num 증가
        GameMgr.Instance.AddStageNum();
    }

    public void UnloadStage()
    {
        UI.DisDelegate(ref npcs);
        GameMgr.Instance.ResetNpcID();
    }
}