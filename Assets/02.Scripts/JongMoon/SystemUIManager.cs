using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUIManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject systemUI; // �Ͻ����� UI
    public GameObject keyGuideUI; // Ű ���̵�

    public GameObject GummunUI; // ���� UI
    public GameObject Player;
    public GameObject ShowItem;
    public GameObject Buttons;

    public GameObject GetMoneyObj;
    public GameObject ItemButton;

    public GameObject NextNpc;
    public GameObject NextDayUI;
    public GameObject ChepoUI;

    private bool isPaused = false;
    //private bool isKeynoti = false;
    private bool isGummun = false; // ���� UI ����
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
        // Canvas�� ��� �ڽ� GameObject���� ��Ȱ��ȭ�մϴ�.
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

        NextNpc.gameObject.SetActive(true);
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

        if(GameMgr.Instance.AbleNextDay == true && !isJosa)
        {
            NextDayUI.gameObject.SetActive(true);
        }
        else
        {
            NextDayUI.gameObject.SetActive(false);
        }

        if(GameMgr.Instance.AbleChepo)
        {
            ChepoUI.gameObject.SetActive(true);
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
    
    //// Ű �˸� UI ��� �޼��� ����
    //public void ToggleKeynotification()
    //{
    //    isKeynoti = !isKeynoti;
    //    Keynotification.SetActive(isKeynoti);
    //    keyGuideUI.SetActive(!isKeynoti);
    //}
    
    // ���� UI ��� �޼��� �߰�
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
                // ��������Ʈ �Ҵ�
                npcScript.OnNPCProximity += ToggleKeyGuideUI;
            }
        }
    }

    public void DisDelegate(ref List<GameObject> npcs)
    {
        if (npcs == null) return;
        foreach (GameObject npc in npcs)
        {
            if (npc == null) continue;
            ShowKeyPopUp npcScript = npc.GetComponent<ShowKeyPopUp>();
            if (npcScript != null)
            {
                npcScript.OnNPCProximity -= ToggleKeyGuideUI;
            }
        }
    }

    public void DecideGate(int id, bool isPass) //�� ���� ���ο����� P / nP ���� ����
    {
        if (isPass) { NpcManager.Instance.PassGate(id); }
        else { NpcManager.Instance.DeninedGate(id); }
        GameMgr.Instance.AddMaxLight();
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
            GetmoneyUI(); //���� UI ���
        }

        //�˻簡 Ʋ������ misscnt++
        NpcManager.Instance.CheckGate(false, npcid);
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
            GetmoneyUI(); //���� UI ���
        }

        //�˻簡 Ʋ������ misscnt++
        NpcManager.Instance.CheckGate(true, npcid);
    }
    public void OnGetMoneyButton()
    {
        GetMoneyObj.SetActive(false);
        NpcManager.Instance.ChangeToHt(npcid);
        StartCoroutine(DelayDecide());

        GameMgr.Instance.money += 100;
        // �ູ�� ����
        GameMgr.Instance.townHappinessPoint -= 20;
    }

    public void OnNonMoneyButton()
    {
        GetMoneyObj.SetActive(false);
        StartCoroutine(DelayDecide());

        // �ູ�� ����
        GameMgr.Instance.townHappinessPoint += 10;
    }

    IEnumerator DelayDecide()
    {
        yield return new WaitForSeconds(1.5f);
        DecideGate(npcid, IsPass);
    }

   

}
