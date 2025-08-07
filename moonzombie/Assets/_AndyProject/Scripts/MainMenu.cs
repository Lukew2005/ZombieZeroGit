
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // the name of the first level (scene) to load
    public string firstLevel;

    // reference to the menu 'continue' button
    public GameObject continueButton;



    // checks for a game 'continue' button
    private void Start()
    {
        ContinueButton();
    }


    // game 'continue' button
    private void ContinueButton()
    {
        // if 'playerprefs' has a 'continue' key entry
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            // if the 'playerprefs' key entry does not have a value
            if (PlayerPrefs.GetString("CurrentLevel") == "")
            {
                // disable the 'continue' button
                continueButton.SetActive(false);
            }
        }

        // otherwise
        // if 'playerprefs' doesn't have a 'continue' key entry
        else
        {
            // simply disable the 'continue' button
            continueButton.SetActive(false);
        }
    }


    // if the player has clicked the 'continue' button
    public void Continue()
    {
        // load the level saved in the playerprefs 'current level' key
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
    }


    // if the player has clicked the 'play' button, start playing the game
    public void PlayGame()
    {
        // load the 'level1' game scene
        SceneManager.LoadScene(firstLevel);

        // reset the playerprefs 'current level' key
        PlayerPrefs.SetString("CurrentLevel", "");

        // reset the 'first level loaded' playerprefs 'check point' key
        PlayerPrefs.SetString(firstLevel + "_cp", "");
    }


    // quit the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }


} // end of class
