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

