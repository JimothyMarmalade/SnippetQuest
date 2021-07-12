/*
 * Created by Logan Edmund, 7/11/21
 * Last Modified by Logan Edmund, 7/11/21
 * 
 * 
 * Dialog/NPC Behaviors for the Park Ranger in Lead Park
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_LeadParkRanger : NPC
{
    public override void Interact()
    {
        Debug.Log("Running Interact for Lead Park Ranger...");

        if (!MetPlayer)
        {
            //Say the opening Welcome to SnippetQuest Text
            ActivateDialog("LP_LeadParkGreeter_Interaction_001-1");
        }
        else if (MetPlayer)
        {
            //Tell the player how many snippets are left to find in the stage.
            ActivateDialog("LP_LeadParkGreeter_Interaction_002-1");
        }

        MetPlayer = true;
    }
}
