using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    public static FirstPersonMovement instance; //Singleton instance

    [SerializeField] private CameraController cam;
    
    [Header("Moving/Jumping")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 6;
    private float defaultSpeed;
    [SerializeField] private float gravity = -30;

    Vector3 _velocity;

    [Header("Ground")]
    bool _isGrounded;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    [Header("Freezing")] 
    private bool _isFrozen;
    
    [Header("Custom Events")]
    [SerializeField] private bool freezeOnStart = false;
    [SerializeField] private bool enableCamOnFreezeStart = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Check if there are errors in the custom events
        //This must come first
        CheckErrors();
        
        //Listen for pausing
        if (PauseMenuManager.Instance != null)
        {
            PauseMenuManager.Instance.OnPausedStatusChanged.AddListener(OnPausedChanged);
        }
        
        defaultSpeed = speed;

        if (freezeOnStart)
        {
            FreezePlayer();

            if (enableCamOnFreezeStart)
            {
                //Used if you want to freeze the player on start but allow the camera to work
                cam.FreezeToggle(false);
            }
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_isFrozen)
            {
                UnfreezePlayer();
            }
            else
            {
                FreezePlayer();
            }
        }

        GravityFunction();

        if (_isFrozen)
        {
            return;
        }
        
        WalkingFunction();
    }

    void GravityFunction()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; //Add a small downward force
        }
        
        _velocity.y += gravity * Time.deltaTime; //Add the gravity
        controller.Move(_velocity * Time.deltaTime); //Move the player vertically
    }

    //Walking
    void WalkingFunction()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Setting up the GroundCheck gameobject to check for ground

        if (_isGrounded && _velocity.y < 0) //Making sure you are grounded by forcing the player onto the ground
        {
            _velocity.y = -2f;
        }

        //Getting the movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Moving the player
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (speed * Time.deltaTime));

        _velocity.y += gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);
    }

    public void FreezePlayer()
    {
        _isFrozen = true;

        speed = 0;

        _velocity.x = 0f;
        _velocity.z = 0f;
        
        cam.FreezeToggle(true);
    }

    public void UnfreezePlayer()
    {
        _isFrozen = false;

        speed = defaultSpeed;
        
        cam.FreezeToggle(false);
    }

    private void CheckErrors()
    {
        if (enableCamOnFreezeStart == true && freezeOnStart == false)
        {
            Debug.LogWarning("enableCamOnFreezeStart should be set to false if freezeOnStart is false. \n Fixing error.");

            enableCamOnFreezeStart = false;
        }
    }
    
    //For the pause
    private void OnPausedChanged(bool isPaused)
    {
        if (isPaused)
        {
            FreezePlayer();
        }
        else
        {
            UnfreezePlayer();
        }
    }
}