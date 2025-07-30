
using UnityEngine;


public class ZombieIdleState : StateMachineBehaviour
{
    // reference to player game object
    public Transform player;

    // idle timer
    private float timer;

    public float idleTime = 0f;

    // player detection radius
    public float detectionAreaRadius = 18;



    // when we enter the'idle' state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // find the player game object
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // set the idle timer
        timer = 0f;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // increase the idle timer
        timer += Time.deltaTime;

        // if we have reached the idle time limit
        if (timer > idleTime)
        {
            // start the patrolling animation
            animator.SetBool("isPatrolling", true);
        }

        
        // see how close the player is
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // if the player has entered the detection zone
        if (distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }


} // end of class
