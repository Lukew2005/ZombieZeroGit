
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStaminaController : MonoBehaviour
{
    public static PlayerStaminaController instance;


    public int maximumStamina;
    
    public int currentStamina;




    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maximumStamina;

        UIController.uiController.staminaSlider.maxValue = maximumStamina;

        UIController.uiController.staminaSlider.value = currentStamina;

        float staminaPercentage = (float)currentStamina / maximumStamina * 100f;

        UIController.uiController.healthText.text = $"{staminaPercentage}%";
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void DamagePlayer(int damageAmount)
    {
        /*if (invincCounter <= 0 && !GameManager.instance.levelEnding)
        {
            AudioManager.instance.PlaySFX(7);

            currentStamina -= damageAmount;

            UIController.uiController.ShowDamage();

            if (currentStamina <= 0)
            {
                gameObject.SetActive(false);

                currentStamina = 0;

                GameManager.instance.PlayerDied();

                AudioManager.instance.StopBGM();

                AudioManager.instance.PlaySFX(6);

                AudioManager.instance.StopSFX(7);
            }

            invincCounter = invincibleLength;


            UIController.uiController.oxygenSlider.value = currentStamina;

            float healthPercentage = (float)currentStamina / maximumStamina * 100f;

            UIController.uiController.healthText.text = $"{healthPercentage}%";
        }*/
    }


    public void HealPlayer(int healAmount)
    {
        currentStamina += healAmount;

        if (currentStamina > maximumStamina)
        {
            currentStamina = maximumStamina;
        }

        UIController.uiController.staminaSlider.value = currentStamina;

        float staminaPercentage = (float)currentStamina / maximumStamina * 100f;

        UIController.uiController.staminaText.text = $"{staminaPercentage}%";
    }


} // end of class
