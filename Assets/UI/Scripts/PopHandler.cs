using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 객체를 비활성화 합니다.
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // 패널을 활성화합니다.
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        // 패널을 비활성화합니다.
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ESC 키를 누를 때 Hide 메서드를 호출합니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
}
