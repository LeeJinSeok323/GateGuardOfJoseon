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
    public static SystemUIManager Instance;

    private bool isPaused = false;
    //private bool isKeynoti = false;
    private bool isGummun = false; // 수색 UI 상태
    private bool isJosa = false;
    private bool isItem = false;

    void Start()
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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    }
    public void ToggleItem()
    {
        isItem = !isItem;
        ShowItem.SetActive(isItem);
    }
}
