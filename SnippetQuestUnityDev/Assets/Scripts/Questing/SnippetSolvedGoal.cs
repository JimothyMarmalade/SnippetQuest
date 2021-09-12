/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * A quest goal for quests -- tracks if a SPECIFIC SNIPPET has been solved/completed
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SnippetSolvedGoal", menuName = "Quests/New SnippetSolvedGoal")]
public class SnippetSolvedGoal : QuestGoal
{
    [Header("Snippet Solve Required")]
    public string SnippetSlug;

    public SnippetSolvedGoal(Quest quest, string snippetSlug, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.AssignedQuest = quest;
        this.SnippetSlug = snippetSlug;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }
    public override void Init(Quest q)
    {
        base.Init(q);
        SnippetEvents.OnSnippetSolved += SnippetSolved;
    }

    //The method called/accessed in SnippetEvents whenever a puzzle has been completed
    void SnippetSolved(string snippetSlug)
    {
        if (snippetSlug == this.SnippetSlug)
        {
            this.CurrentAmount++;
            if (Evaluate())
                SnippetEvents.OnSnippetSolved -= SnippetSolved;
        }
    }

}
