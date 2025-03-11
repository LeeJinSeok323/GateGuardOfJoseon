using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TextUIInfo : MonoBehaviour
{
    public Text Birth;
    public Text Name;
    public Text HomeTown;
    public Text Status;

    //public Text Job1;
    public Text Name2;
    public Text CompeletYear;

    public Text Name3;
    public Text Birth2;
    public Text HomeTown2;
    public Text CompeletYear2;

    public int id;

 
    public void Start()
    {
        Birth.text = " ";
        Name.text = " ";
        HomeTown.text = " ";
        Status.text = " ";

        //Job1.text = " ";
        Name2.text = " ";
        CompeletYear.text = "1860.06.12";

        Name3.text = " ";
        Birth2.text = " ";
        HomeTown2.text = " "; 
        CompeletYear2.text = "1860.06.12";
    }

    public void Update()
    {
        // UIInfoManager가 CreateNpc로 만들어진 정보를 기반으로 만들어진거고
        // NpcInfoGenerater가 테이블 정보 기반으로 만들어진거임
        // 따라서 Create에서 Name, Age, Status, Home을 바꿔 말하도록 만들어서
        // 빌런일때 UI에서 바꿔보이게 만들었음

        if (UIInfoManager.Name != null)
        {
            id = UIInfoManager.Id;

            Birth.text = CheseYear().ToString();
            Name.text = UIInfoManager.Name;
            HomeTown.text = UIInfoManager.Hometown;
            Status.text = UIInfoManager.Status;

            Name2.text = NpcInfoGenerater.Instance.nameTable[id];

            Name3.text = UIInfoManager.Name;
            Birth2.text = CheseYear2().ToString();
            HomeTown2.text = NpcInfoGenerater.Instance.homeTable[id];
        }
    }

    private int CheseYear()
    {
        int year = int.Parse(UIInfoManager.Age.ToString());
        year = 1860 - year;

        return year;
    }

    private int CheseYear2()
    {
        int year = int.Parse(NpcInfoGenerater.Instance.ageTable[id].ToString());
        year = 1860 - year;

        return year;
    }


}
