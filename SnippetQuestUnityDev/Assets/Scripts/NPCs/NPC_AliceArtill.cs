using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AliceArtill : NPC
{
    


    public override void Interact()
    {
        Debug.Log("Running Interact for NPC_AliceArtill...");
        AliceDialogTree();
        MetPlayer = true;
    }

    private void AliceDialogTree()
    {
        ActivateDialog("LP_Alice_FirstMeeting_Q001Unaccepted_001");

    }
}
