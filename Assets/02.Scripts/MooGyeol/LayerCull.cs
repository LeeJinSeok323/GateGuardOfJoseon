using UnityEngine;
using UnityEngine.UI;

public class LayerCull : MonoBehaviour
{
    [SerializeField]
    private LayerMask npcLayer; // NPC 레이어
    [SerializeField]
    private float range;
    private bool ishit; // 레이케스트에 부딪혔는지 검사

    [SerializeField]
    private Text actionText;

    private RaycastHit hit;
    void Start()
    {
        // 20M 거리밖 NPC Cull
        Camera cam = GetComponent<Camera>();
        float[] distance= new float[32];
        distance[10] = 20;
        cam.layerCullDistances = distance;

    }

    //버튼 이벤트

    //void Update()
    //{
    //    // Text 띄우기와 검사
    //    CheckNPC();

    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        ChangeNPCState();
    //    }
    //}

    //void CheckNPC()
    //{
        
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, range, npcLayer))
    //    {
    //        if (hit.collider.CompareTag("NPC"))
    //        {
    //            ishit = true;
    //            actionText.gameObject.SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        ishit = false;
    //        actionText.gameObject.SetActive(false);
    //    }

    //}

    //public void ChangeNPCState()
    //{
    //    if(ishit)
    //    {
    //        //싱글톤으로 사용한 코드
    //        Npc npc = hit.collider.GetComponent<Npc>();
    //        if(npc != null)
    //        {
    //            NpcManager.Instance.ChangeToTalk(npc.ID);
    //        }
            

    //        //// Instance를 이용한 코드
    //        //IState state = hit.collider.GetComponent<IState>();
    //        //if (state != null)
    //        //{
    //        //    state.ChangeToTALK();
    //        //}

    //        //// 형변환 사용하면 코드 의존성 높아짐
    //        //NPCctrl targetObject = hit.collider.GetComponent<NPCctrl>();
    //        //if(targetObject)
    //        //{
    //        //    targetObject.StateToTalk();
    //        //}           

    //        // Static 사용
    //        // NPCctrl.state = NPCctrl.State.TALK;
    //        // Debug.Log("NPC의 상태를 변경합니다: " + NPCctrl.state);
    //    }
       
    //}

}
