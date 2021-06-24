/*
 * Created by Logan Edmund, 6/23/21
 * Last Modified by Logan Edmund, 6/23/21
 * 
 * When interacted with, sends the player to a minigame.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLauncher : Interactable
{

    public SceneHandler.Scene MinigameScene;

    public override void Interact()
    {
        //Loads a minigame scene.
        GameManager.Instance.GoToScene(MinigameScene);
    }
}
