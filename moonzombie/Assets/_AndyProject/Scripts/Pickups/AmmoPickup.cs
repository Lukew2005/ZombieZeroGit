
using UnityEngine;


public class AmmoPickup : MonoBehaviour
{
    // whether the ammo pickup has been collected
    private bool collected;



    private void OnTriggerEnter(Collider other)
    {
        // if the player collides with the ammo pickup and the pickup hasn't already been collected
        if (other.CompareTag("Player") && !collected)
        {
            // add the collected ammo to the player's currently active weapon
            PlayerController.instance.activeGun.GetAmmo();

            // destroy the ammo pickup
            Destroy(gameObject);

            // set 'pickup collected' to true
            collected = true;

            // play the 'ammo pickup collected' sound
            AudioManager.instance.PlaySFX(3);
        }
    }


} // end of class
