using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowKeyPopUp : MonoBehaviour
{
    // ��������Ʈ ����
    public delegate void NPCProximityHandler();
    // �̺�Ʈ ����
    public event NPCProximityHandler OnNPCProximity;

    public float proximityDistance; // UI�� ��Ÿ�� �Ÿ�
    private bool isActive = false;
    private Transform playerTransform;
    private Npc npc;
    private float distance;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        npc = GetComponent<Npc>();
        proximityDistance = 2.0f;
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, playerTransform.position);

        CheckProximity(distance);
        CatuchNpc(distance);
    }

    private void OnDisable()
    {
        // ��ü�� ��Ȱ��ȭ�� �� UI�� �Բ� ��Ȱ��ȭ
        if (isActive)
        {
            OnNPCProximity?.Invoke(); // �̺�Ʈ�� ȣ���Ͽ� UI�� ���� ������ ����
            isActive = false;
        }
    }

    private void CheckProximity(float distance)
    {
        if (playerTransform == null) return;
        if (!isActive && distance <= proximityDistance)  //������ UI ����
        {
            // �̺�Ʈ ȣ��
            OnNPCProximity?.Invoke();
            isActive = true;
        }
        else if(isActive && distance > proximityDistance) // �ָ� ����
        {
            OnNPCProximity?.Invoke();
            isActive = false;
        }
    }

    private void CatuchNpc(float distance)
    {
        if (playerTransform == null) return;
        if(npc == null) return;
        
        if(Input.GetKeyDown(KeyCode.F) && distance <= proximityDistance)
        {
            if (npc.IsVillain && GameMgr.Instance.AbleChepo)
            {
                GameMgr.Instance.catchCnt++;
                this.gameObject.SetActive(false);
            }
            return;
        }
        
    }

}