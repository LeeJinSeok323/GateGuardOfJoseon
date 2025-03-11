using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDay : MonoBehaviour
{
    
    float timeElapsed {get; set;}
    void Start(){
        timeElapsed = 0f;
    }
    void Update(){
        timeElapsed += Time.deltaTime;
    }

    public void ResetTimer(){
        timeElapsed = 0f;
    }
}
