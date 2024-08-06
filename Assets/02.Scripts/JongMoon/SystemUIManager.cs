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

    private bool isPaused = false;
    //private bool isKeynoti = false;
    private bool isGummun = false; // ���� UI ����
    private bool isJosa = false;
    private bool isItem = false;
    Vector3 PlayerPoint;
  
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
    }

    void Update()
    {
        if (!isJosa && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
       
        if (!isPaused && isJosa && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleJosa();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleJosa();
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
        PlayerPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        NpcManager.Instance.ChangeToTalk(NpcManager.Instance.CheckRadiusNPC(PlayerPoint));
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
    }
    public void ToggleItem()
    {
        isItem = !isItem;
        ShowItem.SetActive(isItem);
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
        Debug.Log("�Ҵ�");
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
        Debug.Log("�Ҵ�����");
    }

}
