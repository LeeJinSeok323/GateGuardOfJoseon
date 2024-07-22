using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static NpcCreateParameter;

public class NpcQueueMananger : MonoBehaviour
{
    public List<Transform> Positions;
    public Transform GatePostion;

    public Queue<Npc> npcQueue = new Queue<Npc>();

    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // 1�� ���
        yield return new WaitForSeconds(1.0f);

        GameObject[] NPCList = GameObject.FindGameObjectsWithTag("NPC");

        for (int currentQueueIndex = 0; currentQueueIndex < NPCList.Count(); currentQueueIndex++)
        {
            Npc npc = NPCList[currentQueueIndex].GetComponent<Npc>();

            if (npc.npcType == NpcType.Gate)
            {
                npcQueue.Enqueue(npc);

                // ���� �ٿ� �ִ� NPC�� ��ġ�� �̵�
                NpcManager.Instance.ChangeToWalk(npc.ID, Positions[npcQueue.Count() - 1].position);
            }

        }

        // ���� �� ������ �ڵ�
        Debug.Log("Start �Լ� ���� 1�� �ڿ� ����");
    }


    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.B))
        {
            // �����̽� Ű�� ���� ������ NPC ť�� NPC ����
            DequeueNPC();
        }
        
    }


    public void DequeueNPC()
    {
        if (npcQueue.Count > 0)
        {
            // ť���� NPC ����
            Npc leavingNPC = npcQueue.Dequeue();
            NpcManager.Instance.ChangeToWalk(leavingNPC.ID, GatePostion.position);

            // ������ ����� ��ġ�� �̵�
            for (int i = 0; i < npcQueue.Count; i++)
            {
                NpcManager.Instance.ChangeToWalk(npcQueue.ElementAt(i).ID, Positions[i].position);
            }
        }
    }

}

