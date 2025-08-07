
using UnityEngine;


public class HealthPickup : MonoBehaviour
{
    // whether the health pickup has been collected
    private bool isCollected;

    // the amount of health the player receives when the pickup is collected
    public int healAmount;



    private void OnTriggerEnter(Collider other)
    {
        // if the player collides with the health pickup and the pickup hasn't already been collected
        if (other.CompareTag("Player") && !isCollected)
        {
            // add the amount of 'pickup health' to the player
            PlayerHealthController.instance.HealPlayer(healAmount);

            // destroy the health pickup
            Destroy(gameObject);

            // play the 'health pickup collected' sound
            AudioManager.instance.PlaySFX(5);
        }
    }


} // end of class
