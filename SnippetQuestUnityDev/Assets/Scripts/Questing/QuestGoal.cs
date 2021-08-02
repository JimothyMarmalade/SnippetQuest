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

public class QuestGoal : ScriptableObject
{
    [Header("QuestGoal Values")]
    public Quest AssignedQuest;
    public string Description;

    [Header("Completion Status")]
    public bool Completed;
    public int CurrentAmount;
    public int DEFAULTCurrentAmount;
    public int RequiredAmount;



    public virtual void Init(Quest q)
    {
        //default initialization dealies
        SetQuest(q);
        ResetValues();
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

    public void SetQuest(Quest q)
    {
        AssignedQuest = q;
    }

    //ResetValues ensures questgoals don't have leftover data from previous builds.
    public virtual void ResetValues()
    {
        Completed = false;
        CurrentAmount = DEFAULTCurrentAmount;
    }

}
