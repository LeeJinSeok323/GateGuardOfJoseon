using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        // Player 태그를 가진 오브젝트의 Transform 컴포넌트를 직접 가져옵니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure there's an object with the 'Player' tag in the scene.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 playerDirection = player.position - transform.position;
            playerDirection.y = 0; // Y축을 제외하고 계산
            Quaternion rotation = Quaternion.LookRotation(-playerDirection);
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }
    }
}