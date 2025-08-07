
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerOxygenController : MonoBehaviour
{
    public static PlayerOxygenController instance;


    public int maximumOxygen;
    
    public int currentOxygen;




    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = maximumOxygen;

        UIController.uiController.oxygenSlider.maxValue = maximumOxygen;

        UIController.uiController.oxygenSlider.value = currentOxygen;

        float oxygenPercentage = (float)currentOxygen / maximumOxygen * 100f;

        UIController.uiController.healthText.text = $"{oxygenPercentage}%";
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

            currentOxygen -= damageAmount;

            UIController.uiController.ShowDamage();

            if (currentOxygen <= 0)
            {
                gameObject.SetActive(false);

                currentOxygen = 0;

                GameManager.instance.PlayerDied();

                AudioManager.instance.StopBGM();

                AudioManager.instance.PlaySFX(6);

                AudioManager.instance.StopSFX(7);
            }

            invincCounter = invincibleLength;


            UIController.uiController.oxygenSlider.value = currentOxygen;

            float healthPercentage = (float)currentOxygen / maximumOxygen * 100f;

            UIController.uiController.healthText.text = $"{healthPercentage}%";
        }*/
    }


    public void HealPlayer(int healAmount)
    {
        currentOxygen += healAmount;

        if (currentOxygen > maximumOxygen)
        {
            currentOxygen = maximumOxygen;
        }

        UIController.uiController.oxygenSlider.value = currentOxygen;

        float oxygenPercentage = (float)currentOxygen / maximumOxygen * 100f;

        UIController.uiController.oxygenText.text = $"{oxygenPercentage}%";
    }


} // end of class
