/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/22/21
 * 
 * A quest goal for quests -- tracks if a certain number of a Snippet Type have been solved.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountSolvedGoal : QuestGoal
{
    public Snippet.SnippetType SnippetType { get; set; }

    public AmountSolvedGoal(Quest quest, Snippet.SnippetType snippetType, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.AssignedQuest = quest;
        this.SnippetType = snippetType;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }
    public override void Init()
    {
        base.Init();
        SnippetEvents.OnSnippetSolved += SnippetTypeSolved;
    }

    void SnippetTypeSolved(string snippetSlug)
    {
        if (SnippetDatabase.Instance.GetSnippet(snippetSlug).snippetType == this.SnippetType)
        {
            this.CurrentAmount++;
            if (Evaluate())
                SnippetEvents.OnSnippetSolved -= SnippetTypeSolved;
        }
    }

}
