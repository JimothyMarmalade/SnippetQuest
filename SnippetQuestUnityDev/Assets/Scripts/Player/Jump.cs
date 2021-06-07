/*
 * Created by Logan Edmund, 4/23/21
 * Last Modified by Logan Edmund, 4/23/21
 * 
 * Handles the Player's jumping ability and all related variables/functions
 * Before creation of this script, this action was handled entirely within AdvancedThirdPersonMovement.cs
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerAction
{
    [Header("Movement/Interaction References")]
    public CharacterController Controller;
    public Transform GroundCheck;
    public float GroundDistance = 0.2f;
    public LayerMask groundMask;

    [Header("Jump Strength/Gravity")]
    public float JumpHeight = 4;
    public float ForceGravity = -9.81f;
    public float MaxForceGravity = -30f;

    Vector3 PlayerVelocity;
    public bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

        //Softlock gravity applied to player while grounded
        if (isGrounded && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = -2f;
        }

        //Jump
        if (Input.GetButtonDown("Jump") && canPerform && isGrounded)
        {
            PlayerVelocity.y = Mathf.Sqrt(JumpHeight * -2f * ForceGravity);
        }


        //Apply Gravity
        PlayerVelocity.y += ForceGravity * Time.deltaTime;
        PlayerVelocity.y = Mathf.Clamp(PlayerVelocity.y, MaxForceGravity, Mathf.Infinity);
        Controller.Move(PlayerVelocity * Time.deltaTime);
    }
}
