using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyPopUp : MonoBehaviour
{
    // 델리게이트 정의
    public delegate void NPCProximityHandler();
    // 이벤트 선언
    public event NPCProximityHandler OnNPCProximity;

    public float proximityDistance; // UI가 나타날 거리
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

        if (!isActive && distance <= proximityDistance)  //가까우면 UI 띄우기
        {
            
            // 이벤트 호출
            OnNPCProximity?.Invoke();
            isActive = true;
        }
        else if(isActive && distance > proximityDistance) // 멀면 끄기
        {
            OnNPCProximity?.Invoke();
            isActive = false;
        }
    }
}