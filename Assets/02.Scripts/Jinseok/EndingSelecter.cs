using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndingSelecter : MonoBehaviour
{

    public CutSceneLoader cutSceneLoader;
    public VideoPlayer videoPlayer;
    public VideoClip ending1Video;
    public VideoClip ending2Video;
    void OnTriggerEnter(Collider other) {
        Debug.Log("TriggerEnter");
        if(other.gameObject.CompareTag("Player")){
            string endVideo = this.gameObject.name;
            if (endVideo == "Ending 1")
            {
                ChangeVideo(ending1Video);
                Debug.Log("end 1");

            }
            else if (endVideo == "Ending 2")
            {
                ChangeVideo(ending2Video);
            }
            cutSceneLoader.LoadScreen();
            
        }
    }

    void ChangeVideo(VideoClip newClip)
    {
        if (videoPlayer != null && newClip != null)
        {
            videoPlayer.clip = newClip;
        }
        else
        {
            Debug.LogWarning("VideoPlayer 또는 VideoClip이 설정되지 않았습니다.");
        }
    }
}
