using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    #region 테이블에서 받은 값
    public NpcCreateParameter.NpcType npcType;
    public int ID;
    public string Name;
    public int Age;
    public string Gender;
    public string Style;
    public string Status;
    public string Hometown;
    public string Job;
    public float WalkSpeed;
    public float RunSpeed;
    public bool IsRunning;
    #endregion

}
