    using OpenAi.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoManager : MonoBehaviour
{
    // ���� ����
    public static string Name;
    public static string Age;
    public static string NpcDaily;
    public static string Item;
    public static string Hometown;
    public static string PassPurpose;
    public static string Status;
    public static string Job;
    public static int Id;
    public static bool Villran;

    private static UIInfoManager _instance;

    private Transform PlayerPoint;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //���� ��ư�� ������ ��ó NPC ������ �޾ƿ�
    public void  OnResetNpcBtnDown()    
    {
        PlayerPoint = GameObject.FindGameObjectWithTag("Player").transform;
        int id = NpcManager.Instance.CheckRadiusNPC(PlayerPoint.position);
        if (id > 100) return;
        NpcCreateParameter parm = NpcManager.Instance.GetParmNPC(id);

        // �ν��Ͻ� ������ ���� ���� ������ �Ҵ�
        Name = parm.Name;
        Age = parm.Age;
        NpcDaily = null;
        Item = null;
        Hometown = parm.Hometown;
        PassPurpose = null;
        Status = parm.Status;
        Job = parm.Job;
        Id = parm.Number;
        Villran = parm.IsVillain;

        GptManager.Instance.NpcSetting();
    }
}
