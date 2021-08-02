using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_LPRiverGuy : NPC
{
    public override void Interact()
    {
        Debug.Log("Running Interact for NPC_LPRiverGuy...");
        RiverGuyDialogTree();
        MetPlayer = true;
    }


    private void RiverGuyDialogTree()
    {
        ActivateDialog("LP_RiverWatcher_Interaction_001");
    }
}
