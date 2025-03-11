using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockMouseControl : MonoBehaviour
{
    public static bool isMouseVisible = false;

    void Start()
    {
        // 시작할 때 마우스 커서를 화면 중앙에 고정하고 숨깁니다.
        LockMouse();
    }

    void Update()
    {
        // Alt 키를 누르면 마우스 커서를 보이게 하고 화면을 고정합니다.
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            UnlockMouse();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement()) // 클릭한 위치가 UI 요소가 아닌 경우
            {
                LockMouse();
            }
        }
    }

    void LockMouse()
    {
        // 마우스 커서를 화면 중앙에 고정하고 숨깁니다.
        isMouseVisible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockMouse()
    {
        // 마우스 커서를 해제하고 보이게 만듭니다.
        isMouseVisible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
