using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{
    public Canvas canvas;
    public Button startButton;
    public Button creditButton;
    public Button endButton;
    public Door1AnimationController door1Controller;
    public Door2AnimationController door2Controller;

    void Start()
    {
        // ��ư�� OnClick �̺�Ʈ ������ �߰�
        startButton.onClick.AddListener(OnStartButtonClick);
        creditButton.onClick.AddListener(OnCreditButtonClick);
        endButton.onClick.AddListener(OnEndButtonClick);

        // Door1AnimationController �� Door2AnimationController �ν��Ͻ��� Ȯ���մϴ�.
        if (door1Controller == null || door2Controller == null)
        {
            Debug.LogError("DoorAnimationController �ν��Ͻ��� �������� �ʾҽ��ϴ�.");
        }
    }

    void OnStartButtonClick()
    {
        // Canvas ��Ȱ��ȭ
        canvas.gameObject.SetActive(false);

        // doorAnimationController �ν��Ͻ��� ���� AnimStart �޼��带 ȣ���մϴ�.
        if (door1Controller != null && door2Controller != null)
        {
            door1Controller.AnimStart();
            door2Controller.AnimStart();
            Debug.Log("Start button clicked, animations started.");
        }
        else
        {
            Debug.LogError("DoorAnimationController �ν��Ͻ��� �������� �ʾҽ��ϴ�.");
        }
    }

    void OnCreditButtonClick()
    {
        Debug.Log("�θ�� �߰� ����");
    }

    void OnEndButtonClick()
    {
        // ���ø����̼� ����
        Application.Quit();
    }
}
