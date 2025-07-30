
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;


    public float moveSpeed, gravityModifier, jumpPower, runSpeed = 12f;
    public CharacterController charCon;

    private Vector3 moveInput;

    public Transform camTrans;

    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;

    //public GameObject bullet;
    public Transform firePoint;

    public Gun activeGun;
    public List<Gun> allGuns = new List<Gun>();
    public List<Gun> unlockableGuns = new List<Gun>();
    public int currentGun;

    public Transform adsPoint, gunHolder;
    private Vector3 gunStartPos;
    public float adsSpeed = 2f;

    public GameObject muzzleFlash;

    public AudioSource footstepFast, footstepSlow;

    private float bounceAmount;
    private bool bounce;




    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentGun--;

        SwitchGun();

        gunStartPos = gunHolder.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        if (!UIController.uiController.pauseScreen.activeInHierarchy && !GameManager.instance.levelEnding)
        {
            //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            //store y velocity
            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");

            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = horiMove + vertMove;

            moveInput.Normalize();


            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * runSpeed;
            }

            else
            {
                moveInput = moveInput * moveSpeed;
            }

            moveInput.y = yStore;

            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (charCon.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }

            canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;

            if (canJump)
            {
                canDoubleJump = false;
            }

            //Handle Jumping
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveInput.y = jumpPower;

                canDoubleJump = true;

                AudioManager.instance.PlaySFX(8);
            }

            else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = jumpPower;

                canDoubleJump = false;

                AudioManager.instance.PlaySFX(8);
            }


            if (bounce)
            {
                bounce = false;

                moveInput.y = bounceAmount;

                canDoubleJump = true;
            }

            charCon.Move(moveInput * Time.deltaTime);


            //control camera rotation
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            if (invertX)
            {
                mouseInput.x = -mouseInput.x;
            }

            if (invertY)
            {
                mouseInput.y = -mouseInput.y;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

            camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

            muzzleFlash.SetActive(false);

            //Handle Shooting
            //single shots
            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
                {
                    if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                    {
                        firePoint.LookAt(hit.point);
                    }
                }

                else
                {
                    firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
                }



                //Instantiate(bullet, firePoint.position, firePoint.rotation);
                FireShot();
            }

            //repeating shots
            if (Input.GetMouseButton(0) && activeGun.canAutoFire)
            {
                if (activeGun.fireCounter <= 0)
                {
                    FireShot();
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchGun();
            }

            if (Input.GetMouseButtonDown(1))
            {
                CameraController.instance.ZoomIn(activeGun.zoomAmount);
            }

            if (Input.GetMouseButton(1))
            {
                gunHolder.position = Vector3.MoveTowards(gunHolder.position, adsPoint.position, adsSpeed * Time.deltaTime);
            }

            else
            {
                gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, adsSpeed * Time.deltaTime);
            }



            if (Input.GetMouseButtonUp(1))
            {
                CameraController.instance.ZoomOut();
            }


            anim.SetFloat("moveSpeed", moveInput.magnitude);

            anim.SetBool("onGround", canJump);
        }
    }


    // fire the weapon
    public void FireShot()
    {
        // if the currently active weapon has ammo
        if (activeGun.currentAmmo > 0)
        {
            // subtract a round from the player's ammo
            activeGun.currentAmmo--;

            // instantiate a bullet at the fire point position of the weapon
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);

            // initialise the fire rate counter of the weapon
            activeGun.fireCounter = activeGun.fireRate;

            // update the weapon's ammo UI
            UIController.uiController.ammoSlider.value = activeGun.currentAmmo;

            UIController.uiController.ammoText.text = activeGun.currentAmmo + " / " + activeGun.maximumAmmo;


            // show the weapon's muzzle flash
            muzzleFlash.SetActive(true);
        }
    }


    // switch weapon's when the 'tab' key is pressed
    public void SwitchGun()
    {
        // deactivate the currently active weapon
        activeGun.gameObject.SetActive(false);

        // increment the weapon index
        currentGun++;

        // if the weapon index is greater than or equal thw weapon count
        if (currentGun >= allGuns.Count)
        {
            // set the weapon index to zero
            currentGun = 0;
        }

        // set the 'currently active' weapon to the selected weapon index
        activeGun = allGuns[currentGun];

        // and activate the 'currently active' weapon
        activeGun.gameObject.SetActive(true);


        // update the weapon's ammo UI
        UIController.uiController.ammoSlider.maxValue = activeGun.maximumAmmo;

        UIController.uiController.ammoSlider.value = activeGun.currentAmmo;

        UIController.uiController.ammoText.text = activeGun.currentAmmo + " / " + activeGun.maximumAmmo;


        // set the fire point position of the currently active weapon
        firePoint.position = activeGun.firepoint.position;
    }


    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;

        if (unlockableGuns.Count > 0)
        {
            for (int i = 0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;

                    allGuns.Add(unlockableGuns[i]);

                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                }
            }
            
        }

        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2;

            SwitchGun();
        }
    }


    public void Bounce(float bounceForce)
    {
        bounceAmount = bounceForce;

        bounce = true;
    }


} // end of class
