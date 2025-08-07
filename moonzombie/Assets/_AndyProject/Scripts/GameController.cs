
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    // enable other scripts to access this script
    public static GameController instance;


    // time to wait after player dies before respawning player
    public float waitAfterDying = 2f;

    // when the level has been completed
    [HideInInspector] public bool levelEnding;




    private void Awake()
    {
        instance = this;
    }


    
    private void Start()
    {
        InitialiseCursor();
    }


    // get player input
    private void Update()
    {
        GetPlayerInput();
    }


    private void GetPlayerInput()
    {
        // if the player presses the 'escape' key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // pause / unpause the game
            PauseUnpause();
        }
    }


    private void InitialiseCursor()
    {
        // lock the mouse cursor to the game window and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // player has died
    public void PlayerDied()
    {
        // respawn player
        StartCoroutine(PlayerDiedCoroutine());
    }


    public IEnumerator PlayerDiedCoroutine()
    {
        // short pause before respawning player
        yield return new WaitForSeconds(waitAfterDying);

        // load the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // if the 'escape' key has been pressed, pause / unpause game play
    public void PauseUnpause()
    {
        // if the pause screen is active
        if (UIController.uiController.pauseScreen.activeInHierarchy)
        {
            // hide the pause screen
            UIController.uiController.pauseScreen.SetActive(false);

            // hide and lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // unfreeze the game play
            Time.timeScale = 1f;

            // and play the player footsteps sound
            PlayerController.instance.footstepFast.Play();
            PlayerController.instance.footstepSlow.Play();
        } 
        
        // otherwise
        else
        {
            // show the pause screen
            UIController.uiController.pauseScreen.SetActive(true);

            // unlock and show the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // freeze the game play
            Time.timeScale = 0f;

            // stop playing the player footsteps sound
            PlayerController.instance.footstepFast.Stop();
            PlayerController.instance.footstepSlow.Stop();
        }
    }


} // end of class
