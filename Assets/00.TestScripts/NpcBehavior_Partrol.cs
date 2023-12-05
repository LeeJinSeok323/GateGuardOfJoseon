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
    int direction = 1; // �̵� ����: 1�� ������, -1�� ������

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

            // ��ΰ���� �Ϸ���� �ʾҰ� && ���� ��ο��� ������Ʈ�� ��ġ�� ������ ������ �Ÿ��� 0.1���� �۴�
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                // ���ϴ� �������� �ε��� ����
                currentTargetIndex += direction;

                // ������ �����ؾ� �ϴ��� Ȯ��
                if (currentTargetIndex >= targets.Length || currentTargetIndex < 0)
                {
                    direction *= -1; // ���� ��ȯ
                    currentTargetIndex += 2 * direction; // ��谪���� �ε��� ����
                }

                // �� ������ ����
                Vector3 currentTarget = targets[currentTargetIndex];
                agent.SetDestination(currentTarget);
            }
        }
    }
}
