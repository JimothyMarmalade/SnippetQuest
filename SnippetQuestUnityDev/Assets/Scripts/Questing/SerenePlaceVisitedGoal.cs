/*
 * Created by Logan Edmund, 9/12/21
 * Last Modified by Logan Edmund, 9/12/21
 * 
 * A quest goal for quests -- tracks when/what Serene Places the player visits to solve snippets.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SerenePlaceVisitedGoal", menuName = "Quests/New SerenePlaceVisitedGoal")]
public class SerenePlaceVisitedGoal : QuestGoal
{
    [Header("Serene Place ID")]
    public int SPID = -1;

    public SerenePlaceVisitedGoal(Quest quest, int SPID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.AssignedQuest = quest;
        this.SPID = SPID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init(Quest q)
    {
        base.Init(q);
        SnippetEvents.OnSPAccessed += SPVisited;
    }

    //The method called/accessed in SnippetEvents whenever a puzzle has been completed
    void SPVisited(int SPID)
    {
        if (SPID == this.SPID)
        {
            this.CurrentAmount++;
            if (Evaluate())
                SnippetEvents.OnSPAccessed -= SPVisited;
        }
    }

}
