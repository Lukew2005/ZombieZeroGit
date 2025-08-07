
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;


    public float maximumHealth = 100f;
    
    public float currentHealth;

    public float invincibleLength = 1f;

    private float invincCounter;
    
    
    //Luke's Updated Health Bar

    public Image healthBar;





    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;

        UIController.uiController.healthSlider.maxValue = maximumHealth;

        UIController.uiController.healthSlider.value = currentHealth;

        float healthPercentage = (float)currentHealth / maximumHealth * 100f;

        UIController.uiController.healthText.text = $"{healthPercentage}%";
    }


    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        }



        //Luke UI
        float fillAmount = currentHealth / maximumHealth;
        healthBar.fillAmount = fillAmount;
    }


    public void DamagePlayer(int damageAmount)
    {
        if (invincCounter <= 0 && !GameManager.instance.levelEnding)
        {
            AudioManager.instance.PlaySFX(7);

            currentHealth -= damageAmount;

            UIController.uiController.ShowDamage();

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);

                currentHealth = 0;

                GameManager.instance.PlayerDied();

                AudioManager.instance.StopBGM();

                AudioManager.instance.PlaySFX(6);

                AudioManager.instance.StopSFX(7);
            }

            invincCounter = invincibleLength;


            UIController.uiController.healthSlider.value = currentHealth;

            float healthPercentage = (float)currentHealth / maximumHealth * 100f;

            UIController.uiController.healthText.text = $"{healthPercentage}%";
        }
    }


    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }

        UIController.uiController.healthSlider.value = currentHealth;

        float healthPercentage = (float)currentHealth / maximumHealth * 100f;

        UIController.uiController.healthText.text = $"{healthPercentage}%";
    }


} // end of class
