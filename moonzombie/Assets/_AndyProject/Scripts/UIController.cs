
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    // enable other scripts to access this script
    public static UIController uiController;



    // player stats slider bars
    public Slider healthSlider;
    public Slider staminaSlider;
    public Slider oxygenSlider;
    public Slider ammoSlider;

    // text information for the slider bars
    public TMP_Text healthText;
    public TMP_Text staminaText;
    public TMP_Text oxygenText;
    public TMP_Text ammoText;


    // enemy stats health bar
    public Image enemyHealthBar;


    // reference to the player's damage effect image
    public Image damageEffect;

    // how transparent the player's damage effect is
    public float damageAlpha = .25f;

    // how quickly the player's damage effect fades away
    public float damageFadeSpeed = 2f;

    // reference to the pause screen game object
    public GameObject pauseScreen;

    // reference to screen overlay image
    public Image blackScreen;

    // how quickly the screen overlay fades away
    public float fadeSpeed = 1.5f;





    private void Awake()
    {
        uiController = this;
    }


    // screen fader effects
    private void Update()
    {
        VisualEffects();
    }


    private void VisualEffects()
    {
        // player //
        // if the screen colour's 'alpha' value is not equal to zero
        if (damageEffect.color.a != 0)
        {
            // fade out the player's damage effect
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));
        }

        // game level //
        // if the level is not ending
        if (!GameController.instance.levelEnding)
        {
            // fade the screen colour from black to transparent
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        // otherwise
        else
        {
            // fade the screen colour from transparent to black
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }


    // player damage effect
    public void ShowDamage()
    {
        // show a damage effect when the player is hit
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, .25f);
    }


} // end of class
