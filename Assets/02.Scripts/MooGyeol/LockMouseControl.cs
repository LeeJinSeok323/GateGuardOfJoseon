using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouseControl : MonoBehaviour
{
    public static bool isMouseVisible = false;

    void Start()
    {
        // ������ �� ���콺 Ŀ���� ȭ�� �߾ӿ� �����ϰ� ����ϴ�.
        LockMouse();
    }

    void Update()
    {
        // Alt Ű�� ������ ���콺 Ŀ���� ���̰� �ϰ� ȭ���� �����մϴ�.
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
        // ���콺 Ŀ���� ȭ�� �߾ӿ� �����ϰ� ����ϴ�.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockMouse()
    {
        // ���콺 Ŀ���� �����ϰ� ���̰� ����ϴ�.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
