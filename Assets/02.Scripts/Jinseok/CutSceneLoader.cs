using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class CutSceneLoader : MonoBehaviour
{
    public Canvas ui1;
    public Canvas ui2;    
    public Canvas ui3;
    public RawImage screen;
    public VideoPlayer vp;
    public string sceneName = "Main_Stage1";
    
    public void LoadScreen(){
        screen.rectTransform.anchoredPosition = new Vector2(0, 0);
        ToggleUI(false);
        PlayCutScene();
    }
    void PlayCutScene(){
        vp.Play();
        vp.loopPointReached += endVideo;
        
    }

    void endVideo(UnityEngine.Video.VideoPlayer vp){
        if(!sceneName.Equals("")){
            SceneManager.LoadScene(sceneName);
        }
        else{
            ToggleUI(true);
            screen.rectTransform.anchoredPosition = new Vector2(4000, 0);
        }
    }

    public void OnStartBtnClick(){
        LoadScreen();
    }
  
    public void ToggleUI(bool doActive){
        if(ui1 != null){
            ui1.gameObject.SetActive(doActive);
        }
        if(ui2 != null){
            ui2.gameObject.SetActive(doActive);
        }
        if(ui3 != null){
            ui3.gameObject.SetActive(doActive);
        }
    }
    
}
