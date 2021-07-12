/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 7/10/21
 * 
 * Extends from the Interactable class and holds all methods and data required for NPCs.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class NPC : Interactable
{
    [Header("Base NPC Attributes + References")]
    public string NPCName;
    public bool MetPlayer;

    public ExpressionController NPCFace;

    [Header("Starting Dialog Lines")]
    public List<Dialog> ConversationStarters = new List<Dialog>();

    public DialogTrigger DT { get; set; }

    private void Awake()
    {
        DT = gameObject.GetComponent<DialogTrigger>();
        if (DT == null)
        {
            Debug.LogError("Missing DialogueTrigger component on " + gameObject.name);
        }
        else
        {
            DT.SetFaceReference(NPCFace);
        }
    }

    //Interact runs when the player approaches an NPC and makes a conscious choice to interact with them by pressing the Use button.
    public override void Interact()
    {
        Debug.LogWarning("This is the Default NPC Interact message. If you're reading this, it means the NPC has no scripted interactions.");

        if (DT != null)
        {
            DT.TriggerDialog();
        }
        else
        {
            Debug.LogError("Missing DialogueTrigger component on " + gameObject.name);
        }
    }

    public void ActivateDialog(string dialogPath)
    {
        DT.TriggerDialog(ConversationStarters.Find(item => item.dialogIdentifier == dialogPath), NPCFace);
    }

    #region Conversion in progress

    /*
    public void AssignNextQuest()
    {
        Debug.Log("Branch 2.1");

        //Assign the next quest if there is another quest to assign.
        if (AvailableQuests.Count != 0)
        {
            //Get the next quest from the list, set it as the NPC's current quest, and remove it from the available backlog
            currentQuest = AvailableQuests[0];
            AvailableQuests.RemoveAt(0);
            //Load quest data and Turn on the quest listeners
            currentQuest.SetQuestInProgress();
            //Add quest to Questlog
            QuestLog.Instance.AddQuestToIPQ(currentQuest);
            //Set NPC as having a quest in progress and load the quest's dialogue
            npcData.HasQuestInProgress = true;
            DT.TriggerDialogue(currentQuest.givePlayerQuestDialogue);
        }
        //If the list is empty, there's no currentQuest, and this was somehow triggered by accident, compensate.
        else if (currentQuest == null)
        {
            npcData.AllQuestsCompleted = true;
            DT.TriggerDialogue(AllQuestsCompleteDialogue);
        }
    }
    */

    /*
    void CheckCurrentQuestCompletion()
    {
        Debug.Log("Branch 2.2");

        if (currentQuest.IsCompleted)
        {
            Debug.Log("Branch 2.2.1");

            DT.TriggerDialogue(currentQuest.rewardDialogue);

            currentQuest.GiveRewards();

            QuestLog.Instance.ArchiveQuest(currentQuest);

            currentQuest = null;

            npcData.HasQuestInProgress = false;

            if (AvailableQuests.Count == 0)
                npcData.AllQuestsCompleted = true;

        }
        else
        {
            Debug.Log("Branch 2.2.2");

            DT.TriggerDialogue(currentQuest.inProgressDialogue);
        }
    }
    */

    #endregion




}
