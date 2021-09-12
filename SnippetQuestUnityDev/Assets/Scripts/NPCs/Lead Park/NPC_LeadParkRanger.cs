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
            ActivateDialog("LPGreeterGreeting01");
        }
        else
        {
            if (!QuestLog.Instance.CheckIfQuestAccepted("Q003"))
            {
                //Offer the Serene Places Searching quest
                ActivateDialog("LPGreeterConfidentAsk");
            }
            else
            {
                if (QuestLog.Instance.CheckIfQuestAccepted("Q003") && QuestLog.Instance.GetQuestState("Q003") != Quest.QuestState.Completed)
                {
                    //Check Quest Completion Status
                    if (QuestLog.Instance.GetQuestState("Q003") != Quest.QuestState.ReadyForTurnIn)
                    {
                        ActivateDialog("LPGreeterKeepLooking");
                    }
                    else if (QuestLog.Instance.GetQuestState("Q003") == Quest.QuestState.ReadyForTurnIn)
                    {
                        ActivateDialog("LPGreeterQ003Completion01");
                        QuestLog.Instance.CompleteQuestAndGiveRewards("Q003");
                    }
                }

                else if (QuestLog.Instance.GetQuestState("Q003") == Quest.QuestState.Completed)
                    ActivateDialog("LPGreeterCongratulations01");
            }
        }

        MetPlayer = true;
    }
}
