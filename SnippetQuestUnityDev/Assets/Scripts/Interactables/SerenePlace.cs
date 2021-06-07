/*
 * Created by Logan Edmund, 3/7/21
 * Last Modified by Logan Edmund, 5/10/21
 * 
 * Script for SerenePlaces used to trigger related events such as pulling up the Snippet menu. Can (opefully) be applied to anything in order to turn it into
 * a Serene Place.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerenePlace : Interactable
{
    //Interact runs when the player approaches an NPC and makes a conscious choice to interact with them by pressing the Use button.
    public override void Interact()
    {
        Debug.Log("Interacting with a Serene Place");

        //Revoke Player Control
        PlayerController.Instance.DisableAllMovement();
        PlayerController.Instance.DisablePlayerCameraControl();

        //Turn on the relevant UI
        UIController.Instance.ActivateSnippetSelectionPanel();

        //Add a listener to the button that triggers leaving the Snippet selection panel, meaning each serene place can have unique leaving properties
        UIController.Instance.UI_SNIPPET.LeaveSnippetPanelButton.onClick.AddListener(LeaveSerenePlace);

        //Change music to Snippet Music
        AudioManager.Instance.BGMFocusSnippet(1.5f);


    }

    private void LeaveSerenePlace()
    {
        //Remove the UI Listener
        UIController.Instance.UI_SNIPPET.LeaveSnippetPanelButton.onClick.RemoveListener(LeaveSerenePlace);

        //Turn off the UI
        UIController.Instance.DeactivateSnippetSelectionPanel();

        //Return Player Control
        PlayerController.Instance.EnableAllMovement();
        PlayerController.Instance.EnablePlayerCameraControl();

        //Change Music to Exploration
        AudioManager.Instance.BGMFocusExploration(1.5f);
    }
}
