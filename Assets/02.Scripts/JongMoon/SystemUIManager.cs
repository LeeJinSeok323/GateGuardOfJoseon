using UnityEngine;

public class SystemUIManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject PauseUI; // 일시정지 UI
    public GameObject Keynotification;
    public GameObject Defaultbutton;
    public GameObject GummunUI; // 수색 UI
    public GameObject Player;
    public static SystemUIManager Instance;

    private bool isPaused = false;  
    private bool isKeynoti = false;
    private bool isGummun = false; // 수색 UI 상태
    private bool isJosa = false;

    void Start()
    {
        // Canvas의 모든 자식 GameObject들을 비활성화합니다.
        foreach (Transform child in Canvas.transform)
        {
            child.gameObject.SetActive(false);
        }

        //Player = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        if (!isJosa && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (!isPaused && Input.GetKeyDown(KeyCode.F1))
        {
            ToggleKeynotification();
        }
        if(!isPaused && isJosa)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleJosa();
            }
        }
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    public void ToggleGummun()
    {
        isGummun = !isGummun;
        Defaultbutton.SetActive(isGummun);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        PauseUI.SetActive(isPaused);
    }

    public void ToggleJosa()
    {
        isJosa = !isJosa;
        GummunUI.SetActive(isJosa);
    }

    // 키 알림 UI 토글 메서드 수정
    public void ToggleKeynotification()
    {
        isKeynoti = !isKeynoti;
        Keynotification.SetActive(isKeynoti);
    }
}
