using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class NpcBehavior_Partrol : MonoBehaviour
{
    private Vector3[] targets =
    {
        new Vector3(4, 0, -44),
        new Vector3(-15, 0, -44),
        new Vector3(-23, 0, -128),
        new Vector3(-34, 0, -172),
    };

    private Vector3[] targets2 =
    {
        new Vector3(68, 0, -151),
        new Vector3(3, 0, -144),
        new Vector3(-9, 0, -157),
        new Vector3(-34, 0, -172),
    };

    private Vector3[] targets3 =
    {
        new Vector3(54, 0, -143),
        new Vector3(47, 0, -154),
        new Vector3(-2, 0, -140),
        new Vector3(-17, 0, -125),
        new Vector3(-20, 0, -85),
        new Vector3(-14, 0, -45),
        new Vector3(7, 0, -45),
    };

    private NavMeshAgent agent;
    int currentTargetIndex = 0;
    int direction = 1; // 이동 방향: 1은 정방향, -1은 역방향

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Invoke("ActivateNavMeshAgent", 0.5f);
        StartCoroutine(PartolPoint());

    }

    private void ActivateNavMeshAgent()
    {
        agent.enabled = true;

        Vector3 currentTarget = targets[currentTargetIndex];

        // NavMeshAgent를 사용하여 이동 설정
        agent.SetDestination(currentTarget);
    }

    IEnumerator PartolPoint()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {

            yield return new WaitForSeconds(0.3f);

            // 경로계산이 완료되지 않았고 && 현재 경로에서 에이전트의 위치와 목적지 사이의 거리가 0.1보다 작다
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                // 원하는 방향으로 인덱스 증가
                currentTargetIndex += direction;

                // 방향을 변경해야 하는지 확인
                if (currentTargetIndex >= targets.Length || currentTargetIndex < 0)
                {
                    direction *= -1; // 방향 전환
                    currentTargetIndex += 2 * direction; // 경계값에서 인덱스 조정
                }

                // 새 목적지 설정
                Vector3 currentTarget = targets[currentTargetIndex];
                agent.SetDestination(currentTarget);
            }
        }
    }
}
