/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Ensures that the loading screen displayed during scene transitions will actually appear
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            SceneHandler.LoadScreenCallback();
        }
    }
}
