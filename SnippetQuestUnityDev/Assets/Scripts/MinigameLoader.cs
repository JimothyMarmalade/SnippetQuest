/*
 * Created by Logan Edmund, 5/18/21
 * Last Modified by Logan Edmund, 5/18/21
 * 
 * Inherits from Interactable. Can trigger transition into a minigame scene.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLoader : Interactable
{
    public string MinigameSceneToLoad = "";
    public override void Interact()
    {
        //Revoke Player Control
        //PlayerController.Instance.DisableAllMovement();

        //Load minigame scene from the scene handler.
        if (MinigameSceneToLoad != "")
            Debug.Log("Attempting to load scene \"" + MinigameSceneToLoad + "\"");
        else
        {
            Debug.LogWarning("MinigameSceneToLoad has not been set!");

        }
    }
}
