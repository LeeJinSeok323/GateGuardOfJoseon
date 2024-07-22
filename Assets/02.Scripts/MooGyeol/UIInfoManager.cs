    using OpenAi.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoManager : MonoBehaviour
{
    private Transform GatePoint;
    private float radius;
    private static float radius2;
    private Transform PlayerPoint;
    public GptManager gpt;

    // 정적 변수
    public static string Name;
    public static string Age;
    public static string NpcDaily;
    public static string Item;
    public static string Hometown;
    public static string PassPurpose;
    public static string Status;
    public static string Job;
    public static int Id;

    //리셋 버튼을 누르면 근처 NPC 정보를 받아옴
    public void  OnResetNpcBtnDown()    
    {
        int id = CheckRadiusNPC(PlayerPoint.position);
        if (id > 100) return;
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
        Id = parm.Number;

        gpt.GetComponent<GptManager>().NpcSetting();
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
       
        GatePoint = GameObject.FindGameObjectWithTag("Point").transform;
        radius = 2.0f;
        radius2 = 2.0f;
    }

    private void Update()
    {
        PlayerPoint = GameObject.FindGameObjectWithTag("Player").transform;
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
    public static GameObject CheckRadiusNPCObject(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius2);
        float closestDistance = Mathf.Infinity;
        GameObject closestNPC = null;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NPC"))
            {
                float distance = Vector3.Distance(position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNPC = collider.gameObject;
                }
            }
        }

        return closestNPC;
        
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
            Debug.Log("호출됌.");
        }

    }



}
