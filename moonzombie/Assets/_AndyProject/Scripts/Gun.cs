
using UnityEngine;


public class Gun : MonoBehaviour
{
    // reference to the weapon's bullet game object
    public GameObject bullet;

    // whether the weapon can auto fire
    public bool canAutoFire;

    // the fire rate of the weapon
    public float fireRate;

    // fire rate counter
    [HideInInspector] public float fireCounter;

    // the current amount of ammo of the weapon
    public int currentAmmo;

    // the maximum amount of ammo of the weapon
    public int maximumAmmo;

    // the amount of ammo the player picks up
    public int pickupAmount;

    // reference to the weapon's fire point transform component
    public Transform firepoint;

    // how much zoom the weapon has
    public float zoomAmount;

    // name of the weapon
    public string weaponName;






    // fire rate countdown timer of the player's weapon
    void Update()
    {
        FireRate();
    }


    private void FireRate()
    {
        // if the fire rate counter is greater than zero
        if (fireCounter > 0)
        {
            // decrease the weapon's fire rate counter
            fireCounter -= Time.deltaTime;
        }
    }


    // add ammo pickup
    public void GetAmmo()
    {
        // increase the player's ammo count by the amount of the ammo pickup
        currentAmmo += pickupAmount;


        // update the ammo ui
        UIController.uiController.ammoSlider.maxValue = maximumAmmo;
        UIController.uiController.ammoSlider.value = currentAmmo;
        UIController.uiController.ammoText.text = currentAmmo + " / " + maximumAmmo;
    }


} // end of class
