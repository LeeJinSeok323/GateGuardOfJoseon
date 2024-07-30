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
        if(UIInfoManager.Name != null)
        {
            id = UIInfoManager.Id;

            Birth.text = CheseYear().ToString();
            Name.text = NpcInfoGenerater.Instance.nameTable[id];
            HomeTown.text = UIInfoManager.Hometown;
            Status.text = UIInfoManager.Status;

            Name2.text = NpcInfoGenerater.Instance.nameTable[id];

            Name3.text = UIInfoManager.Name;
            Birth2.text = CheseYear().ToString();
            HomeTown2.text = UIInfoManager.Hometown;
        }
    }

    private int CheseYear()
    {
        int year = int.Parse(UIInfoManager.Age.ToString());
        year = 1860 - year;

        return year;
    }


}
