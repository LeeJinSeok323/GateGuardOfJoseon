using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcCreateParameter
{
    public NpcCreateParameter(
        NpcType type, 
        int number, 
        string name, 
        string age, 
        string gender, 
        // string style, 
        string status, 
        string town,
        string job,
        string passPurpose,
        string item,
        string npcDaily)
    {
        npcType = type;
        Number = number;
        Name = name;
        Age = age;   
        Gender = gender;
        // Style = style;
        Status = status;
        Hometown = town;
        Job = job;
        PassPurpose = passPurpose;
        Item = item;
        NpcDaily = npcDaily;
    }

    public enum NpcType
    {
        None,
        Stay,
        Patrol,
        Gate,
        Run,
    }

//  C# class에서 get set 접근자로 변수 선언: 다른 클래스에서 접근이 가능하게 됌
    public NpcType npcType { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public string Style { get; set; }
    public string Hometown { get; set; }
    public string Status { get; set; }
    public string Job { get; set; }
    public string PassPurpose { get; set; }
    public string Item { get; set; }
    public string NpcDaily { get; set; }
}
