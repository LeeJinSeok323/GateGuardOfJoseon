using UnityEngine;
using static NpcCreateParameter;

public class StandardNpcCreate : MonoBehaviour
{
    private NpcCreateParameter[] _patrolNpcParams; // NpcCreateParameter 배열 선언
    private NpcCreateParameter[] _stayNpcParams; // 또 다른 NpcCreateParameter 배열 선언
    
    #region NPC 스폰 위치 Vector3 값들
    //StayNPC 리스트
    private static Vector3[] _stayNpcPosition =
    {
        new Vector3(-13, 0, -59),
        new Vector3(-20, 0, -67),
        new Vector3(-14, 0, -54),
        new Vector3(-19, 0, -45),
        new Vector3(-16, 0, -73),
        new Vector3(-24, 0, -76),
        new Vector3(-24, 0, -88),
        new Vector3(-19, 0, -86),
        new Vector3(-25, 0, -97),
        new Vector3(-19, 0,-97),
        new Vector3(-25, 0, -110),
        new Vector3(-25, 0, -123),
        new Vector3(-18, 0, -113),
        new Vector3(-11, 0, -127),
        new Vector3(-7, 0, -157),
        new Vector3(-24, 0, -158),
        new Vector3(7, 0, -146),
        new Vector3(0, 0, -139),
        new Vector3(13, 0, -139),
        new Vector3(19, 0, -147),
        new Vector3(29, 0, -149),
        new Vector3(35, 0, -145),
        new Vector3(-26, 0, -143)
    };

    //PatrolNPC 리스트
    private Vector3[] _patrolNpcPosition =
    {
        new Vector3(-14, 1, -160),
        new Vector3(-14, 1, -160),
        new Vector3(-33, 1, -172),
        new Vector3(5, 1, -45),
        new Vector3(48, 1, -151),
        new Vector3(48, 1, -132),
        new Vector3(-20, 1, -77),
        new Vector3(-20, 1, -77),

    };
    #endregion

    public void Start()
    {   
        //NPC 파라미터 생성
        NpcManager.Instance.SetParameters(ref _stayNpcParams, NpcType.Stay, 20);
        NpcManager.Instance.SetParameters(ref _patrolNpcParams, NpcType.Patrol, 8);

       //StayNPC 스폰
       for (int i=0; i < _stayNpcParams.Length; i++)
       {
           GameObject Staynpc = NpcManager.Instance.CreateNPC(_stayNpcParams[i]);
           Staynpc.transform.position = _stayNpcPosition[i];
       }

       //PartolNPC 스폰
       for (int i=0; i < _patrolNpcParams.Length; i++)
       {
           GameObject Partrolnpc = NpcManager.Instance.CreateNPC(_patrolNpcParams[i]);
           Partrolnpc.transform.position = _patrolNpcPosition[i];
       }

    }   
}
