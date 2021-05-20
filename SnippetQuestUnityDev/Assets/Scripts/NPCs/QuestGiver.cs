/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 5/16/21
 * 
 * Overrides NPC.cs. To be used when an NPC has a quest or string of quests that can be undertaken by the player.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    /*
    public bool AssignedQuest { get; set; }
    public bool CompletedQuest { get; set; }
    */

    public bool MetPlayer { get; set; }
    public bool HasQuestInProgress { get; set; }
    public bool AllQuestsCompleted { get; set; }

    public Quest currentQuest;
    public List<Quest> AvailableQuests = new List<Quest>();

    public Dialogue FirstEncounterDialogue;
    public Dialogue AllQuestsCompleteDialogue;

    /*
    [SerializeField]
    private GameObject questLogReference;
    [SerializeField]
    private string questType;
    [SerializeField]
    private string questTreeType;
    */

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

        MetPlayer = false;

        /*
        this.questTree = (QuestTree)gameObject.AddComponent(System.Type.GetType(questTreeType));
        if (questTree = null)
            Debug.LogError("There is no attached QuestTree on NPC " + npcData.characterName);
        */

    }

    public override void Interact()
    {
        /*
        if (questTree == null)
            questTree = gameObject.GetComponent<QuestTree>();
        */
        //First check to see if the player has interacted with this NPC before. If they have unique dialogue for a first encounter, initiate it.
        if (!MetPlayer)
        {
            Debug.Log("Branch 1");
            MetPlayer = true;
            DT.TriggerDialogue(FirstEncounterDialogue);
        }
        else if (!AllQuestsCompleted)
        {
            Debug.Log("Branch 2");

            if (currentQuest == null)
                AssignNextQuest();
            else
                CheckCurrentQuestCompletion();
        }
        else
        {
            Debug.Log("Branch 3");

            DT.TriggerDialogue(AllQuestsCompleteDialogue);
        }
    }

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
            HasQuestInProgress = true;
            DT.TriggerDialogue(currentQuest.givePlayerQuestDialogue);
        }
        //If the list is empty, there's no currentQuest, and this was somehow triggered by accident, compensate.
        else if (currentQuest == null)
        {
            AllQuestsCompleted = true;
            DT.TriggerDialogue(AllQuestsCompleteDialogue);
        }
    }

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

            HasQuestInProgress = false;

            if (AvailableQuests.Count == 0)
                AllQuestsCompleted = true;

        }
        else
        {
            Debug.Log("Branch 2.2.2");

            DT.TriggerDialogue(currentQuest.inProgressDialogue);
        }
    }
}
