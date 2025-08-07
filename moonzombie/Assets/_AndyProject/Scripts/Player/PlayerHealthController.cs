
using UnityEngine;


public class PlayerHealthController : MonoBehaviour
{
    // enable other scripts to access this script
    public static PlayerHealthController instance;


    // player's current health
    public int currentHealth;

    // player's maximum health
    public int maximumHealth = 100;

    // how long the player is invicible for
    public float invincibleLength = 1f;

    // invicibility countdown timer
    private float invincibleCounter;




    private void Awake()
    {
        instance = this;
    }



    // initialise the player's health
    private void Start()
    {
        InitialiseHealth();
    }


    // player invincibility
    private void Update()
    {
        PlayerInvincibility();
    }


    private void PlayerInvincibility()
    {
        // if the player's 'invincibility' counter is greater than zero
        if (invincibleCounter > 0)
        {
            // countdown the player's 'invincibility' counter
            invincibleCounter -= Time.deltaTime;
        }
    }


    // initialise the player's health
    private void InitialiseHealth()
    {
        // get the player's current health
        currentHealth = maximumHealth; // GameData.gameDataInstance.currentHealth;

        // update the player ui health slider values
        UIController.uiController.healthSlider.maxValue = maximumHealth; // GameData.gameDataInstance.maximumHealth;
        UIController.uiController.healthSlider.value = currentHealth;

        // convert the player health value to a percentage
        float healthPercentage = (float)currentHealth / maximumHealth * 100f; // GameData.gameDataInstance.maximumHealth * 100f;

        // update the player ui health text 
        UIController.uiController.healthText.text = $"{healthPercentage}%";
    }


    // damage the player
    public void DamagePlayer(int damageAmount)
    {
        // if the player's 'invicible' counter is less than or equal to zero and the current game level is not ending
        if (invincibleCounter <= 0 && !GameController.instance.levelEnding)
        {
            // play the 'player hurt' sound
            AudioManager.instance.PlaySFX(7);

            // decrease the player's health by the 'damage amount'
            currentHealth -= damageAmount;

            // show player damage
            UIController.uiController.ShowDamage();

            // if the player's health is less than or equal to zero
            if (currentHealth <= 0)
            {
                // deactivate the player
                gameObject.SetActive(false);

                // set the player's current health to zero
                currentHealth = 0;

                // respawn the player at the player's starting position
                GameController.instance.PlayerDied();

                // stop the background music
                AudioManager.instance.StopBackgroundMusic();

                // play 'player dead' sound
                AudioManager.instance.PlaySFX(6);

                // stop the 'player hurt' sound
                AudioManager.instance.StopSFX(7);
            }

            // reset the player's invincibility counter
            invincibleCounter = invincibleLength;

            // update the player ui health slider value
            UIController.uiController.healthSlider.value = currentHealth;

            // convert the player health value to a percentage
            float healthPercentage = (float)currentHealth / maximumHealth * 100f; // GameData.gameDataInstance.maximumHealth * 100f;

            // update the player ui health ui text 
            UIController.uiController.healthText.text = $"{healthPercentage}%";
        }
    }


    // heal the player
    public void HealPlayer(int healAmount)
    {
        // increment the player's health by the 'heal amount'
        //GameData.gameDataInstance.currentHealth += healAmount;
        currentHealth += healAmount;

        // if the player's current health is greater than the player's maximum health
        //if (GameData.gameDataInstance.currentHealth > GameData.gameDataInstance.maximumHealth)
        if (currentHealth > maximumHealth)
        {
            // then set the player's current health to the player's maximum health
            //GameData.gameDataInstance.currentHealth = GameData.gameDataInstance.maximumHealth;
            currentHealth = maximumHealth;
        }

        // update the player ui health slider value
        UIController.uiController.healthSlider.value = currentHealth; // GameData.gameDataInstance.currentHealth;

        // convert the player health value to a percentage
        //float healthPercentage = (float)GameData.gameDataInstance.currentHealth / GameData.gameDataInstance.maximumHealth * 100f;
        float healthPercentage = (float)currentHealth / maximumHealth * 100f;

        // update the player ui health ui text 
        UIController.uiController.healthText.text = $"{healthPercentage}%";
    }


} // end of class
