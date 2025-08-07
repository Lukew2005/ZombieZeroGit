
using UnityEngine;


public class BulletController : MonoBehaviour
{
    // how fast the projectile moves - variable speeds for different projectiles
    public float moveSpeed;

    // how long the projectile is in play after it is fired
    public float lifeTime = 5f;

    // reference to the projectile's rigidbody component
    public Rigidbody projectileRigidbody;

    // reference to the 'impace effect' game object
    public GameObject impactEffect;

    // the amount of damage the projectile causes
    public int damage;

    // the maximum amount of damage a projectile can cause
    private int maximumDamage = 100;

    // whether the projectile can damage the 'enemy' or the 'player'
    public bool damageEnemy;
    public bool damagePlayer;






    // move the projectile
    private void Update()
    {
        MoveProjectile();
    }


    private void MoveProjectile()
    {
        // move the projectile in a forward direction at the projectile's speed
        projectileRigidbody.linearVelocity = transform.forward * moveSpeed;

        // decrease the bullet's lifetime count
        lifeTime -= Time.deltaTime;

        // if the projectile's lifetime counter is less than or equal to zero
        if (lifeTime <= 0)
        {
            // destroy them projectile
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider objectProjectileHit)
    {
        // if the projectile hit an enemy and the projectile can 'damage' the enemy
        if (objectProjectileHit.gameObject.CompareTag("Enemy") && damageEnemy)
        {
            // call the method on the 'enemy health' script to damage the enemy with the amount of 'damage'
            objectProjectileHit.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        // if the projectile hit the enemies 'head' and the projectile can 'damage' the enemy
        if (objectProjectileHit.gameObject.CompareTag("Headshot") && damageEnemy)
        {
            // call the method on the 'enemy health' script to damage the enemy with two times the amount of damage
            objectProjectileHit.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(maximumDamage); // damage * 2);

            // display a debug message to say we got a head shot
            Debug.Log("Headshot hit");
        }

        // if the projectile hit the player and the projectile can 'damage' the player
        if (objectProjectileHit.gameObject.CompareTag("Player") && damagePlayer)
        {
            //Debug.Log("Hit Player at " + transform.position);

            // call the method on the 'player health' script to damage the player with the amount of 'damage'
            ///PlayerHealthController.instance.DamagePlayer(damage);
        }

        // destroy the projectile
        Destroy(gameObject);

        // create an 'impact' effect where the projectile hits
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }


} // end of class
