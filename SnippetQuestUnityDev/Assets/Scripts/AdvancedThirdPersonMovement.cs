/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 3/7/21
 * 
 * Holds all data and methods relevant to the Player character;
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AdvancedThirdPersonMovement : MonoBehaviour
{
    //References to Components/other objects
    [Header("Character Controller Reference")]
    public CharacterController Controller;

    [Header("Game Controller References")]
    public NewUIController NewUIControllerReference;

    [Header("Camera References")]
    public CinemachineFreeLook CinemachineBrain;
    private string CMInputAxisNameX;
    private string CMInputAxisNameY;
    public Transform Cam;

    [Header("Movement/Interaction References")]
    public Transform GroundCheck;
    public float GroundDistance = 0.2f;
    public LayerMask groundMask;

    public LayerMask interactableMask;
    public Interactable focusedInteractable;
    //--------------------
    //Variables to be changed by the Developer
    public float Speed = 6;
    public float JumpHeight = 4;
    public float ForceGravity = -9.81f;
    public float TurnSmoothTime = 0.1f;
    //--------------------

    Vector3 PlayerVelocity;
    float SmoothTurnVelocity;

    bool isGrounded;
    bool hasFreeMovement;
    bool isInMenu;
    public bool isInSerenePlace;
    bool canInteract;


    private void Awake()
    {
        hasFreeMovement = true;
        isInMenu = false;
        isInSerenePlace = false;


        CMInputAxisNameX = CinemachineBrain.m_XAxis.m_InputAxisName;
        CMInputAxisNameY = CinemachineBrain.m_YAxis.m_InputAxisName;

        NewUIControllerReference = FindObjectOfType<NewUIController>();
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");
        //Camera should be disabled while Player is in a menu
        CinemachineBrain.m_XAxis.m_InputAxisName = !isInMenu ? CMInputAxisNameX : "";
        CinemachineBrain.m_YAxis.m_InputAxisName = !isInMenu ? CMInputAxisNameY : "";

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

        //Softlock gravity applied to player while grounded
        if (isGrounded && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = -2f;
        }

        //-----Checking for Deliberate Player Actions-------------
        //Player WASD Movement Availability Check
        if (hasFreeMovement)
        {
            CheckPlayerMovement();
        }

        CheckForInteractables();
        //"Interact Key Pressed" Heiarchy -- begin by checking if an interactable is being focused on
        if (Input.GetKeyDown("e"))
        {
            if (focusedInteractable != null)
            {
                InteractWith(focusedInteractable);
            }
        }
        

        //-----End Deliberate Action Check------------------------

        //Gravity
        PlayerVelocity.y += ForceGravity * Time.deltaTime;
        Controller.Move(PlayerVelocity * Time.deltaTime);


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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PlayerVelocity.y = Mathf.Sqrt(JumpHeight * -2f * ForceGravity);
        }
    }

    private void EnterSnippetMenu()
    {

    }

    private void CheckForInteractables()
    {
        //Debug.Log("Running CheckForInteractables");
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            //Debug.Log("Raycast hit gameobject: " + hit.collider.name);
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
            }
        }
        else
        {
            //Debug.Log("No Interactables detected");
            if (focusedInteractable != null)
                RemoveFocus();
        }
    }

    //----------Checking with Interactables
    private void SetFocus(Interactable i)
    {
        focusedInteractable = i;
    }
    private void RemoveFocus()
    {
        focusedInteractable = null;
    }
    private void InteractWith(Interactable i)
    {
        i.Interact();
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
            isInSerenePlace = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SerenePlace")
        {
            Debug.Log("Exited Triggerzone for a Serene Place");
            isInSerenePlace = false;
        }
    }

    //Debug Stuff



}
