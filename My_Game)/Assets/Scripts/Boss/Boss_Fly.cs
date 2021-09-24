using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fly : StateMachineBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private float _attackRange = 3f;
    
    [SerializeField]
    private float _rainRate = 0.35f;
    private float nextRainTime = 0f;


    private Transform player;

    private Rigidbody2D rb;
    private Boss boss;
    bool alreadyExecuted = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (alreadyExecuted) return;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        nextRainTime = Time.time + 1f / _rainRate;
        alreadyExecuted = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        if (animator.gameObject.GetComponent<Boss>().Obstacle == false)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, _speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        if (Time.time >= nextRainTime)
        {
            animator.SetTrigger("Rain");
            nextRainTime = Time.time + 1f / _rainRate;
        }

        if (Mathf.Abs( player.position.x -  rb.position.x) <= _attackRange)
        {
            animator.SetTrigger("attack02");
        }



    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack02");
        animator.ResetTrigger("Rain");
    }

}
