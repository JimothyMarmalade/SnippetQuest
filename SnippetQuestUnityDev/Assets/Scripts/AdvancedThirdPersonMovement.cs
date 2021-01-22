using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AdvancedThirdPersonMovement : MonoBehaviour
{
    //References to Components/other objects
    public CharacterController Controller;

    public UIController UIControllerReference;

    public CinemachineFreeLook CinemachineBrain;
    private string CMInputAxisNameX;
    private string CMInputAxisNameY;

    public Transform Cam;

    public Transform GroundCheck;
    public float GroundDistance = 0.2f;
    public LayerMask groundMask;
    //--------------------
    //Variables to be changed by the Developer
    public float Speed = 6;
    public float JumpHeight = 4;
    public float ForceGravity = -9.81f;
    public float TurnSmoothTime = 0.1f;
    //--------------------

    Vector3 PlayerVelocity;
    float SmoothTurnVelocity;

    bool PlayerIsGrounded;
    bool PlayerHasFreeMovement;
    bool PlayerIsInMenu;
    public bool PlayerIsInSerenePlace;


    private void Awake()
    {
        PlayerHasFreeMovement = true;
        PlayerIsInMenu = false;
        PlayerIsInSerenePlace = false;


        CMInputAxisNameX = CinemachineBrain.m_XAxis.m_InputAxisName;
        CMInputAxisNameY = CinemachineBrain.m_YAxis.m_InputAxisName;
    }


    // Update is called once per frame
    void Update()
    {
        //Camera should be disabled while Player is in a menu
        CinemachineBrain.m_XAxis.m_InputAxisName = !PlayerIsInMenu ? CMInputAxisNameX : "";
        CinemachineBrain.m_YAxis.m_InputAxisName = !PlayerIsInMenu ? CMInputAxisNameY : "";

        PlayerIsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

        //Softlock gravity applied to player while grounded
        if (PlayerIsGrounded && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = -2f;
        }

        //-----Checking for Deliberate Player Actions-------------
        //Player WASD Movement Availability Check
        if (PlayerHasFreeMovement)
            CheckPlayerMovement();

        if (!PlayerIsInMenu)
            CheckEnterSnippetMenu();
        else
            CheckExitSnippetMenu();

        //-----End Deliberate Action Check------------------------

        //Gravity
        PlayerVelocity.y += ForceGravity * Time.deltaTime;
        Controller.Move(PlayerVelocity * Time.deltaTime);

        //Debug stuff -- shouldn't be active in final submission
        DEBUGPlayerMenuStatusToggle();


    }

    private void CheckPlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref SmoothTurnVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDirection.normalized * Speed * Time.deltaTime);
        }
        //Jump
        if (Input.GetButtonDown("Jump") && PlayerIsGrounded)
        {
            PlayerVelocity.y = Mathf.Sqrt(JumpHeight * -2f * ForceGravity);
        }
    }

    private void CheckEnterSnippetMenu()
    {
        if (Input.GetKeyDown("e"))
            if (PlayerIsInSerenePlace)
            {
                //Lock player movement/Camera, display menu
                TogglePlayerMovement(false);
                TogglePlayerInMenu(true);
                UIControllerReference.ShowPanel(1);
            }
    }

    private void CheckExitSnippetMenu()
    {
        if (Input.GetKeyDown("escape"))
        {
            //unlock player movement/Camera, display menu
            TogglePlayerMovement(true);
            TogglePlayerInMenu(false);
            UIControllerReference.ShowPanel(0);
        }
    }

    private void TogglePlayerMovement(bool status)
    {
        PlayerHasFreeMovement = status;
    }

    private void TogglePlayerInMenu(bool status)
    {
        PlayerIsInMenu = status;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PuzzlePickup")
        {
            Debug.Log("Collected Puzzle Pickup with PuzzleID " + other.GetComponent<PuzzlePickup>().PuzzleID);
            other.GetComponent<PuzzlePickup>().Collect();
        }

        if (other.tag == "SerenePlace")
        {
            Debug.Log("Entered Triggerzone for a Serene Place");
            PlayerIsInSerenePlace = true;
        }

        if (other.tag == "MessageZone")
        {
            Debug.Log("Entered Triggerzone for a MessagePopup");
            UIControllerReference.UpdateHUDMessage(other.GetComponent<MessagePopup>().Message);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SerenePlace")
        {
            Debug.Log("Exited Triggerzone for a Serene Place");
            PlayerIsInSerenePlace = false;
        }
        else if (other.tag == "MessageZone")
        {
            Debug.Log("Exited Triggerzone for a MessagePopup");
            UIControllerReference.UpdateHUDMessage("");
        }
    }

    //Debug Stuff
    private void DEBUGPlayerMenuStatusToggle()
    {
        if (Input.GetKeyDown("p"))
        {
            if (PlayerIsInMenu)
                PlayerIsInMenu = false;
            else if (!PlayerIsInMenu)
                PlayerIsInMenu = true;
            else
                PlayerIsInMenu = true;

            if (PlayerHasFreeMovement)
                PlayerHasFreeMovement = false;
            else if (!PlayerHasFreeMovement)
                PlayerHasFreeMovement = true;
            else
                PlayerHasFreeMovement = true;
        }
    }





}
