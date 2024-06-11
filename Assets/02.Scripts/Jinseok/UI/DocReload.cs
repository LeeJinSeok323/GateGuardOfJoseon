using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocReload : MonoBehaviour
{
    public GameObject Canvas;

    void DocumentReload(){
        //통행증 리로드
        //통행증 찾아 받아오기. 
        if (Canvas.transform.Find("Hopae") != null){
            Debug.Log("호패 찾음");
        }
        // Text pass_name = npc.Name;
    }
}
