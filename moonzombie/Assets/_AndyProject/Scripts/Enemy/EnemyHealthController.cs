
using UnityEngine;

using UnityEngine.UI;


public class EnemyHealthController : MonoBehaviour
{
    // how much health the enemy has
    public int currentHealth = 100;

    // the enemy's maximum health
    private int maximumHealth = 100;

    // reference to the enemy's 'enemy controller' script
    public EnemyController enemyControllerScript;


    public Image healthBar;




    // damage the enemy
    public void DamageEnemy(int damageAmount)
    {
        // decrease the enemy's health by the 'damage' amount
        currentHealth -= damageAmount;

        // update the enemy's floating health bar
        healthBar.fillAmount = (float)currentHealth / (float)maximumHealth;


        // if the enemy has an active enemy controller script attached
        if (enemyControllerScript != null)
        {
            // call the 'get shot' method on the enemy controller script
            enemyControllerScript.GetShot();
        }


        // if the enemy's health is less than or equal to zero
        if (currentHealth <= 0)
        {
            // destroy the enemy
            Destroy(gameObject);

            // play the 'explosion' sound effect
            AudioManager.instance.PlaySFX(2);
        }
    }


} // end of class
