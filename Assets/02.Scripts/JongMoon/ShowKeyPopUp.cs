using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyPopUp : MonoBehaviour
{
    // ��������Ʈ ����
    public delegate void NPCProximityHandler();
    // �̺�Ʈ ����
    public event NPCProximityHandler OnNPCProximity;

    public float proximityDistance; // UI�� ��Ÿ�� �Ÿ�
    private bool isActive = false;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        proximityDistance = 2.0f;
    }

    private void Update()
    {
        CheckProximity();
    }

    private void CheckProximity()
    {
        if (playerTransform == null) return;
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        //Debug.Log(distance);

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
}