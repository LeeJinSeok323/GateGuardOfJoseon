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
        int age, 
        string gender, 
        string style, 
        string status, 
        string town,
        string job,
        float walkSpeed, 
        float runSpeed,
        bool isRunning)
    {

        npcType = type;
        Number = number;
        Name = name;
        Age = age;   
        Gender = gender;
        Style = style;
        Status = status;
        Hometown = town;
        Job = job;
        WalkSpeed = walkSpeed;
        RunSpeed = runSpeed;
        IsRunning = isRunning;
    }

    public enum NpcType
    {
        None,
        Stay,
        Patrol,
        Gate,
        Run,
    }


    public NpcType npcType { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Style { get; set; }
    public string Status { get; set; }

    public string Hometown { get; set; }
    public string Job { get; set; }



    public float WalkSpeed { get; set; }
    public float RunSpeed { get; set; }
    public bool IsRunning { get; set; }

}
