/*
 * Created by Logan Edmund, 5/17/21
 * Last Modified by Logan Edmund, 5/17/21
 * 
 * Objects with this script will be hard-coded to always be facing the camera.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("PlayerCamera").transform != null)
            cam = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
    }

    private void LateUpdate()
    {
        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
        else
            transform.LookAt(transform.position + cam.forward);
    }


}
