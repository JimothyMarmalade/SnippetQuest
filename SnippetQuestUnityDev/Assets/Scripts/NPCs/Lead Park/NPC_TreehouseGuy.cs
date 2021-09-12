using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_TreehouseGuy : NPC
{
    public override void Interact()
    {
        Debug.Log("Running Interact for NPC_TreehouseGuy...");
        TreehouseGuyDialogTree();
        MetPlayer = true;
    }

    private void TreehouseGuyDialogTree()
    {
        if (!MetPlayer)
        {
            ActivateDialog("LPTreehouseGuy001");
        }
        else
        {
            ActivateDialog("LPTreehouseGuy004");
        }
    }
}
