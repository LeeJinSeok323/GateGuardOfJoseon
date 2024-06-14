using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            isMouseVisible = !isMouseVisible;
            if (isMouseVisible)
            {
                UnlockMouse();
            }
            else
            {
                LockMouse();
            }
        }
    }

    void LockMouse()
    {
        // 마우스 커서를 화면 중앙에 고정하고 숨깁니다.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockMouse()
    {
        // 마우스 커서를 해제하고 보이게 만듭니다.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
