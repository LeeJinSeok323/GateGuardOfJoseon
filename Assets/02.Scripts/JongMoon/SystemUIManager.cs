using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUIManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject systemUI; // 일시정지 UI
    public GameObject keyGuideUI; // 키 가이드

    public GameObject GummunUI; // 수색 UI
    public GameObject Player;
    public GameObject ShowItem;
    public GameObject Buttons;

    public GameObject GetMoneyObj;
    public GameObject ItemButton;

    public GameObject NextDayUI;
    public GameObject ChepoUI;

    private bool isPaused = false;
    //private bool isKeynoti = false;
    private bool isGummun = false; // 수색 UI 상태
    private bool isJosa = false;
    private bool isItem = false;
    private bool isGetMoney = false;
    Vector3 PPoint;

    private bool IsPass;
    private int npcid;

    private static SystemUIManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Canvas의 모든 자식 GameObject들을 비활성화합니다.
        foreach (Transform child in Canvas.transform)
        {
            child.gameObject.SetActive(false);
        }
        List<GameObject> keyGuides = new List<GameObject>();
        foreach (Transform child in keyGuideUI.transform)
        {
            keyGuides.Add(child.gameObject);
        }

        if(!GameMgr.Instance.AbleCheckItem)
        {
            ItemButton.gameObject.SetActive(false);
        }

        NextDayUI.gameObject.SetActive(false);
        ChepoUI.gameObject.SetActive(false);

    }

    void Update()
    {
        if (!isJosa && !isItem && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (!isPaused && isJosa && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleJosa();
        }
        if (!isPaused && isItem && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleItem();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        systemUI.SetActive(isPaused);
    }

    public void ToggleJosa()
    {
        isJosa = !isJosa;
        GummunUI.SetActive(isJosa);
        PPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        NpcManager.Instance.ChangeToTalk(NpcManager.Instance.CheckRadiusNPC(PPoint));
    }
    
    //// 키 알림 UI 토글 메서드 수정
    //public void ToggleKeynotification()
    //{
    //    isKeynoti = !isKeynoti;
    //    Keynotification.SetActive(isKeynoti);
    //    keyGuideUI.SetActive(!isKeynoti);
    //}
    
    // 수색 UI 토글 메서드 추가
    public void ToggleKeyGuideUI()
    {
        isGummun = !isGummun;
        keyGuideUI.SetActive(isGummun);
        Buttons.SetActive(isGummun);
        if (GameMgr.Instance.AbleCheckItem) ItemButton.gameObject.SetActive(isGummun);
    }
    public void ToggleItem()
    {
        isItem = !isItem;
        ShowItem.SetActive(isItem);
    }

    public void GetmoneyUI()
    {
        isGetMoney = !isGetMoney;
        GetMoneyObj.gameObject.SetActive(isGetMoney);
    }

    public void OnDelegate(ref List<GameObject> npcs)
    {
        foreach (GameObject npc in npcs)
        {
            ShowKeyPopUp npcScript = npc.GetComponent<ShowKeyPopUp>();
            if (npcScript != null)
            {
                // 델리게이트 할당
                npcScript.OnNPCProximity += ToggleKeyGuideUI;
            }
        }
    }

    public void DisDelegate(ref List<GameObject> npcs)
    {
        foreach (GameObject npc in npcs)
        {
            ShowKeyPopUp npcScript = npc.GetComponent<ShowKeyPopUp>();
            if (npcScript != null)
            {
                npcScript.OnNPCProximity -= ToggleKeyGuideUI;
            }
        }
    }

    public void DecideGate(int id, bool isPass) //참 거짓 여부에따라 P / nP 여부 결정
    {
        if (isPass) { NpcManager.Instance.PassGate(id); }
        else { NpcManager.Instance.DeninedGate(id); }
    }

    public void OnClickPassButton()
    {
        IsPass = true;
        PPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        npcid = NpcManager.Instance.CheckRadiusNPC(PPoint);

        if (!GameMgr.Instance.AbleGetMoney)
        {
            DecideGate(npcid, IsPass);
        }
        else
        {
            GetmoneyUI(); //수금 UI 띄움
        }
    }

    public void OnClickDeninedButton()
    {
        IsPass = false;
        PPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        npcid = NpcManager.Instance.CheckRadiusNPC(PPoint);
        if (!GameMgr.Instance.AbleGetMoney)
        {
            DecideGate(npcid, IsPass);
        }
        else
        {
            GetmoneyUI(); //수금 UI 띄움
        }
    }
    public void OnGetMoneyButton()
    {
        GetMoneyObj.SetActive(false);
        NpcManager.Instance.ChangeToHt(npcid);
        StartCoroutine(DelayDecide());

        GameMgr.Instance.money += 100;
        // TODO 마을 행복도 감소
    }

    public void OnNonMoneyButton()
    {
        GetMoneyObj.SetActive(false);
        StartCoroutine(DelayDecide());
        //TODO 마을 행복도 증가
    }

    IEnumerator DelayDecide()
    {
        yield return new WaitForSeconds(1.5f);
        DecideGate(npcid, IsPass);
    }

   

}
