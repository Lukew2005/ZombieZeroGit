
using UnityEngine;


public class Explosion : MonoBehaviour
{
    // the amount of damage created by the rocket projectile explosion
    public int damage = 25;


    // whether the damage should be caused to the enemy or the player
    public bool damageEnemy;
    public bool damagePlayer;

    

    // when there is an explosion
    private void OnTriggerEnter(Collider other)
    {
        // if the enemy gets it be the explosion
        if (other.gameObject.CompareTag("Enemy") && damageEnemy)
        {
            // call the 'damage enemy' method on the enemy health controller script
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        // if the player gets hit by the explosion
        if (other.gameObject.CompareTag("Player") && damagePlayer)
        {
            // call the 'damage player' method on the player health controller scipt
            PlayerHealthController.instance.DamagePlayer(damage);
        }
    }


} // end of class
