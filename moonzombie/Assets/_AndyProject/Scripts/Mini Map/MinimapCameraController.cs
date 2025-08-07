
using UnityEngine;


public class MinimapCameraController : MonoBehaviour
{
    // reference to the player's transform 
    public Transform player;


    #region MINI MAP CONTROLLER

    // reference to the mini map animator controller
    public Animator consoleAnimator;

    // whether minimap is active
    private bool minimapActive;

    // minimap game object
    [SerializeField] private GameObject miniMap;

    // whether the mini map console is hidden or displayed
    private const int CONSOLE_ACTIVE = 1;
    private const int CONSOLE_INACTIVE = -1;

    // console control modes
    [HideInInspector] public int consoleState;

    #endregion



    private void Start()
    {
        // show map console
        consoleState = CONSOLE_ACTIVE;

        SetConsoleState(CONSOLE_ACTIVE);
    }


    private void Update()
    {
        // switch minimap on/off
        ActivateMinimap();
    }



    private void LateUpdate()
    {
        // get position of player in minimap
        Vector3 newMapPosition = player.position;

        // set 'y' position of minimap
        newMapPosition.y = transform.position.y;

        // set new position of minimap
        transform.position = newMapPosition;

        // rotate map in direction of player
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }


    // select whether minimap is displayed
    private void ActivateMinimap()
    {
        // if the player presses the 'M' key
        if (Input.GetKeyDown(KeyCode.M))
        {
            // display the minimap depending upon its previous state
            consoleState = -consoleState;

            SetConsoleState(consoleState);
        }
    }


    private void SetConsoleState(int mapMode)
    {
        switch (mapMode)
        {
            // display the minimap
            case CONSOLE_ACTIVE:

                consoleAnimator.SetBool("consoleMode", true);

                break;

            // hide the minimap
            case CONSOLE_INACTIVE:

                consoleAnimator.SetBool("consoleMode", false);

                break;
        }
    }


} // end of class
