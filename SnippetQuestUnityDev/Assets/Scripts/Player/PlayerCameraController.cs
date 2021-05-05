/*
 * Created by Logan Edmund, 4/22/21
 * Last Modified by Logan Edmund, 4/22/21
 * 
 * Handles the positioning of the player camera. Most commonly, takes in user input to orbit it around the player character.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    public static PlayerCameraController Instance { get; set; }

    [Header("Camera References")]
    public CinemachineFreeLook CinemachineBrain;
    private string CMInputAxisNameX;
    private string CMInputAxisNameY;
    public Transform Cam;

    void Awake()
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnablePlayerCamControl()
    {

    }

    public void DisablePlayerCamControl()
    {

    }
}
