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
    public Snippet SnippetReward { get; set; }
    public bool IsActive { get; set; }
    public bool IsCompleted { get; set; }

    public void CheckGoals()
    {
        //Checks to see if all goals are completed
        IsCompleted = Goals.All(g => g.Completed);

        if (IsCompleted)
            GiveReward();
    }

    void GiveReward()
    {
        if (SnippetReward != null)
        {
            //Give the snippet to the player by inserting it in the inventory

        }
    }

}
