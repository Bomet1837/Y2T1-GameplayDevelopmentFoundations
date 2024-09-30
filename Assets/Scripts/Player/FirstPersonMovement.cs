using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("Player")]
    public GameObject defaultCapsule;

    [Header("Game rules")]
    bool canJump_GameRule = true;
    bool canSprint_GameRule = true;
    bool canCrouch_GameRule = true;

    [Header("Moving/Jumping")]
    public CharacterController controller;

    public float originalSpeed = 12;
    float speed = 12;
    public float sprintSpeed = 20;
    public float crouchSpeed = 5;
    public float gravity = -30;
    public float jumpHeight = 2f;

    float currentSpeed; //Used for getting the speed of the joystick (forward)
    float newCurrentSpeed; //Used for getting the speed of the joystick (reversed)

    Vector3 velocity;

    float x;

    bool isMoving;

    bool isJumping;

    string currentFloor;

    [Header("Sprinting")]
    public float defaultSprintTime;
    float sprintTime;
    private bool regeneratingSprint;

    bool isSprinting;
    private bool isRegeneratingSprint;

    public Slider sprintSlider;

    public GameObject sprintWalkingIcon;
    public GameObject sprintRunningIcon;
    public GameObject sprintCrouchingIcon;
    public GameObject sprintEmptyIcon;

    [Header("Ground")]
    bool isGrounded;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Transform groundCheck;

    [Header("Crouching")]
    Vector3 crouchScale = new Vector3(1, 0.5f, 1); //The players crouch height
    Vector3 playerScale = new Vector3(1, 1, 1); //The players original height
    bool isRoofAbove;
    bool stillCrouching = false;
    public float roofDistance = 0.4f;
    public LayerMask roofMask;
    public Transform roofCheck;

    [Header("Audio")]
    public AudioSource[] FootstepGrass;
    public AudioSource[] FootstepStone;
    public AudioSource[] FootstepWater;
    public AudioSource[] FootstepDirt;

    public AudioSource grassJumpingAudio;
    public AudioSource stoneJumpingAudio;
    public AudioSource waterJumpingAudio;
    public AudioSource dirtJumpingAudio;

    bool playingWalkingSound = false;

    public AudioSource openInventoryAudio;
    public AudioSource closeInventoryAudio;

    private float walkingAudioSpeed = 1f;

    void Start()
    {
        //Sprinting
        sprintTime = defaultSprintTime; //Setting the private sprintTime to the public defaultSprintTime

        sprintSlider.maxValue = defaultSprintTime;

        //Disabling default capsule (for shadows)
        if(defaultCapsule != null)
        {
            defaultCapsule.SetActive(false);
        }
    }
    void Update()
    {
        WalkingFunction();

        if (canCrouch_GameRule == true)
        {
            CrouchingFunction();
        }

        SpeedFunction();

        if (canJump_GameRule == true)
        {
            JumpFunction();
        }

        if (canSprint_GameRule)
        {
            SprintFunction();
        }
    }

    //Walking
    void WalkingFunction()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Setting up the GroundCheck gameobject to check for ground
        isRoofAbove = Physics.CheckSphere(roofCheck.position, roofDistance, roofMask);

        if (isGrounded && velocity.y < 0) //Making sure you are grounded by forcing the player onto the ground
        {
            velocity.y = -2f;
        }

        //Getting the movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Getting the joystick axis amount for audio
        currentSpeed = Mathf.Abs(x) + Mathf.Abs(z); //Sets the current speed to the amount on the joystick
        newCurrentSpeed = Mathf.InverseLerp(0.9f, 0.6f, currentSpeed); //Reverses the currentSpeed so it will start at the slowest and move up


        //Moving the player
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Checking if you are moivng
        if (x != 0 || z != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }



    //Jumping
    public void JumpFunction()
    {
        //Jumping
        if (isGrounded && Input.GetButtonDown("Jump")) //Checking if the jump button is pressed and you are grounded
        {
            if (currentFloor == "GroundGrass")
            {
                grassJumpingAudio.Play();
            }
            else if (currentFloor == "GroundStone")
            {
                stoneJumpingAudio.Play();
            }
            else if (currentFloor == "GroundWater")
            {
                waterJumpingAudio.Play();
            }
            else if (currentFloor == "GroundDirt")
            {
                dirtJumpingAudio.Play();
            }

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //jump
        }
    }



    //Crouching/Sprinting speed
    void SpeedFunction()
    {
        //Checking if you are crouching here as limiting the speed does not work in the crouching section of the script (it does not automatically switch when the crouching button is pressed
        if (stillCrouching == true) //If you are crouching
        {
            if (newCurrentSpeed < 0.8f) //If the fastest speed is less than than 0.5f
            {
                newCurrentSpeed = 0.8f; //Set the speed to 0.5f (Limiting the value so there are no audio glitches with the speed going too fast)
            }

        }
        else if (isSprinting == true) //If you are sprinting
        {
            if (newCurrentSpeed < 0.3f) //If the fastest speed is less than than 0.5f
            {
                newCurrentSpeed = 0.3f; //Set the speed to 0.5f (Limiting the value so there are no audio glitches with the speed going too fast)
            }

            else if (stillCrouching == false && newCurrentSpeed < 0.5f) //If the fastest speed is less than than 0.5f
            {
                newCurrentSpeed = 0.5f; //Set the speed to 0.5f (Limiting the value so there are no audio glitches with the speed going too fast)
            }
        }
    }


    //Crouching
    public void CrouchingFunction()
    {
        if (Input.GetButtonDown("Crouch") && !stillCrouching) //Checks if the player has pressed the crouch button down and you are not already crouching
        {
            StartCrouching(); //Calls the StartCrouching function
        }
        else if (Input.GetButtonUp("Crouch") && !isRoofAbove || !isRoofAbove && stillCrouching && Input.GetButton("Crouch") == false) //Checks if you have let go of the crouch button and there is no roof above or there is no roof above, you are still crouchind and you are not holding the crouch button
        {
            StopCrouching(); //Calls the StopCrouching function
        }
    }

    public void StartCrouching()
    {
        speed = crouchSpeed; //Slowing the player down whilst they are crouching
        transform.localScale = crouchScale; //Setting the player to 50% its original height
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z); //Moving the player down rather than falling
        stillCrouching = true;
    }



    public void StopCrouching()
    {
        speed = originalSpeed; //Setting the speed back to default
        transform.localScale = playerScale; //Setting the player to its original height
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); //Moving the player up so it doesn't clip through the floor
        stillCrouching = false;
    }


    //Sprinting
    void SprintFunction()
    {
        //Sprinting
        if (Input.GetButtonDown("Sprint") && isRegeneratingSprint == false && stillCrouching == false && isMoving == true) //If you press the sprint button and you are not regenerating sprint and are not crouching
        {
            isSprinting = true; //Start sprinting
            isRegeneratingSprint = false; //Stop regenerating sprint

            sprintRunningIcon.SetActive(true);
            sprintWalkingIcon.SetActive(false);
            sprintCrouchingIcon.SetActive(false);
            sprintEmptyIcon.SetActive(false);
        }
        if (Input.GetButtonUp("Sprint")) //if you let go of the sprint button
        {
            isSprinting = false; //Stop sprinting
            isRegeneratingSprint = true; //Regenerate sprint

            //Set the icon to active

            sprintWalkingIcon.SetActive(true);
            sprintRunningIcon.SetActive(false);
            sprintCrouchingIcon.SetActive(false);
            sprintEmptyIcon.SetActive(false);
        }
        if (isSprinting == true && sprintTime > 0) //If you are sprinting and the sprint time is more than 0
        {
            speed = sprintSpeed; //Change the speed of the player to sprintSpeed
            sprintTime -= Time.deltaTime; //Lower the sprintTime

            if (sprintTime < 0) //If the sprintTime is less than 0
            {
                isSprinting = false; //Stop sprinting
                isRegeneratingSprint = true; //Regenerate sprint

                //Set the icon to active

                sprintEmptyIcon.SetActive(true);
                sprintWalkingIcon.SetActive(false);
                sprintRunningIcon.SetActive(false);
                sprintCrouchingIcon.SetActive(false);
            }
        }
        else if (isSprinting == false && stillCrouching == false) //If you are not sprinting and not crouching
        {
            speed = originalSpeed; //Set the players speed to the originalSpeed
            isRegeneratingSprint = true; //Regenerate sprint
        }
        if (isRegeneratingSprint == true) //If you are regenerating sprint
        {
            if (sprintTime < defaultSprintTime) //If your sprintTime is less than the defaultSprintTime
            {
                sprintTime += Time.deltaTime; //Add the sprintTime over time

                if (!sprintEmptyIcon.active) //If the icon is not active
                {
                    //Set the icon to active

                    sprintEmptyIcon.SetActive(true);
                    sprintWalkingIcon.SetActive(false);
                    sprintRunningIcon.SetActive(false);
                    sprintCrouchingIcon.SetActive(false);
                }
            }
            else //Else (if your sprintTime is more than the defaultSprintTIme
            {
                isRegeneratingSprint = false; //Stop regenerating

                if (!sprintWalkingIcon.active) //If the icon is not active
                {
                    //Set the icon to active

                    sprintWalkingIcon.SetActive(true);
                    sprintRunningIcon.SetActive(false);
                    sprintEmptyIcon.SetActive(false);
                    sprintCrouchingIcon.SetActive(false);
                }
            }
        }
        if (stillCrouching == true)
        {
            sprintCrouchingIcon.SetActive(true);
            sprintWalkingIcon.SetActive(false);
            sprintRunningIcon.SetActive(false);
            sprintEmptyIcon.SetActive(false);
        }

        sprintSlider.value = sprintTime;
    }



    //Collision
    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if(isSprinting == true)
        {
            walkingAudioSpeed = sprintSpeed / 15;
        }
        else
        {
            walkingAudioSpeed = originalSpeed / 5;
        }

        //Detecting what you are ground standing on for audio
        //TODO: REMOVE THE BLOODY IF STATEMENTS, MAKE IT SIMPLER
        if (other.gameObject.tag == "GroundGrass") //Checking if the player is on a ground with the "GroundGrass" tag
        {
            currentFloor = "GroundGrass";

            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) //Checking if the player is moving (if the input of the Horizontal or Vertical axis are not equal to 0)
            {
                StartCoroutine(WalkAudio(FootstepGrass[Random.Range(0, FootstepGrass.Length)])); //Plays audio in IEnumeraor as we have to wait before playing the sound again
            }
        }
        else if (other.gameObject.tag == "GroundStone")
        {
            currentFloor = "GroundStone";

            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                StartCoroutine(WalkAudio(FootstepStone[Random.Range(0, FootstepStone.Length)]));
            }
        }
        else if (other.gameObject.tag == "GroundWater")
        {
            currentFloor = "GroundWater";

            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                StartCoroutine(WalkAudio(FootstepWater[Random.Range(0, FootstepWater.Length)]));
            }
        }
        else if (other.gameObject.tag == "GroundDirt")
        {
            currentFloor = "GroundDirt";

            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                StartCoroutine(WalkAudio(FootstepDirt[Random.Range(0, FootstepDirt.Length)]));
            }
        }
        else
        {
            currentFloor = null;
        }
    }



    //The walking audio
    IEnumerator WalkAudio(AudioSource inAudio)
    {
        if (playingWalkingSound == false)
        {
            inAudio.Play(); //Plays audio from the array using the randomly generated int

            playingWalkingSound = true;
            yield return new WaitForSeconds(walkingAudioSpeed); //Waits before playing the sound again, uses the joystick amount to determine how fast the player is moving
            playingWalkingSound = false;
        }
    }
}