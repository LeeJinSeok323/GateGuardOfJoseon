using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHD : MonoBehaviour
{
    public PopHandler popupWindow;

    public void OnButtonClick()
    {
        // �˾� â�� ǥ���մϴ�.
        popupWindow.Show();
    }
}

