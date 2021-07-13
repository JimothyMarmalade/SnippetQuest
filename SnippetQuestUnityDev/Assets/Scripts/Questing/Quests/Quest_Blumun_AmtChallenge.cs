using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Blumun_AmtChallenge : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Blumun's Amount Challenge";
        Description = "Demonstrate your twice awesome puzzling skills!";
        SnippetReward = new List<string>();
        SnippetReward.Add("Futoshiki_3");
        SnippetReward.Add("Picross_Leaf");

        Goals.Add(new AmountSolvedGoal(this, Snippet.SnippetType.Picross, "Complete two Picross Puzzles", false, 0, 2));
        Goals.Add(new AmountSolvedGoal(this, Snippet.SnippetType.Futoshiki, "Complete one Futoshiki Puzzle", false, 0, 1));
    }

    public override void ActivateQuest()
    {

        Goals.ForEach(g => g.Init(this));
    }
}
