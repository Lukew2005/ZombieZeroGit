
using UnityEngine;

using UnityEngine.AI;


public class ZombieAttackState : StateMachineBehaviour
{

    // reference to player
    private Transform player;

    // reference to then enemy nav mesh agent
    private NavMeshAgent navAgent;


    public float stopAttackingDistance = 2.5f;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // find the player game object
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // set reference to the enemy nav mesh agent
        navAgent = animator.GetComponent<NavMeshAgent>();

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // make enemy look at player
        LookAtPlayer();


        // get the distance of the player from the enemy
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // if the player is to far away
        if (distanceFromPlayer > stopAttackingDistance)
        {
            // stop attacking the player
            animator.SetBool("isAttacking", false);
        }
    }


    private void LookAtPlayer()
    {
        // get the direction the player
        Vector3 playerDirection = player.position - navAgent.transform.position;

        // set the direction of rotation
        navAgent.transform.rotation = Quaternion.LookRotation(playerDirection);

        // calculate the rotation
        var yRotation = navAgent.transform.eulerAngles.y;

        // rotate the enemy to face the player
        navAgent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }


} // end of class
