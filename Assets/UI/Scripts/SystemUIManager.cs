using UnityEngine;
using UnityEngine.UI;

public class SystemUIManager : MonoBehaviour
{
    public GameObject systemUIRoot;
    public GameObject GummunUIRoot;
    public GameObject Canvas;
    public GameObject Defaultbutton;
    public GameObject Keynoti;

    private bool isPaused = false;
    private bool isGummun = false;
    private bool isNoti = false;
    void Start()
    {
        // Canvas의 모든 자식 GameObject들을 비활성화합니다.
        foreach (Transform child in Canvas.transform)
        {
            child.gameObject.SetActive(false);
        }
        Defaultbutton.SetActive(true);
        // 일시정지 UI와 GummunUI를 비활성화합니다.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                Debug.Log("게임 재개");
            }
            else
            {
                Debug.Log("게임 일시정지");
            }
            TogglePause();
        }
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("수색 UI 띄우기");
                ToggleGummun();
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                Debug.Log("조작법 보여주기");
                ToggleKeyNoti();
            }
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        systemUIRoot.SetActive(isPaused);
        Defaultbutton.SetActive(!isGummun);
        Keynoti.SetActive(false);
    }

    public void ToggleGummun()
    {
        isGummun = !isGummun;
        GummunUIRoot.SetActive(isGummun);
        Defaultbutton.SetActive(!isGummun);
        Keynoti.SetActive(false);
    }

    public void ToggleKeyNoti()
    {  
        isNoti = !isNoti;
        Keynoti.SetActive(false);
        Defaultbutton.SetActive(false);
    }
    // 추가 UI 제어 메소드
}
