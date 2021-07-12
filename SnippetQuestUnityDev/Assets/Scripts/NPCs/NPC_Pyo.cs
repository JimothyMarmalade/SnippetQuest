/*
 * Created by Logan Edmund, 7/10/21
 * Last Modified by Logan Edmund, 7/10/21
 * 
 * 
 * Dialog/NPC Behaviors for Pyo in Lead Park
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Pyo : NPC
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Running Interact for NPC_Pyo...");

        if (!MetPlayer)
        {
            Debug.Log("This is the first time meeting the Player.");
            MetPlayer = true;
        }
        else if (MetPlayer)
        {
            Debug.Log("This is not the first time Pyo has met the player.");
        }
    }

}
