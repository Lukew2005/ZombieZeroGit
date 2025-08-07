
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController uiController;


    public Slider healthSlider;
    public Slider staminaSlider;
    public Slider oxygenSlider;
    public Slider ammoSlider;

    public TMP_Text healthText;
    public TMP_Text staminaText;
    public TMP_Text oxygenText;
    public TMP_Text ammoText;

    public Image damageEffect;
    public float damageAlpha = .25f, damageFadeSpeed = 2f;

    public GameObject pauseScreen;

    public Image blackScreen;
    public float fadeSpeed = 1.5f;





    private void Awake()
    {
        uiController = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (damageEffect.color.a != 0)
        {
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));
        }

        if (!GameManager.instance.levelEnding)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        } 
        
        else
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }


    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, .25f);
    }


} // end of class
