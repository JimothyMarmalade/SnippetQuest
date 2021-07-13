using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Alice_FindPyoHeadgear : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Girl, 7, loses homemade artillery cannon";
        Description = "";
        SnippetReward = new List<string>();

        Goals.Add(new ItemCollectedGoal(this, "Pyos_Headgear", "Find Pyo's missing artillery helmet", false, 1));

    }

    public override void ActivateQuest()
    {
        Goals.ForEach(g => g.Init(this));
    }
}
