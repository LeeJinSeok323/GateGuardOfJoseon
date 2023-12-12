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
        // Canvas�� ��� �ڽ� GameObject���� ��Ȱ��ȭ�մϴ�.
        foreach (Transform child in Canvas.transform)
        {
            child.gameObject.SetActive(false);
        }
        Defaultbutton.SetActive(true);
        // �Ͻ����� UI�� GummunUI�� ��Ȱ��ȭ�մϴ�.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                Debug.Log("���� �簳");
            }
            else
            {
                Debug.Log("���� �Ͻ�����");
            }
            TogglePause();
        }
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("���� UI ����");
                ToggleGummun();
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                Debug.Log("���۹� �����ֱ�");
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
    // �߰� UI ���� �޼ҵ�
}
