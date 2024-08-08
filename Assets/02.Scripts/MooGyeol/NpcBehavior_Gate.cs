using FIMSpace.FLook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NpcBehavior_Gate : MonoBehaviour
{
    public enum State
    {
        IDLE,
        WALK,
        TALK,
        LOOK,
        HIT,
        RUN
    }

    public State state = State.IDLE;

    private NavMeshAgent agent;
    private Animator anim;
    public Vector3 currentTarget;
    private Transform player;
    private FLookAnimator LookAnime;

    private float talkdist = 3.0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        LookAnime = GetComponent<FLookAnimator>();
        LookAnime.ObjectToFollow = Camera.main.transform;


        //코루틴으로 상태 검사와 애니메이션 적용
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

                case State.WALK:
                    MoveTo(currentTarget);
                    break;

                case State.TALK:
                    yield return new WaitForSeconds(2.0f);
                    state = State.IDLE;
                    break;

                case State.LOOK:
                    this.transform.LookAt(player.transform);
                    state = State.IDLE;
                    break;

                case State.HIT:
                    yield return new WaitForSeconds(0.5f);
                    state = State.IDLE;
                    break;

                case State.RUN:
                    yield return new WaitForSeconds(2.0f);
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
                    anim.SetBool("isWalk", false);
                    anim.SetBool("isTalk", false);
                    anim.SetBool("isHit", false);
                    anim.SetBool("isRun", false);
                    break;
                case State.WALK:
                    anim.SetBool("isWalk", true);
                    break;
                case State.TALK:
                    anim.SetBool("isTalk", true);
                    break;
                case State.HIT:
                    anim.SetBool("isHit", true);
                    break;
                case State.RUN:
                    anim.SetBool("isRun", true);
                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);

        // 이동 후 정지
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                {
                    state = State.IDLE;
                }
            }
        }
    }

    public void RunTo(Vector3 position)
    {
        agent.speed = 1.5f;
        agent.SetDestination(position);
 
    }
}
