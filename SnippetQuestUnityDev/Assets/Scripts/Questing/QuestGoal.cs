/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 5/20/21
 * 
 * Inherited class for the different kinds of QuestGoals that can exist for different Quests.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public Quest AssignedQuest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {
        //default initialization dealies

    }

    public bool Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
            return true;
        }
        else return false;
    }

    public void Complete()
    {
        Completed = true;
        Debug.Log("Quest goal \"" + Description + "\" completed.");

        AssignedQuest.CheckGoals();

        //Send Notification to UI to update quest objective display
        QuestLog.Instance.CheckUpdateAQID(this);
    }


}
