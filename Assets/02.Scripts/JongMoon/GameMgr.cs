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
    /// ������
    /// </summary>
    public int townHappinessPoint = 40; // ���� �ູ��
    public int bossSatisfaction = 30; // Ž������ ������
    public int money = 0;
    public int stageNum = 1;
    public string PlageVilage = "";
    public int catchCnt = 0;
    public int missCnt;

    // �������� ����
    public bool AbleCheckItem = false;
    public bool AbleRunNpc = false; // ���� Npc ����� Stage
    public bool AbleGetMoney = false; // ���� �ر� Stage
    public bool AblePlague = false; //���� �ر� Stage
    public bool AbleChepo = true; //ü�� �ر�
    public bool AbleNextDay = true; // ���� �ر�

    //�Ҵ�
    public int NpcID = 0;
    private Light mainLight;
    public GameObject UI;

    //��ī�̹ڽ�
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
        string[] s = {"�Ѿ�", "��⵵", "��û��", "����", "���", "Ȳ�ص�", "��ȵ�", "������", "�԰浵" };
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

        //ChangeSkybox(); //��ī�̹ڽ� ��ü �κ� �����ʿ�

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

        // ��Ƽ���� ��� �غôµ� ���� x
        // ����ä�� ��� �غ��°� ���� x
        // ��Ƽ���� ��ü�� ���°� ���� �̹��� �޾Ƽ� �������� ���ο� ��ī�̹ڽ� ����� �ɷ� �� ����
        
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
        sunsetSkybox.SetFloat("_Mode", 2);  // fade ��� ��� ���� ���� Ȯ��

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