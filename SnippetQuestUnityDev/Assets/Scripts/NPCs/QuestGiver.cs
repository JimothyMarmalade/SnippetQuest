/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/27/21
 * 
 * Overrides NPC.cs. To be used when an NPC has a quest or string of quests that can be undertaken by the player.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool CompletedQuest { get; set; }

    public bool MetPlayer { get; set; }

    [SerializeField]
    private GameObject questLogReference;
    [SerializeField]
    private string questType;
    [SerializeField]
    private string questTreeType;

    public Quest activeQuest;
    public QuestTree questTree;

    private void Awake()
    {
        if (npcData == null)
        {
            Debug.LogError("Missing NPCData for " + gameObject.name);
        }

        DT = gameObject.GetComponent<DialogueTrigger>();
        if (DT == null)
        {
            Debug.LogError("Missing DialogueTrigger component on " + gameObject.name);
        }

        this.questTree = (QuestTree)gameObject.AddComponent(System.Type.GetType(questTreeType));
        if (questTree = null)
            Debug.LogError("There is no attached QuestTree on NPC " + npcData.characterName);


    }

    public override void Interact()
    {
        //The Idea is that Interact will do the current actions, but be making calls to QuestTree in order to retreive the relevant information.
        if (questTree == null)
            questTree = gameObject.GetComponent<QuestTree>();

        if (!AssignedQuest && !CompletedQuest)
        {
            //Assign the First Quest
            DT.TriggerDialogue(questTree.FirstEncounterDialogue);
            AssignQuest(); 
        }
        else if (AssignedQuest && !CompletedQuest)
        {
            //Display the In-Progress Dialogue
            Debug.Log("Checking Quest status...");
            CheckQuest();
        }
        else
        {
            //If there is another quest to undertake, begin it and start over.
            if (questTree.CheckNewQuestExists())
            {
                AssignQuest();
                Debug.LogError("Quest Successfully assigned, but quest-giving dialogue could not be loaded.");
                Debug.LogError("Implement Dialogue existing for the assignment of quests past the first in a QuestTree. " +
                    "Current Dialogue problems are caused by a loading error -- WILL NEED to overhaul Quest system down the line.");
            }
            //If not, then display the end-of-questTree dialogue.
            else
            {
                DT.TriggerDialogue(questTree.AllQuestsCompleteDialogue);
            }
        }
    }

    public void AssignQuest()
    {
        //Get the next quest to undertake from the QuestTree
        AssignedQuest = true;
        CompletedQuest = false; 
        activeQuest = (Quest)questLogReference.AddComponent(System.Type.GetType(questTree.GetActiveQuest()));
        QuestLog.Instance.AddQuestToLog(activeQuest);
    }

    void CheckQuest()
    {
        if (activeQuest.IsCompleted)
        {
            activeQuest.GiveRewards();
            CompletedQuest = true;
            AssignedQuest = false;
            DT.TriggerDialogue(activeQuest.rewardDialogue);
            Destroy(questLogReference.GetComponent(System.Type.GetType(activeQuest.name)));
            questTree.TrySetNextQuestActive();
        }
        else
        {
            DT.TriggerDialogue(activeQuest.inProgressDialogue);
        }
    }
}
