using NpcInfoManagerSpace;
using OpenAi.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoManager : MonoBehaviour
{
    private Transform GatePoint;
    private float radius;
    private Transform PlayerPoint;
    public GptManager gpt;


    // 정적 변수
    [SerializeField] public static string Name;
    [SerializeField] public static int Age;
    [SerializeField] public static string NpcDaily;
    [SerializeField] public static string Item;
    [SerializeField] public static string Hometown;
    [SerializeField] public static string PassPurpose;
    [SerializeField] public static string Status;
    [SerializeField] public static string Job;

    //리셋 버튼을 누르면
    public void OnResetNpcBtnDown()
    {
        int id = CheckRadiusNPC(PlayerPoint.position);
        NpcCreateParameter parm = NpcManager.Instance.GetParmNPC(id);

        // 인스턴스 변수의 값을 정적 변수에 할당
        Name = parm.Name;
        Age = parm.Age;
        NpcDaily = null;
        Item = null;
        Hometown = parm.Hometown;
        PassPurpose = null;
        Status = parm.Status;
        Job = parm.Job;

        gpt.GetComponent<GptManager>().NpcSetting();

    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        GatePoint = GameObject.FindGameObjectWithTag("Point").transform;
        radius = 3.0f;

    }

    private void Update()
    {
        PlayerPoint = GameObject.FindGameObjectWithTag("Player").transform;
        //int id = CheckRadiusNPC(PlayerPoint.position);

        //Debug.Log(id);
    }

    public int CheckRadiusNPC(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        float closestDistance = Mathf.Infinity;
        int id = 9999;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NPC"))
            {
                float distance = Vector3.Distance(position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    id = NpcManager.Instance.GetIdByObject(collider.gameObject);
                    
                }
            }
        }
        return id;
    }

   

    public void OnClickPassButton()
    {
        int id = CheckRadiusNPC(GatePoint.position);
        if(id != 9999)
        {
            NpcManager.Instance.PassGate(id);
            NpcManager.Instance.Remove(id);
        }

    }

    public void OnClickDeninedButton()
    {
        int id = CheckRadiusNPC(GatePoint.position);
        if (id != 9999)
        {
            NpcManager.Instance.DeninedGate(id);
            NpcManager.Instance.Remove(id);
        }

    }



}
