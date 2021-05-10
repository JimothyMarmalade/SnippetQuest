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
        FindObjectOfType<NewUIController>().ActivateSnippetSelectionPanel();
    }
}
