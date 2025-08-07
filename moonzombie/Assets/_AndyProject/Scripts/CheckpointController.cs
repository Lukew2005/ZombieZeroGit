
using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckpointController : MonoBehaviour
{
    // check point name
    public string checkpointName;



    // spawn player at check point
    void Start()
    {
        GetCheckpointName();
    }


    private void GetCheckpointName()
    {
        // if the playerprefs has a 'check point name' key
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            // get the name of the check point
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == checkpointName)
            {
                // and spawn the player at the check point position
                PlayerController.instance.transform.position = transform.position;
                Debug.Log("Player starting at " + checkpointName);
            }
        }
    }


    // get player input
    void Update()
    {
        GetPlayerInput();
    }


    // clear the check point name key
    private void GetPlayerInput()
    {
        // if the player presses the 'L' key
        if (Input.GetKeyDown(KeyCode.L))
        {
            // clear the playerprefs 'check point name' key in the currently active scene
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", "");
        }
    }


    // if the check point is triggered
    private void OnTriggerEnter(Collider checkpointTrigger)
    {
        // if the player has triggered a checkpoint
        if(checkpointTrigger.gameObject.CompareTag("Player"))
        {
            // set a playerprefs key and set the it to the 'check point name' in the currently active scene
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", checkpointName);
            Debug.Log("Player hit " + checkpointName);

            // play the 'check point triggered' sound
            AudioManager.instance.PlaySFX(1);
        }
    }


} // end of class
