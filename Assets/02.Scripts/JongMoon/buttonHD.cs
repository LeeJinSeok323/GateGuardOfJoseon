using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHD : MonoBehaviour
{
    public PopHandler popupWindow;

    public void OnButtonClick()
    {
        // 팝업 창을 표시합니다.
        popupWindow.Show();
    }
}

