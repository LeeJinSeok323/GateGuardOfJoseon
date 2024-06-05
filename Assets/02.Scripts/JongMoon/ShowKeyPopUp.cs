using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyPopUp : MonoBehaviour
{
    // ������ ���� â����, NPC���� �־����.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("2222222222222222");

            SystemUIManager.Instance.ToggleGummun(); // Gummun UI�� �Ҵ�
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("3333333333333333");

            SystemUIManager.Instance.ToggleGummun(); // Gummun UI�� ����
        }
    }
}

