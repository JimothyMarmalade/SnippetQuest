using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Blumun : NPC
{
    // Start is called before the first frame update
    public override void Interact()
    {
        Debug.Log("Running Interact for NPC_Blumun...");
        BlumunDialogTree();
        MetPlayer = true;
    }

    private void BlumunDialogTree()
    {
        //If blumun hasn't met the player, give his intro dialog that feeds into the quest offer.
        if (!MetPlayer)
        {
            ActivateDialog("LP_Blumun_FirstMeeting_Q002Unaccepted_001");
        }
        //If blumun has met the player... 
        else
        {
            //...but they haven't accepted Q002, then offer the quest.
            if (!QuestLog.Instance.CheckIfQuestAccepted("Q002"))
            {
                ActivateDialog("LP_Blumun_FirstMeeting_Q002Offer_001");
            }

            //...and they've accepted his quest, but haven't finished it.
            else if (QuestLog.Instance.GetQuestState("Q002") != Quest.QuestState.ReadyForTurnIn)
            {
                ActivateDialog("LP_Blumun_Interaction_Q002Accepted_001");
            }

            //...and they've accepted his quest, and are ready to turn it in.
            else if (QuestLog.Instance.GetQuestState("Q002") == Quest.QuestState.ReadyForTurnIn)
            {
                ActivateDialog("LP_Blumun_QuestComplete_Q002Accepted_001");
                QuestLog.Instance.CompleteQuestAndGiveRewards("Q002");
            }

            //...and completed his quest.
            else
            {
                ActivateDialog("LP_Blumun_AllQuestsCompleted_001");
            }
        }
    }
}
