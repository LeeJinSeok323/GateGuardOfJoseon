using UnityEngine;
using UnityEngine.UI;

public class LayerCull : MonoBehaviour
{
    [SerializeField]
    private LayerMask npcLayer; // NPC ���̾�
    [SerializeField]
    private float range;
    private bool ishit; // �����ɽ�Ʈ�� �ε������� �˻�

    [SerializeField]
    private Text actionText;

    private RaycastHit hit;
    void Start()
    {
        // 20M �Ÿ��� NPC Cull
        Camera cam = GetComponent<Camera>();
        float[] distance= new float[32];
        distance[10] = 20;
        cam.layerCullDistances = distance;

    }

    //��ư �̺�Ʈ

    //void Update()
    //{
    //    // Text ����� �˻�
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
    //        //�̱������� ����� �ڵ�
    //        Npc npc = hit.collider.GetComponent<Npc>();
    //        if(npc != null)
    //        {
    //            NpcManager.Instance.ChangeToTalk(npc.ID);
    //        }
            

    //        //// Instance�� �̿��� �ڵ�
    //        //IState state = hit.collider.GetComponent<IState>();
    //        //if (state != null)
    //        //{
    //        //    state.ChangeToTALK();
    //        //}

    //        //// ����ȯ ����ϸ� �ڵ� ������ ������
    //        //NPCctrl targetObject = hit.collider.GetComponent<NPCctrl>();
    //        //if(targetObject)
    //        //{
    //        //    targetObject.StateToTalk();
    //        //}           

    //        // Static ���
    //        // NPCctrl.state = NPCctrl.State.TALK;
    //        // Debug.Log("NPC�� ���¸� �����մϴ�: " + NPCctrl.state);
    //    }
       
    //}

}
