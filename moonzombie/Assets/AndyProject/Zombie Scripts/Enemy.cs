
using UnityEngine;

using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    // how much health the zombie has
    [SerializeField] private int HP = 5; //100;

    // set a reference to the zombies animator component
    private Animator enemyAnimator;

    // set a reference to the zombies nav mesh agent component
    private NavMeshAgent navAgent;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get the reference to the zombies animator component
        enemyAnimator = GetComponent<Animator>();

        // get the reference to the zombies nav mesh agent component
        navAgent = GetComponent<NavMeshAgent>();
    }


    // when the zombie takes damage
    public void TakeDamage(int damageAmount)
    {
        // subtract the damage amount from the zombie's health
        HP -= damageAmount;

        // if the zombie's health is less than or equal to zero
        if (HP <= 0)
        {
            // select a random death animation to play between 0 and 1
            int randomDeathAnimation = Random.Range(0, 2);

            // play a 'zombie death' animation
            if (randomDeathAnimation == 0)
            {
                enemyAnimator.SetTrigger("DIE1");
            }

            else
            {
                enemyAnimator.SetTrigger("DIE2");
            }
        }

        // otherwise
        else
        {
            // play the 'zombie take damage' animation
            enemyAnimator.SetTrigger("DAMAGE");
        }
    }


} // end of class
