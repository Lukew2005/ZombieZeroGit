
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VictoryScreen : MonoBehaviour
{
    // name of the main menu scene
    public string mainMenuScene;

    // how much time to allow before show victory screen text and  return button
    public float timeBetweenShowing = 1f;

    // reference to the victory screens 'text box' game object
    public GameObject textBox;

    // reference to the victory screens 'return button' game object
    public GameObject returnButton;

    // reference to the victory screens 'screen fader' image
    public Image blackScreen;

    // how long it takes to fade the screen
    public float blackScreenFade = 2f;



    
    // initialise the victory screen
    void Start()
    {
        InitialiseVictoryScreen();
    }


    // screen fader
    void Update()
    {
        ScreenFader();
    }


    private void InitialiseVictoryScreen()
    {
        // allow a short pause before showing the victory screen text and return button
        StartCoroutine(ShowObjectsCoroutine());

        // unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    private void ScreenFader()
    {
        // fade the screen from black to transparent
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFade * Time.deltaTime));
    }


    public void MainMenu()
    {
        // load the main menu scene
        SceneManager.LoadScene(mainMenuScene);
    }


    public IEnumerator ShowObjectsCoroutine()
    {
        // allow a short pause before showing the victory screen text
        yield return new WaitForSeconds(timeBetweenShowing);

        // show the victory screen text box
        textBox.SetActive(true);

        // allow a short pause before show the return button
        yield return new WaitForSeconds(timeBetweenShowing);

        // show the victory screen return button
        returnButton.SetActive(true);
    }


} // end of class
