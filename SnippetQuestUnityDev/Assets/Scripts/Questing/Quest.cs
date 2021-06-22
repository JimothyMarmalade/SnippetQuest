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

[System.Serializable]
public class Quest : MonoBehaviour
{

    public string QuestName;

    public string Description;

    public List<QuestGoal> Goals = new List<QuestGoal>();

    public List<string> SnippetReward;

    public bool IsInProgress;

    public bool IsCompleted;

    public Dialogue givePlayerQuestDialogue = new Dialogue();

    public Dialogue inProgressDialogue = new Dialogue();

    public Dialogue rewardDialogue = new Dialogue();


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
        //Turn on quest goals and listeners here
    }

    public void CheckGoals()
    {
        //Checks to see if all goals are completed
        IsCompleted = Goals.All(g => g.Completed);

        if (IsCompleted)
        {
            Debug.Log("Quest \"" + QuestName + "\" completed.");
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

    public void SetQuestInProgress()
    {
        Debug.Log("Running SetQuestInProgress() in Quest.cs");
        ActivateQuest();

        IsInProgress = true;

        Debug.Log("Quest name is: " + QuestName);
        Debug.Log("Goals count is: " + Goals.Count);


    }

    public void SetQuestCompleted()
    {
        IsInProgress = false;
        IsCompleted = true;
    }


}
