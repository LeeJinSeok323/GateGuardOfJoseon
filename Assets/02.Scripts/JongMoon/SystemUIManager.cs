using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUIManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject systemUI; // �Ͻ����� UI
    public GameObject Keynotification;
    public GameObject Defaultbutton;
    public GameObject GummunUI; // ���� UI
    public GameObject Player;
    public static SystemUIManager Instance;

    private bool isPaused = false;
    private bool isKeynoti = false;
    private bool isGummun = false; // ���� UI ����
    private bool isJosa = false;

    void Start()
    {
        // Canvas�� ��� �ڽ� GameObject���� ��Ȱ��ȭ�մϴ�.
        foreach (Transform child in Canvas.transform)
        {
            child.gameObject.SetActive(false);
        }
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
        if (!isPaused && isJosa && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleJosa();
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

    // Ű �˸� UI ��� �޼��� ����
    public void ToggleKeynotification()
    {
        isKeynoti = !isKeynoti;
        Keynotification.SetActive(isKeynoti);
        Defaultbutton.SetActive(!isKeynoti);
    }

    // ���� UI ��� �޼��� �߰�
    public void ToggleGummun()
    {
        isGummun = !isGummun;
        Defaultbutton.SetActive(isGummun);
    }
}
