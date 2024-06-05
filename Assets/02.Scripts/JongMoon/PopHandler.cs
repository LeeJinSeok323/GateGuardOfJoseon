using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ��ü�� ��Ȱ��ȭ �մϴ�.
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // �г��� Ȱ��ȭ�մϴ�.
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        // �г��� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ESC Ű�� ���� �� Hide �޼��带 ȣ���մϴ�.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
}
