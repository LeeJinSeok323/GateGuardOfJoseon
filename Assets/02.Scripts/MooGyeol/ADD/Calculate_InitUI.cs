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
        // ���콺 Ŀ���� �����ϰ� ���̰� ����ϴ�.
        LockMouseControl.isMouseVisible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
