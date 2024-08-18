using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate_InitUI : MonoBehaviour
{
    void Start()
    {
        GameObject obj = GameObject.Find("UI");
        obj.SetActive(false);
    }


    
}
