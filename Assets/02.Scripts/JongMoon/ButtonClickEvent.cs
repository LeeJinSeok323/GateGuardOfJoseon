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
        // 버튼에 OnClick 이벤트 리스너 추가
        startButton.onClick.AddListener(OnStartButtonClick);
        creditButton.onClick.AddListener(OnCreditButtonClick);
        endButton.onClick.AddListener(OnEndButtonClick);

        // Door1AnimationController 및 Door2AnimationController 인스턴스를 확인합니다.
        if (door1Controller == null || door2Controller == null)
        {
            Debug.LogError("DoorAnimationController 인스턴스가 설정되지 않았습니다.");
        }
    }

    void OnStartButtonClick()
    {
        // Canvas 비활성화
        canvas.gameObject.SetActive(false);

        // doorAnimationController 인스턴스를 통해 AnimStart 메서드를 호출합니다.
        if (door1Controller != null && door2Controller != null)
        {
            door1Controller.AnimStart();
            door2Controller.AnimStart();
            Debug.Log("Start button clicked, animations started.");
        }
        else
        {
            Debug.LogError("DoorAnimationController 인스턴스가 설정되지 않았습니다.");
        }
    }

    void OnCreditButtonClick()
    {
        Debug.Log("인명부 추가 예정");
    }

    void OnEndButtonClick()
    {
        // 애플리케이션 종료
        Application.Quit();
    }
}
