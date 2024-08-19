using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowKeyPopUp : MonoBehaviour
{
    // 델리게이트 정의
    public delegate void NPCProximityHandler();
    // 이벤트 선언
    public event NPCProximityHandler OnNPCProximity;

    public float proximityDistance; // UI가 나타날 거리
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
        // 객체가 비활성화될 때 UI도 함께 비활성화
        if (isActive)
        {
            OnNPCProximity?.Invoke(); // 이벤트를 호출하여 UI를 끄는 로직을 실행
            isActive = false;
        }
    }

    private void CheckProximity(float distance)
    {
        if (playerTransform == null) return;
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