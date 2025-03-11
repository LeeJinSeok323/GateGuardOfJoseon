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
    private bool isProcessingNPC = false;

    public Queue<Npc> npcQueue = new Queue<Npc>();

    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // 1초 대기
        yield return new WaitForSeconds(2.0f);

        GameObject[] NPCList = GameObject.FindGameObjectsWithTag("NPC");

        for (int currentQueueIndex = 0; currentQueueIndex < NPCList.Count(); currentQueueIndex++)
        {
            Npc npc = NPCList[currentQueueIndex].GetComponent<Npc>();

            if (npc.npcType == NpcType.Gate)
            {
                npcQueue.Enqueue(npc);

                // 현재 줄에 있는 NPC의 위치로 이동
                NpcManager.Instance.ChangeToWalk(npc.ID, Positions[npcQueue.Count() - 1].position);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            DequeueNPC();
        }
    }

    public void DequeueNPC()
    {
        if (isProcessingNPC) return; // 연속 두번눌림 방지
        isProcessingNPC = true;

        if (NpcManager.Instance.CheckGate(GatePostion.position))
        {
            isProcessingNPC = false;
            return;
        }

        if (npcQueue.Count > 0)
        {
            // 큐에서 NPC 제거 후 GatePoint 보내기
            Npc leavingNPC = npcQueue.Dequeue();
            NpcManager.Instance.ChangeToWalk(leavingNPC.ID, GatePostion.position);
            GameMgr.Instance.AddMaxLightAngle(); //시간이 흐르게 만듬

            // 앞으로 당겨진 위치로 이동
            for (int i = 0; i < npcQueue.Count; i++)
            {
                NpcManager.Instance.ChangeToWalk(npcQueue.ElementAt(i).ID, Positions[i].position);
            }

            StartCoroutine(ResetProcessingFlagAfterDelay());
        }

    }

    private IEnumerator ResetProcessingFlagAfterDelay()
    {
        yield return new WaitForSeconds(5.0f);
        isProcessingNPC = false;
    }

}

