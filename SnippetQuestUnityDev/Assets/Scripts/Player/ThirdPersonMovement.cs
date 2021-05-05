/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 4/23/21
 * 
 * Holds all data/methods/variables needed to physically move the player (and handle the camera?)
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    //References to Components/other objects
    [Header("Character Controller Reference")]
    public CharacterController Controller;

    [Header("Camera Reference")]
    public Transform Cam;

    [Header("Speed/Turning")]
    public float Speed = 6;
    public float TurnSmoothTime = 0.1f;

    float SmoothTurnVelocity;
    bool hasFreeMovement;


    private void Awake()
    {
        hasFreeMovement = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (hasFreeMovement)
        {
            CheckPlayerMovement();
        }

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
    }

    private void EnterSnippetMenu()
    {

    }

    //Debug Stuff



}
