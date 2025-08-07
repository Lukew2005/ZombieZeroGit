
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


public class ZombiePatrollingState : StateMachineBehaviour
{

    // reference to player
    private Transform player;

    // reference to then enemy nav mesh agent
    private NavMeshAgent navAgent;


    private float timer;

    // how long the enemy will patrol
    public float patrolTime = 10f;

    public float detectionAreaRadius = 18f;

    // how fast the enemy moves when patrolling
    public float patrolSpeed = 2f;


    // list of enemy patrol points
    private List<Transform> patrolPointsList = new List<Transform>();



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // find the player game object
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // set reference to the enemy nav mesh agent
        navAgent = animator.GetComponent<NavMeshAgent>();

        // set the enemy patrol speed
        navAgent.speed = patrolSpeed;

        // reset timer
        timer = 0;


        // get all the patrol points
        GameObject patrolPointCluster = GameObject.FindGameObjectWithTag("Patrol Points");

        // loop through all the patrol points and add them to the patrol points list
        foreach (Transform patrolPoint in patrolPointCluster.transform)
        {
            patrolPointsList.Add(patrolPoint);
        }

        // get a random patrol point
        Vector3 nextPatrolPoint = patrolPointsList[Random.Range(0, patrolPointsList.Count)].position;

        // set the patrol point destination for the nav mesh agent
        navAgent.SetDestination(nextPatrolPoint);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if the enemy has reached the current patrol point
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            // select another random patrol point to move to
            navAgent.SetDestination(patrolPointsList[Random.Range(0, patrolPointsList.Count)].position);
        }


        // increase the patrol timer
        timer += Time.deltaTime;

        // if we have reached the patrol time limit
        if (timer > patrolTime)
        {
            // start the idle animation
            animator.SetBool("isPatrolling", false);
        }


        // see how close the player is
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // if the player has entered the detection zone
        if (distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // stop the enemy from moving when the patrol time has been reached
        navAgent.SetDestination(navAgent.transform.position);
    }


} // end of class
