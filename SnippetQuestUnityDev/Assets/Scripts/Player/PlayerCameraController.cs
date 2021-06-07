/*
 * Created by Logan Edmund, 4/22/21
 * Last Modified by Logan Edmund, 5/10/21
 * 
 * Handles the positioning of the player camera. Most commonly, takes in user input to orbit it around the player character.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : PlayerAction
{
    public static PlayerCameraController Instance { get; set; }

    [Header("Camera References")]
    public CinemachineFreeLook CinemachineBrain;
    private string CMInputAxisNameX;
    private string CMInputAxisNameY;
    public Transform Cam;

    [Header("Camera Movement Variables")]
    public float defaultMaxSpeedX;
    public float defaultMaxSpeedY;

    public override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null && Instance != this)
        {
            Instance = this;
        }

        CMInputAxisNameX = CinemachineBrain.m_XAxis.m_InputAxisName;
        CMInputAxisNameY = CinemachineBrain.m_YAxis.m_InputAxisName;

        base.Awake();
    }

    public override void DisableAction()
    {
        SetCamSpeed(0, 0);
        canPerform = false;
    }

    public override void EnableAction()
    {
        SetCamSpeed(defaultMaxSpeedX, defaultMaxSpeedY);
        canPerform = true;
    }

    public void SetCamSpeed(float XSPD, float YSPD)
    {

        CinemachineBrain.m_XAxis.m_MaxSpeed = XSPD;
        CinemachineBrain.m_YAxis.m_MaxSpeed = YSPD;
    }
}
