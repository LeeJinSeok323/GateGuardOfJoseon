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


    // ���� ����
    
    [SerializeField] public static int id;
    [SerializeField] public static string Name;
    [SerializeField] public static string Age;
    [SerializeField] public static string NpcDaily;
    [SerializeField] public static string Item;
    [SerializeField] public static string Hometown;
    [SerializeField] public static string PassPurpose;
    [SerializeField] public static string Status;
    [SerializeField] public static string Job;

    //���� ��ư�� ������
    public void OnResetNpcBtnDown()
    {
        int id = CheckRadiusNPC(PlayerPoint.position);
        NpcCreateParameter pram = NpcManager.Instance.GetParmNPC(id);

        // �ν��Ͻ� ������ ���� ���� ������ �Ҵ�
        Name = pram.Name;
        Age = pram.Age;
        NpcDaily = pram.NpcDaily;
        Item = pram.Item;
        Hometown = pram.Hometown;
        PassPurpose = pram.PassPurpose;
        Status = pram.Status;
        Job = pram.Job;

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
        int id = CheckRadiusNPC(PlayerPoint.position);

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
