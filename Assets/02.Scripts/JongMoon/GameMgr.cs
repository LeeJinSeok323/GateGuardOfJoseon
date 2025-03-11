using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMgr : MonoBehaviour
{
    private static GameMgr _instance;

    public static GameMgr Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<GameMgr>();
                singletonObject.name = typeof(GameMgr).ToString() + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    /// <summary>
    /// 변수들
    /// </summary>
    public int townHappinessPoint = 40; // 마을 행복도
    public int bossSatisfaction = 30; // 탐관오리 만족도
    public int money = 0;
    public int stageNum = 1;
    public string PlageVilage = "";
    public int catchCnt = 0;
    public int missCnt;

    // 스테이지 설정
    public bool AbleCheckItem = false;
    public bool AbleRunNpc = false; // 도주 Npc 허락할 Stage
    public bool AbleGetMoney = false; // 수금 해금 Stage
    public bool AblePlague = false; //역병 해금 Stage
    public bool AbleChepo = true; //체포 해금
    public bool AbleNextDay = true; // 정산 해금

    //할당
    public int NpcID = 0;
    private Light mainLight;
    public GameObject UI;

    //스카이박스
    public Material daySkybox;
    public Material sunsetSkybox;
    private float transitionDuration = 3.0f;

    private Material currentSkybox;
    private float transitionProgress = 0.0f;
    private bool isTransitioning = false;

    private bool isDaySkyboxActive = false;

    public void SetPlagueVilage()
    {
        int n = Random.Range(0, 8);
        string[] s = {"한양", "경기도", "충청도", "전라도", "경상도", "황해도", "평안도", "강원도", "함경도" };
        PlageVilage = s[n];
        Debug.Log(PlageVilage);
    }

    public int GetStageNum()
    {
        return stageNum;
    }

    public void AddStageNum()
    {
        stageNum++;
    }

    public void AddNpcID() 
    {
        NpcID++;
    }
    public void ResetNpcID()
    {
        NpcID = 0;
    }

    public void ChangeSence()
    {
        SceneManager.LoadScene("Calculate_Scene");
    }

    public void InitUI()
    {
        UI.SetActive(true);
    }

    private float Angle = 80;
    private float MaxAngle = 80;

    private void Update()
    {
        if(AbleNextDay && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Calculate_Scene");
        }
    }

    void FixedUpdate()
    {
        if (mainLight == null) { return; }

        if (Angle > MaxAngle) Angle = MaxAngle;
        Angle += Time.deltaTime *2.0f;
        mainLight.transform.rotation = Quaternion.Euler(Angle, 80, 0);

        //ChangeSkybox(); //스카이박스 교체 부분 수정필요

        if (Angle > 185) AbleNextDay = true;
    }

    public void InitValue()
    {
        missCnt = 0;
        catchCnt = 0;
    }

    public void InitLight()
    {
        AbleNextDay = false;
        mainLight = GameObject.Find("Directional Light").GetComponent<Light>(); 
        Angle = 80;
        MaxAngle = 80;
    }

    public void AddMaxLightAngle()
    {
        MaxAngle += 20;
        if (MaxAngle > 185) MaxAngle = 185;
    }
    public void AddMaxLight()
    {
        MaxAngle++;
    }

    public void ChangeSkybox()
    {

        // 머티리얼 섞어서 해봤는데 적용 x
        // 알파채널 섞어서 해보는거 적용 x
        // 머티리얼 자체를 섞는거 보다 이미지 받아서 섞은다음 새로운 스카이박스 만드는 걸로 할 예정
        
        if (Angle < 85) 
        {
            if (isDaySkyboxActive) return;
            RenderSettings.skybox = daySkybox;
            DynamicGI.UpdateEnvironment();
            isDaySkyboxActive = true;
        }
        else 
        { 
            if(isDaySkyboxActive) isDaySkyboxActive = false;
            if (isTransitioning) return;
            StartCoroutine(SunsetSkyBox()); 
        }
    }

    private IEnumerator SunsetSkyBox()
    {
        isTransitioning = true;

        RenderSettings.skybox = daySkybox;
        sunsetSkybox.SetFloat("_Mode", 2);  // fade 모드 사용 가능 여부 확인

        Color initialColor = daySkybox.color;
        Color targetColor = sunsetSkybox.color;

        while (transitionProgress < 1.0f)
        {
            transitionProgress += Time.deltaTime / transitionDuration;

            float alphaValue = Mathf.Lerp(1, 0, transitionProgress);

            Color dayColor = new Color(initialColor.r, initialColor.g, initialColor.b, alphaValue);
            Color sunsetColor = new Color(targetColor.r, targetColor.g, targetColor.b, 1 - alphaValue);

            daySkybox.color = dayColor;
            sunsetSkybox.color = sunsetColor;

            DynamicGI.UpdateEnvironment();

            yield return null;
        }

        RenderSettings.skybox = sunsetSkybox;
        transitionProgress = 0.0f;
        isTransitioning = false;
    }
}