using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class NpcBehavior_Partrol : MonoBehaviour
{
    private Vector3[] targets =
    {
        new Vector3(0, 0, 23),
        new Vector3(-22, 0, 11),
        new Vector3(-56, 0, 19),
    };
        
    private NavMeshAgent agent;
    int currentTargetIndex = 0;
    int direction = 1; // 이동 방향: 1은 정방향, -1은 역방향

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Vector3 currentTarget = targets[currentTargetIndex];
        agent.SetDestination(currentTarget);

        StartCoroutine(PartolPoint());
    }

    IEnumerator PartolPoint()
    {
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
