
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    // the name of the next level to load
    public string nextLevel;

    // short pause before ending the level
    public float waitToEndLevel;



    private void OnTriggerEnter(Collider other)
    {
        // if the player walks into the portal
        if(other.CompareTag("Player"))
        {
            // set the 'level ending' flag in the game controller script to true
            GameController.instance.levelEnding = true;

            // allow a short pause before ending the level
            StartCoroutine(EndLevelCoroutine());

            // play the 'level victory' music
            AudioManager.instance.PlayLevelVictory();
        }
    }


    private IEnumerator EndLevelCoroutine()
    {
        // reset the playerprefs 'check point' key for the next level
        PlayerPrefs.SetString(nextLevel + "_cp", "");

        // set the name of the playerprefs 'current level' key to the name of the next level
        PlayerPrefs.SetString("CurrentLevel", nextLevel);

        // allow a short pause before ending the level
        yield return new WaitForSeconds(waitToEndLevel);

        // load the next level
        SceneManager.LoadScene(nextLevel);
    }


} // end of class
