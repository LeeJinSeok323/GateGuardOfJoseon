using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocReload : MonoBehaviour
{
    public GameObject Canvas;

    void DocumentReload(){
        //������ ���ε�
        //������ ã�� �޾ƿ���. 
        if (Canvas.transform.Find("Hopae") != null){
            Debug.Log("ȣ�� ã��");
        }
        // Text pass_name = npc.Name;
    }
}
