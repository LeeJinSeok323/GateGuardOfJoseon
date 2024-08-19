using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (Angle > 185) AbleNextDay = true;
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
}