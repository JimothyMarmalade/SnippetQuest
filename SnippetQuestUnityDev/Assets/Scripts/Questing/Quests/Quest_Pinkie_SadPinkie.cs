using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Pinkie_SadPinkie : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Pinkie's Dilemma";
        Description = "Find a way to cheer up Pinkie.";
        SnippetReward = null;



        Goals.Add(new SnippetSolvedGoal(this, "Picross_SmileyFace", "Find a way to cheer up Pinkie", false, 0, 1));

    }

    public override void ActivateQuest()
    {
        Goals.ForEach(g => g.Init(this));
    }
}
