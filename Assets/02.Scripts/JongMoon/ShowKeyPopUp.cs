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
  
            SystemUIManager.Instance.ToggleGummun();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
       
            SystemUIManager.Instance.ToggleGummun();
        }
    }
}

