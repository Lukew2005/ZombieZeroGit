
using UnityEngine;

// enable unity's ai system
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    // if the enemy is chasing the player
    private bool enemyIsChasing;

    // how close the player has to be to the enemy before the enemy starts to chase the player
    public float distanceToChase = 10f;

    // how far away the player has to be from the enemy before the enemy stops chasing the player
    public float distanceToLose = 15f;

    // how close the enemy can get to the player before the enemy should stop
    public float distanceToStop = 2f;

    // what the enemy is moving towards - the player
    private Vector3 targetPoint;

    // the starting position of the enemy
    private Vector3 startPoint;

    // reference to the enemy's nav mesh agent component
    public NavMeshAgent agent;

    // how long the enemy should chase the player
    public float keepChasingTime = 5f;

    // enemy is chasing the player countdown counter
    private float chaseCounter;


    // reference to the enemy's bullet game object
    public GameObject bullet;

    // reference to the enemy's weapon fire position transform component
    public Transform firePoint;

    // enemy weapon's fire rate
    public float fireRate;

    // the time between the enemy firing bullets
    public float waitBetweenShots = 2f;

    // grace period before enemy starts to shoot at player
    public float timeToShoot = 1f;

    // fire rate countdown counter
    private float fireCount;

    // the time between the enemy firing bullets counter
    private float shotWaitCounter;

    // wait to shoot at player counter
    private float shootTimeCounter;


    // reference to the enemy's animator component
    public Animator enemyAnimator;

    // when the enemy gets shot by the player
    private bool wasShot;




    // initialise the enemy
    private void Start()
    {
        Initialise();
    }


    // enemy control
    private void Update()
    {
        EnemyControl();
    }


    private void Initialise()
    {
        // get the enemy's starting position
        startPoint = transform.position;

        // initialise the wait to shoot at player counter
        shootTimeCounter = timeToShoot;

        // initialise the time between the enemy firing bullets counter
        shotWaitCounter = waitBetweenShots;
    }


    private void EnemyControl()
    {
        // get the position of the player
        targetPoint = PlayerController.instance.transform.position;

        // only look for a target at the same 'y' position as the enemy
        targetPoint.y = transform.position.y;


        // if the enemy is not chasing the player
        if (!enemyIsChasing)
        {
            NotChasingPlayer();
        }

        // otherwise
        // chase the player
        else
        {
            ChasePlayer();
        }
    }


    private void ChasePlayer()
    {
        // if the enemy is not within stopping distance of the player
        if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
        {
            // move the enemy toward the player
            agent.destination = targetPoint;
        }

        // otherwise
        else
        {
            // set the destination of the enemy to be its current position
            agent.destination = transform.position;
        }

        // if the player is not close enough for the enemy to chase
        if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
        {
            // and the enemy has not been shot by the player
            if (!wasShot)
            {
                // set the enemy is chasing flag to false - stop chasing the player
                enemyIsChasing = false;

                // reset the enemy is chasing counter to how long the enemy should chase the player
                chaseCounter = keepChasingTime;
            }
        }

        // otherwise
        else
        {
            // set the 'enemy shot by player' flag to false
            wasShot = false;
        }


        // if the time between the enemy firing bullets counter is greater than zero
        if (shotWaitCounter > 0)
        {
            // decrease the time between the enemy firing bullets counter
            shotWaitCounter -= Time.deltaTime;

            // if the time between the enemy firing bullets counter is less than or equal to zero
            if (shotWaitCounter <= 0)
            {
                // reset the 'wait to shoot at player' counter
                shootTimeCounter = timeToShoot;
            }

            // play the enemy moving animation
            enemyAnimator.SetBool("isMoving", true);
        }

        // otherwise
        else
        {
            // if the player is not dead
            if (PlayerController.instance.gameObject.activeInHierarchy)
            {
                // decrease the 'wait to shoot at player' counter
                shootTimeCounter -= Time.deltaTime;

                // if the 'wait to shoot at player' counter is greater than zero
                if (shootTimeCounter > 0)
                {
                    // decrease the enemy's fire rate counter
                    fireCount -= Time.deltaTime;

                    // if the fire rate counter is less than or equal to zero
                    if (fireCount <= 0)
                    {
                        // reset the fire rate counter to the fire rate
                        fireCount = fireRate;

                        // make the enemy aim at the player
                        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));

                        // get the 'direction position' of the player
                        Vector3 targetDirection = PlayerController.instance.transform.position - transform.position;

                        // calculate the firing angle to the player's direction position
                        float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                        // if the firing angle of the player's direction position is between zero and thirty degrees
                        if (Mathf.Abs(angle) < 30f)
                        {
                            // create a bullet at the enemy weapon's fire point position
                            Instantiate(bullet, firePoint.position, firePoint.rotation);

                            // play the enemy firing animation
                            enemyAnimator.SetTrigger("fireShot");
                        }

                        // otherwise
                        else
                        {
                            // reset the time between the enemy firing bullets counter
                            shotWaitCounter = waitBetweenShots;
                        }
                    }

                    // set the destination of the enemy to be its current position
                    agent.destination = transform.position;
                }

                // otherwise
                else
                {
                    // reset the time between the enemy firing bullets counter
                    shotWaitCounter = waitBetweenShots;
                }
            }

            // stop playing the enemy moving animation
            enemyAnimator.SetBool("isMoving", false);
        }
    }


    // enemy is not chasing the player
    private void NotChasingPlayer()
    {
        // if the distance from the enemy to the player is less than the distance to chase the player
        if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
        {
            // set the enemy is chasing flag to true
            enemyIsChasing = true;

            // set the 'wait to shoot at player' counter
            shootTimeCounter = timeToShoot;

            shotWaitCounter = waitBetweenShots;
        }

        // if the enemy is chasing counter is greater than zero
        if (chaseCounter > 0)
        {
            // decrease the enemy is chasing counter
            chaseCounter -= Time.deltaTime;

            // if the enemy is chasing counter is less than or equal to zero
            if (chaseCounter <= 0)
            {
                // send the enemy back to their starting position
                agent.destination = startPoint;
            }
        }

        // if the enemy is moving and the remaining distance to the target position is less than 0.25
        if (agent.remainingDistance < .25f)
        {
            // stop moving the enemy
            enemyAnimator.SetBool("isMoving", false);
        }

        // otherwise
        else
        {
            // move the enemy
            enemyAnimator.SetBool("isMoving", true);
        }
    }


    // when the enemy gets shot by the player
    public void GetShot()
    {
        // set the 'enemy was shot by the player' flag
        wasShot = true;

        // make the enemy start chasing the player
        enemyIsChasing = true;
    }


} // end of class
