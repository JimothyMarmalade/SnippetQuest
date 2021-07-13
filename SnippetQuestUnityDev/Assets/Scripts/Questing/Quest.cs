/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 5/14/21
 * 
 * The data container for quest info and their reward for completion
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/New Quest")]
public class Quest : ScriptableObject
{
    //Name of the Quest
    public string QuestName;

    //The Quest's Current Description
    public string Description;

    //All goals needed to be completed for the quest to be ready to turn in
    public List<QuestGoal> Goals = new List<QuestGoal>();

    //The Snippet(s) awarded upon completion
    public List<string> SnippetReward;

    //Tracks the current state of the Quest
    public enum QuestState {Unaccepted, Accepted, ReadyForTurnIn, Completed }
    public QuestState CurrentState = QuestState.Unaccepted;

    private void Start()
    {
        LoadQuestData();
    }

    public virtual void LoadQuestData()
    {
        //Quest data is loaded and set up here
    }

    public virtual void ActivateQuest()
    {
        foreach (QuestGoal goal in Goals)
        {
            goal.Init(this);
        }
    }

    public void CheckGoals()
    {
        //Checks to see if all goals are completed
        if (Goals.All(g => g.Completed))
            CurrentState = QuestState.ReadyForTurnIn;

        if (CurrentState == QuestState.ReadyForTurnIn)
        {
            Debug.Log("All goals for Quest \"" + QuestName + "\" completed.");
        }
    }

    public void GiveRewards()
    {
        Debug.Log("Distributing Quest Rewards...");
        if (SnippetReward != null)
        {
            //Give the snippet to the player by inserting it in the inventory
            foreach (string s in SnippetReward)
            {
                InventoryController.Instance.AddSnippet(s);
                Debug.Log("Added Snippet with slug " + s + "to player Inventory");
            }
        }
        else
        {
            Debug.LogError("Give Rewards ran successfully, but the SnippetReward for this activeQuest is null!");
        }
    }

    public void AcceptQuest()
    {
        Debug.Log("Running SetQuestInProgress() in Quest.cs");
        ActivateQuest();

        CurrentState = QuestState.Accepted;

        Debug.Log("Quest name is: " + QuestName);
        Debug.Log("Goals count is: " + Goals.Count);


    }

    public void SetQuestCompleted()
    {
        CurrentState = QuestState.Completed;
    }


}
