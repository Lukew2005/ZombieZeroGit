
using UnityEngine;


public class WeaponPickup : MonoBehaviour
{
    // the name of the weapon the player has collected
    public string weaponName;

    // whether the weapon pickup has been collected
    private bool collected;



    private void OnTriggerEnter(Collider other)
    {
        // if the player collides with the weapon pickup and the pickup hasn't already been collected
        if (other.CompareTag("Player") && !collected)
        {
            // add the weapon
            PlayerController.instance.AddGun(weaponName);

            // destroy the weapon pickup
            Destroy(gameObject);

            // set the 'weapon collected' flag to true
            collected = true;

            // play the 'weapon collected' sound
            AudioManager.instance.PlaySFX(4);
        }
    }


} // end of class
