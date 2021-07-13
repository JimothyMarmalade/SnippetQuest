using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Blumun_Test : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Blumun's Challenge";
        Description = "Demonstrate your awesome puzzling skills!";

        SnippetReward = new List<string>();
        SnippetReward.Add("Picross_SmileyFace");

        Goals.Add(new SnippetSolvedGoal(this, "Picross_TestHeart", "Complete Picross 1", false, 0, 1));
    }

    public override void ActivateQuest()
    {
        Goals.ForEach(g => g.Init(this));
    }
}
