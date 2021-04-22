using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Blumun_Test : Quest
{
    private void Start()
    {
        QuestName = "Blumun's Challenge";
        Description = "Demonstrate your awesome puzzling skills!";
        SnippetReward = null;

        Goals.Add(new SnippetSolvedGoal(1, "Solve the Picross puzzle Blumun gave you", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }
}
