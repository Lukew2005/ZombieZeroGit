
using UnityEngine;


public class CameraController : MonoBehaviour
{
    // enable other scripts to access this script
    public static CameraController instance;



    // reference to the camera's rig position transform component
    public Transform cameraRigPosition;

    // weapon's default field of view setting
    private float defaultFieldOfView;

    // weapon's 'zoom' field of view setting
    private float zoomFieldOfView;

    // how quickly the weapon changes its 'zoom' setting
    public float zoomSpeed = 1f;

    // reference to the player camera
    public Camera playerCamera;




    private void Awake()
    {
        instance = this;
    }


    // initialise the weapon's default field of view 
    private void Start()
    {
        InitialiseFieldOfView();
    }


    // adjust the camera's field of view based on the weapon's 'zoom' value
    private void LateUpdate()
    {
        AdjustCameraFieldOfView();
    }


    private void InitialiseFieldOfView()
    {
        // set the 'default' field of view to the camera's current field of view value
        defaultFieldOfView = playerCamera.fieldOfView;

        // set the weapon's 'zoom' field of view to the default value
        zoomFieldOfView = defaultFieldOfView;
    }


    private void AdjustCameraFieldOfView()
    {
        // set the camera position to the position of the camera rig
        transform.position = cameraRigPosition.position;

        // set the camera rotation to the rotation of the camera rig
        transform.rotation = cameraRigPosition.rotation;

        // adjust the camera's 'field of view' to the weapon's 'zoom' position
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFieldOfView, zoomSpeed * Time.deltaTime);
    }


    public void ZoomIn(float newZoom)
    {
        // zoom in weapon
        zoomFieldOfView = newZoom;
    }


    public void ZoomOut()
    {
        // return weapon to default zoom
        zoomFieldOfView = defaultFieldOfView;
    }


} // end of class
