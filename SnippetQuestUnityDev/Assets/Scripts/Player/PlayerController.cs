/*
 * Created by Logan Edmund, 5/10/21
 * Last Modified by Logan Edmund, 5/10/21
 * 
 * PlayerController is the "master" controller for the player and handles the enabling/disabling of all playerActions
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }

    public delegate void PlayerEvent();
    public static event PlayerEvent EnableAction;
    public static event PlayerEvent DisableAction;

    public PlayerAction playerCameraController; 
    public PlayerAction thirdPersonMovement;
    public PlayerAction playerJump;
    public PlayerAction playerWorldInteraction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null && Instance != this)
        {
            Instance = this;
        }

        playerCameraController = gameObject.GetComponent<PlayerCameraController>();
        thirdPersonMovement = gameObject.GetComponent<ThirdPersonMovement>();
        playerJump = gameObject.GetComponent<Jump>();
        playerWorldInteraction = gameObject.GetComponent<WorldInteraction>();
    }

    public void EnableAllMovement()
    {
        thirdPersonMovement.EnableAction();
        playerJump.EnableAction();
        playerWorldInteraction.EnableAction();
    }

    public void DisableAllMovement()
    {
        thirdPersonMovement.DisableAction();
        playerJump.DisableAction();
        playerWorldInteraction.DisableAction();
    }

    public void EnablePlayerCameraControl()
    {
        playerCameraController.EnableAction();

    }

    public void DisablePlayerCameraControl()
    {
        playerCameraController.DisableAction();
    }

}
