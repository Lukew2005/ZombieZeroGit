
using UnityEngine;


public class BouncePad : MonoBehaviour
{
    // how high the player bounces when they walk onto a bounce pad
    public float bounceForce;



    private void OnTriggerEnter(Collider other)
    {
        // if the player walks onto the bounce pad
        if(other.CompareTag("Player"))
        {
            // call the 'bounce' method on the player controller script
            PlayerController.instance.Bounce(bounceForce);

            // play the 'player jump' sound
            AudioManager.instance.PlaySFX(0);
        }
    }


} // end of class
