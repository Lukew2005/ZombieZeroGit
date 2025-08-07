
using UnityEngine;

using UnityEngine.AI;


public class ZombieChaseState : StateMachineBehaviour
{

    // reference to player
    private Transform player;

    // reference to then enemy nav mesh agent
    private NavMeshAgent navAgent;


    public float chaseSpeed = 6f;

    public float stopChasingDistance = 21f;

    public float attackingDistance = 2.5f;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // find the player game object
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // set reference to the enemy nav mesh agent
        navAgent = animator.GetComponent<NavMeshAgent>();

        // set the chase speed of the enemy
        navAgent.speed = chaseSpeed;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // move enemy toward the player
        navAgent.SetDestination(player.position);

        // make enemy look at the player
        animator.transform.LookAt(player);


        // get the distance of the player from the enemy
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // if the player is to far away
        if (distanceFromPlayer > stopChasingDistance)
        {
            // stop chasing the player
            animator.SetBool("isChasing", false);
        }

        // otherwise
        // if the player is too close
        if (distanceFromPlayer < attackingDistance)
        {
            // attack the player
            animator.SetBool("isAttacking", true);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // stop the enemy from moving
        navAgent.SetDestination(navAgent.transform.position);
    }


} // end of class
