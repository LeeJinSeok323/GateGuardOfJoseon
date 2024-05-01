using FIMSpace.FLook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehavior_Stay : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TALK,
        LOOK,
    }

    private Animator anim;
    private float talkdist = 3.0f;
    private Transform player;
    private FLookAnimator LookAnime;

    public State state = State.IDLE;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        LookAnime = GetComponent<FLookAnimator>();
        LookAnime.ObjectToFollow = Camera.main.transform;

        StartCoroutine(CheckNPCState());
        StartCoroutine(CheckNPCAction());
    }

    IEnumerator CheckNPCState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            // 두 물체 사이간 거리
            float distance = Vector3.Distance(player.position, this.transform.position);

            // 두 물체 간 각도 (반환 값 양수)
            Vector3 direction = player.position - this.transform.position;
            float angle = Vector3.Angle(this.transform.forward, direction);


            switch (state)
            {
                case State.IDLE:
                    if (distance < talkdist && angle > 120.0)
                    {
                        state = State.LOOK;
                    }
                    break;

                case State.TALK:
                    yield return new WaitForSeconds(1.5f);
                    state = State.IDLE;
                    Debug.Log("TALK 애니메이션 후 IDLE");
                    break;

                case State.LOOK:
                    Vector3 targetPosition = player.transform.position;
                    targetPosition.y = transform.position.y;
                    this.transform.LookAt(targetPosition);
                    state = State.IDLE;
                    break;
            }
        }
    }

    IEnumerator CheckNPCAction()
    {
        while (true)
        {
            switch (state)
            {
                case State.IDLE:
                    anim.SetBool("isTalk", false);
                    break;
                case State.TALK:
                    anim.SetBool("isTalk", true);
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}
