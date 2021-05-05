/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * The data container for quest info and their reward for completion
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public List<QuestGoal> Goals { get; set; } = new List<QuestGoal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public List<Snippet> SnippetReward { get; set; }
    public bool IsActive { get; set; }
    public bool IsCompleted { get; set; }

    public Dialogue inProgressDialogue = new Dialogue();
    public Dialogue rewardDialogue = new Dialogue();
    public Dialogue completedDialogue = new Dialogue();

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
            foreach (Snippet s in SnippetReward)
            {
                InventoryController.Instance.GiveSnippet(s.snippetSlug);
                Debug.Log("Added Snippet with slug " + s.snippetSlug + "to player Inventory");
            }
        }
        else
        {
            Debug.LogError("SnippetReward for this activeQuest is null!");
        }
    }

    public void SetQuestActive()
    {
        IsActive = true;
    }

    public void SetQuestCompleted()
    {
        IsActive = false;
        IsCompleted = true;
    }

}
