using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate_InitUI : MonoBehaviour
{
    void Start()
    {
        GameObject obj = GameObject.Find("UI");
        obj.SetActive(false);

        UnlockMouse();
    }

    void UnlockMouse()
    {
        // 마우스 커서를 해제하고 보이게 만듭니다.
        LockMouseControl.isMouseVisible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
