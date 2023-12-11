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

    private Queue<Npc> npcQueue = new Queue<Npc>();

    private void Start()
    {
        GameObject[] NPCList = GameObject.FindGameObjectsWithTag("NPC");

        for (int currentQueueIndex = 0; currentQueueIndex < NPCList.Count(); currentQueueIndex++)
        {
            Npc npc = NPCList[currentQueueIndex].GetComponent<Npc>();
            if (npc.npcType == NpcType.Gate)
            {
                npcQueue.Enqueue(npc);

                // 현재 줄에 있는 NPC의 위치로 이동
                NpcManager.Instance.ChangeToWalk(npc.ID, Positions[npcQueue.Count()-1].position);
            }
            
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 스페이스 키를 누를 때마다 NPC 큐에 NPC 삭제
            DequeueNPC();
        }
    }

    public void DequeueNPC()
    {
        if (npcQueue.Count > 0)
        {
            // 큐에서 NPC 제거
            Npc leavingNPC = npcQueue.Dequeue();
            NpcManager.Instance.ChangeToWalk(leavingNPC.ID, GatePostion.position);

            // 앞으로 당겨진 위치로 이동
            for (int i = 0; i < npcQueue.Count; i++)
            {
                NpcManager.Instance.ChangeToWalk(npcQueue.ElementAt(i).ID, Positions[i].position);
            }
        }
    }

}

