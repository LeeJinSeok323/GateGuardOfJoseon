using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDay : MonoBehaviour
{
    public float Timer;
    public string SceneSelecter;

    void FixedUpdate()
    {
        Timer -= Time.deltaTime;
        //Debug.Log(Timer+"초 남았습니다.");
        if (Timer <= 0)
        {
            //SceneManager.LoadScene(SceneSelecter);
        }
    }
}
