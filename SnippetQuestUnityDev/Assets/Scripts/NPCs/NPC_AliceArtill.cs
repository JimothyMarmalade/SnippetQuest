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
        //If this is the first meeting or if the player has not yet accepted the first quest
        if (!MetPlayer || !QuestLog.Instance.CheckIfQuestAccepted("Q001"))
            ActivateDialog("LP_Alice_FirstMeeting_Q001Unaccepted_001");
        //else if the player has accepted the quest
        else if (QuestLog.Instance.CheckIfQuestAccepted("Q001") && QuestLog.Instance.GetQuestState("Q001") != Quest.QuestState.Completed)
        {
            //if the Quest is ready to turn in
            if (QuestLog.Instance.GetQuestState("Q001") == Quest.QuestState.ReadyForTurnIn)
            {
                ActivateDialog("LP_Alice_QuestComplete_Q001Accepted_001");
                QuestLog.Instance.MarkQuestAsComplete("Q001");
            }
            //Else if the Quest isn't ready to turn in
            else
            {
                ActivateDialog("LP_Alice_Interaction_Q001Accepted_001");

            }
        }
        else if (QuestLog.Instance.GetQuestState("Q001") == Quest.QuestState.Completed)
            ActivateDialog("LP_Alice_AllQuestsComplete_001");


    }
}
