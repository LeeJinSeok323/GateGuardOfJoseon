using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = player.position - transform.position;
        playerDirection.y = 0; // Y축을 제외하고 계산
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
    }
}
