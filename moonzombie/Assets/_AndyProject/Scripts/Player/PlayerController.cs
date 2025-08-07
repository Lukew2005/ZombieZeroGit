
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // enable other scripts to access this script
    public static PlayerController instance;


    // player's movement speed
    public float moveSpeed = 8f;

    // how much gravity is applied to the player
    public float gravityModifier = 2f;

    // moon's gravity
    public float moonGravity = -1.62f;

    // how much force is applied to the player to make them jump
    public float jumpPower = 10f;

    // player's run speed
    public float runSpeed = 12f;

    // reference to player character controller component
    public CharacterController characterController;

    // player input values
    private Vector3 moveInput;

    // reference to the camera rig position transform component
    public Transform cameraRigPosition;

    // mouse movement sensitivity
    public float mouseSensitivity = 2f;

    // mouse movement reversal flags
    public bool invertX;
    public bool invertY;


    // if player can jump / double jump
    private bool canJump;
    private bool canDoubleJump;

    // reference to the player's ground check position transform component
    public Transform groundCheckPoint;

    // checks if the player is walking on the ground lanyer
    public LayerMask whatIsGround;

    // reference to player animator component
    public Animator playerAnimator;

    // reference to the player's weapon fire position transform component
    public Transform firePoint;
  
    // create and initialise a list of all available weapons
    public List<Gun> allGuns = new List<Gun>();

    // create and initialise a list of unlockable weapons
    public List<Gun> unlockableGuns = new List<Gun>();

    // reference to the player's 'gun' script
    public Gun activeGun;

    // currently active weapon index
    public int currentGun;

    // the minimum number of weapons the player has to have to switch weapons
    private int minimumWeaponCount = 2;

    // reference to the weapon aim adjustment position transform component
    public Transform weaponAimAdjustmentPoint;

    // reference to the player's weapon holder transform component
    public Transform gunHolder;

    // weapon start position
    private Vector3 gunStartPosition;

    // weapon aim adjustment speed
    public float weaponAimAdjustmentSpeed = 2f;

    // reference to player's weapon muzzle flash game oject
    public GameObject muzzleFlash;

    // reference to player footstep audio
    public AudioSource footstepFast;
    public AudioSource footstepSlow;

    // how high the player bounces if they walk onto a bounce pad
    private float bounceAmount;

    // if the player has bounced
    private bool bounce;




    private void Awake()
    {
        instance = this;
    }


    // set the weapon the player starts the level with
    private void Start()
    {
        SetStartWeapon();
    }


    // get player input
    private void Update()
    {
        PlayerInput();
    }


    private void SetStartWeapon()
    {
        // decrement the 'current weapon' index
        ///currentGun--;

        // select a weapon
        ///SwitchGun();

        // set the weapon's position starting to the player's weapon holder position
        gunStartPosition = gunHolder.localPosition;
    }


    private void PlayerInput()
    {
        // if the pause screen is not active and the game level is not ending
        if (!UIController.uiController.pauseScreen.activeInHierarchy && !GameController.instance.levelEnding)
        {          
            MovementControl();

            JumpControl();

            MovePlayer();

            CameraControl();

            WeaponControl();
        }
    }


    private void MovementControl()
    {
        // temporary store for player's 'moveinput y' value if the player leaves the ground
        float yAxisInput = moveInput.y;

        // get player's vertical 'z' axis movement
        Vector3 verticleMovement = transform.forward * Input.GetAxis("Vertical");

        // get player's horizontal 'x' axis movement
        Vector3 horizontalMoveement = transform.right * Input.GetAxis("Horizontal");

        // set the player's movement input
        moveInput = horizontalMoveement + verticleMovement;

        // normalise the player's input value
        moveInput.Normalize();


        // if the player presses the 'left shift' key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // player is running
            moveInput *= runSpeed;
        }

        // otherwise
        else
        {
            // player is walking
            moveInput *= moveSpeed;
        }

        // set the gravity to simulate the moon's gravity
        ///Physics.gravity = new Vector3(0, moonGravity, 0);

        // reset the 'moveinput y' value of the player from the temporary store
        moveInput.y = yAxisInput;

        // apply gravity to the player
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        // if the player is on the ground
        if (characterController.isGrounded)
        {
            // set the 'moveinput y' value to the gravity value
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
    }


    private void MovePlayer()
    {
        characterController.Move(moveInput * Time.deltaTime);

        AnimatePlayer();
    }


    private void AnimatePlayer()
    {
        // if the player is on the ground, animate the player's movement
        playerAnimator.SetFloat("moveSpeed", moveInput.magnitude);

        playerAnimator.SetBool("onGround", canJump);
    }


    private void JumpControl()
    {
        // set the 'can jump' flag if the player is on the 'ground' layer
        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;


        // if the player can jump and they have pressed the 'space' key
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            // set the player's 'moveinput y' value to the player's 'jump' value
            moveInput.y = jumpPower;

            // allow the player to 'double jump'
            canDoubleJump = true;

            // play the jump sound
            AudioManager.instance.PlaySFX(8);
        }

        // otherwise
        // if the player can 'double jump' and they have pressed the 'space' key
        else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            // set the player's 'moveinput y' value to the player's 'jump' value
            moveInput.y = jumpPower;

            // disable 'double jump'
            canDoubleJump = false;

            // play the jump sound
            AudioManager.instance.PlaySFX(8);
        }


        // if the player has bounced
        if (bounce)
        {
            // set the 'player has bounced' flag to false
            bounce = false;

            // set the player's 'moveinput y' value to the player's 'bounce amount' value
            moveInput.y = bounceAmount;

            // allow the player to 'double jump'
            canDoubleJump = true;
        }
    }


    private void CameraControl()
    {
        // get camera rotation input
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        // if the 'invertx' flag is set
        if (invertX)
        {
            // reverse camera movement along the 'y' axis
            mouseInput.x = -mouseInput.x;
        }

        // if the 'inverty' flag is set
        if (invertY)
        {
            // reverse camera movement along the 'x' axis
            mouseInput.y = -mouseInput.y;
        }

        // rotate the player around the 'y' axis - camera rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        // rotate the camera around the 'x' axis - camera tilt
        cameraRigPosition.rotation = Quaternion.Euler(cameraRigPosition.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }


    private void WeaponControl()
    {
        // disable the weapon muzzle flash
        muzzleFlash.SetActive(false);


        // if the player has a weapon to fire
        if (allGuns.Count != 0)
        {

            // single shots //
            // if the player presses the 'left' mouse button and the weapon's 'fire rate' counter is less than or equal to zero
            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                // initialise a 'ray cast' hit point
                RaycastHit hit;

                // start the 'ray cast' from the camera rig position and send it out 'fifty' units it in a 'forward' direction
                if (Physics.Raycast(cameraRigPosition.position, cameraRigPosition.forward, out hit, 50f))
                {
                    // if the ray cast 'hits' something that is greater than two units from the player
                    if (Vector3.Distance(cameraRigPosition.position, hit.point) > 2f)
                    {
                        // direct the 'fire point' of the player's weapon at the 'hit point' of the ray cast
                        firePoint.LookAt(hit.point);
                    }
                }

                // otherwise
                else
                {
                    // simply direct the 'fire point' of the player's weapon straight ahead thirty units from the camera rig position
                    firePoint.LookAt(cameraRigPosition.position + (cameraRigPosition.forward * 30f));
                }


                // fire the weapon
                FireShot();
            }


            // repeating shots //
            // if the player presses the 'left' mouse button and the weapon can 'auto fire'
            if (Input.GetMouseButton(0) && activeGun.canAutoFire)
            {
                // if the weapon's 'fire rate' counter is less than or equal to zero
                if (activeGun.fireCounter <= 0)
                {
                    // fire the weapon
                    FireShot();
                }
            }

            // if the player presses the 'tab' key
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // if the player has more than one weapon
                if (allGuns.Count >= minimumWeaponCount)
                {
                    // select another weapon
                    SwitchGun();
                }
            }

            // if the player presses the 'right' mouse button
            if (Input.GetMouseButtonDown(1))
            {
                // zoom 'in' the camera on the target the player is aiming at
                CameraController.instance.ZoomIn(activeGun.zoomAmount);
            }

            // if the player presses the 'right' mouse button
            if (Input.GetMouseButton(1))
            {
                // move the player's weapon to the aim position of the player
                gunHolder.position = Vector3.MoveTowards(gunHolder.position, weaponAimAdjustmentPoint.position, weaponAimAdjustmentSpeed * Time.deltaTime);
            }

            // otherwise
            // if the player releases the 'right' mouse button
            else
            {
                // return the player's weapon to its 'default' firing position
                gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPosition, weaponAimAdjustmentSpeed * Time.deltaTime);
            }


            // if the player releases the right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                // zoom 'out' the camera from the target the player was aiming at
                CameraController.instance.ZoomOut();
            }

        }
    }


    // fire the weapon
    public void FireShot()
    {
        // if the currently acive weapon has ammo
        if (activeGun.currentAmmo > 0)
        {
            // decrease the weapon's ammo by one round
            activeGun.currentAmmo--;

            // create a projectile at the weapon's 'fire point' position
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);

            // set the weapon's fire rate
            activeGun.fireCounter = activeGun.fireRate;

            // update the weapon's ui
            UIController.uiController.ammoSlider.value = activeGun.currentAmmo;
            UIController.uiController.ammoText.text = activeGun.currentAmmo + " / " + activeGun.maximumAmmo;

            // show the weapon's muzzle flash
            muzzleFlash.SetActive(true);
        }
    }


    // if the player presses the 'tab' key, switch to another weapon
    public void SwitchGun()
    {
        // deactivate the currently active weapon
        activeGun.gameObject.SetActive(false);

        // increment the 'current gun' index
        currentGun++;

        // if the 'current gun' index is greater than or equal to the number of available weapons
        if (currentGun >= allGuns.Count)
        {
            // set the 'current gun' index to zero
            currentGun = 0;
        }

        // set the selected weapon to be the currently active weapon
        activeGun = allGuns[currentGun];

        // activate the weapon
        activeGun.gameObject.SetActive(true);

        // update the selected weapon's ui
        UIController.uiController.ammoSlider.maxValue = activeGun.maximumAmmo;
        UIController.uiController.ammoSlider.value = activeGun.currentAmmo;
        UIController.uiController.ammoText.text = activeGun.currentAmmo + " / " + activeGun.maximumAmmo;

        // set the 'fire position' of the selected weapon
        firePoint.position = activeGun.firepoint.position;
    }


    // add the weapon picked up by the player to the player's 'all weapons' list
    public void AddGun(string weaponToAdd)
    {
        // initialise a flag to indicate the collected weapon is locked
        bool gunUnlocked = false;

        // if the number of available 'unlockable' weapons in the player's 'unlockable' weapons list is greater than zero
        if (unlockableGuns.Count > 0)
        {
            // loop through the number of 'unlockable' weapons in the list
            for (int weaponIndex = 0; weaponIndex < unlockableGuns.Count; weaponIndex++)
            {
                Debug.Log(unlockableGuns[weaponIndex].weaponName);
                // if the weapon 'name' at the current weapon index in the list is equal to the weapon 'name' picked up by the player
                if (unlockableGuns[weaponIndex].weaponName == weaponToAdd)
                {
                    // unlock the weapon at the current weapon index
                    gunUnlocked = true;

                    // add the now 'unlocked' weapon to the player's 'all weapons' list
                    allGuns.Add(unlockableGuns[weaponIndex]);

                    // 'remove' the 'unlockable' weapon from the 'unlockable' weapons list
                    unlockableGuns.RemoveAt(weaponIndex);

                    // and set the weapon index to the number of 'unlockable' weapons
                    weaponIndex = unlockableGuns.Count;
                }
            }        
        }

        // if the collected weapon has been 'unlocked'
        if (gunUnlocked)
        {
            // set the 'current' weapon index to the 'collected' weapon index
            currentGun = allGuns.Count - 2;

            // and switch the player's 'current' weapon to the 'collected' weapon
            SwitchGun();
        }
    }


    // when the player walks onto a bounce pad
    public void Bounce(float bounceForce)
    {
        // apply the bounce pad's 'bounce force'
        bounceAmount = bounceForce;

        // set the 'player has bounced' flag
        bounce = true;
    }


} // end of class
