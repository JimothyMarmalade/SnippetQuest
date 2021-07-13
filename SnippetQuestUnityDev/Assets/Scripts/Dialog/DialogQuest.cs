/*
 * Created by Logan Edmund, 7/13/21
 * Last Modified by Logan Edmund, 7/13/21
 * 
 * Holds dialog data for the DialogTrigger monobehavior including a casual identifier, speaker name, and spoken text. Can also hold Quest information
 * that can be given to the player based on dialogue choices.
 * 
 * DialogQuest assigns the player a quest upon being triggered.
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogQuest", menuName = "Dialog/New DialogQuest")]
public class DialogQuest : DialogNormal
{
    [Header("Assigned Quest")]
    public Quest AssignedQuest;

    public override void DisplayDialog()
    {
        //Turn on listeners for the quest
        AssignedQuest.ActivateQuest();
        QuestLog.Instance.AddToPlayerAcceptedQuests(AssignedQuest);
        
        base.DisplayDialog();
    }
}
