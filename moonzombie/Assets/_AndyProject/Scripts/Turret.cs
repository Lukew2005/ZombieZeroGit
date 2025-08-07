
using UnityEngine;


public class Turret : MonoBehaviour
{
    // reference to the turret bullet game object
    public GameObject bullet;

    // how close the player is to the turret
    public float rangeToTargetPlayer;

    // how quickly the turret fires at the player
    public float timeBetweenShots = .5f;

    // the turret's fire rate counter
    private float shotCounter;

    // reference to the turret transform
    public Transform gun;

    // reference to the turret's fire point position transform
    public Transform firepoint;

    // how quickly the turret rotates
    public float rotateSpeed = 45f;

    


    // initialise turret
    void Start()
    {
        InitaliseTurret();
    }

    
    // fire at the player
    void Update()
    {
        FireAtPlayer();
    }


    // initialise turret
    private void InitaliseTurret()
    {
        // set the fire rate counter to the time between shooting
        shotCounter = timeBetweenShots;
    }


    private void FireAtPlayer()
    {
        // if the player is within shooting range of the turret
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer)
        {
            // aim at the player
            gun.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));

            // decrease the fire rate counter
            shotCounter -= Time.deltaTime;

            // if the fire rate counter is less than or equal to zero
            if (shotCounter <= 0)
            {
                // create a bullet at the fire point of the turret
                Instantiate(bullet, firepoint.position, firepoint.rotation);

                // reset the fire rate counter to the time between shooting
                shotCounter = timeBetweenShots;
            }
        }

        // otherwise
        else
        {
            // reset the fire rate counter to the time between shooting
            shotCounter = timeBetweenShots;

            // simply rotate the turret - patrolling
            gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0f, gun.rotation.eulerAngles.y + 10f, 0f), rotateSpeed * Time.deltaTime);
        }
    }


} // end of class
