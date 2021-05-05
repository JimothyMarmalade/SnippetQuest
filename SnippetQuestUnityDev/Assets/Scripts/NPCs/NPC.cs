/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 4/27/21
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
    [SerializeField]
    public DialogueTrigger DT { get; set; }

    private void Start()
    {
        if (npcData == null)
        {
            Debug.LogError("Missing NPCData for " + gameObject.name);
        }
        DT = gameObject.GetComponent<DialogueTrigger>();
        if (DT == null)
        {
            Debug.LogError("Missing DialogueTrigger component on " + gameObject.name);
        }
    }

    //Interact runs when the player approaches an NPC and makes a conscious choice to interact with them by pressing the Use button.
    public override void Interact()
    {
        if (npcData != null)
        {
            if (DT != null)
            {
                DT.TriggerDialogue();
            }
            else
            {
                Debug.LogError("Missing DialogueTrigger component on " + gameObject.name);
            }
        }
        else
        {
            Debug.LogError("Error! NPC has no NPCData!");
        }
    }
}
