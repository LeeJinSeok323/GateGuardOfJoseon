using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            UnlockMouse();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement()) // Ŭ���� ��ġ�� UI ��Ұ� �ƴ� ���
            {
                LockMouse();
            }
        }
    }

    void LockMouse()
    {
        // ���콺 Ŀ���� ȭ�� �߾ӿ� �����ϰ� ����ϴ�.
        isMouseVisible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockMouse()
    {
        // ���콺 Ŀ���� �����ϰ� ���̰� ����ϴ�.
        isMouseVisible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
