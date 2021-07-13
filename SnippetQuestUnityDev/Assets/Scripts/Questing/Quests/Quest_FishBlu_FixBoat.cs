using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_FishBlu_FixBoat : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Whatever Floats Your Boat";
        Description = "";
        SnippetReward = new List<string>();

        Goals.Add(new SnippetSolvedGoal(this, "Futoshiki_3", "Help Blumun solve the Futoshiki puzzle to fix his boat", false, 0, 1));

    }

    public override void ActivateQuest()
    {
        Goals.ForEach(g => g.Init(this));
    }
}
