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

public class SnippetSolvedGoal : QuestGoal
{
    public int SnippetID { get; set; }

    public SnippetSolvedGoal(int snippetID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.SnippetID = snippetID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        //CombatEvents.OnEnemyDeath
        //-- Look at GameGrind's tutorial series for guidance on how to handle event handlers
    }

    void SnippetSolved(Snippet snippet)
    {
        if (snippet.masterID == this.SnippetID)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }

}
