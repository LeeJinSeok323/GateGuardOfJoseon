using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyPopUp : MonoBehaviour
{
    // 가까이 오면 창띄우기, NPC한테 넣어야함.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("2222222222222222");

            SystemUIManager.Instance.ToggleGummun(); // Gummun UI를 켠다
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("3333333333333333");

            SystemUIManager.Instance.ToggleGummun(); // Gummun UI를 끈다
        }
    }
}

