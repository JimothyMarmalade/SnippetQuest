/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 3/7/21
 * 
 * Extends from the Interactable class and holds all methods and data required for NPCs.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    //Uses an NPCData class to hold all the data associated with the NPC - name, quest distribution, dialog trees
    public NPCData npcData;

    //Interact runs when the player approaches an NPC and makes a conscious choice to interact with them by pressing the Use button.
    public override void Interact()
    {
        Debug.Log("Called Interact from NPC, name: " + name);
        if (npcData != null)
        {
            Debug.Log("name from npcData: " + npcData.characterName);
            if (transform.gameObject.GetComponent<DialogueTrigger>() != null)
            {
                transform.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
        else
        {
            Debug.LogError("Error! NPC has no NPCData!");
        }
    }
}
