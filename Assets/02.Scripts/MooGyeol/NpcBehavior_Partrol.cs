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
    int direction = 1; // �̵� ����: 1�� ������, -1�� ������

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

        // NavMeshAgent�� ����Ͽ� �̵� ����
        agent.SetDestination(currentTarget);
    }

    IEnumerator PartolPoint()
    {
        yield return new WaitForSeconds(0.5f);

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
