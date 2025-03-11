using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void OnStageScene()
    {
        SceneManager.LoadScene("Main_Stage1");
    }
}

