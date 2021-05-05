using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Blumun_TwoPicross : Quest
{
    private void Start()
    {
        QuestName = "Blumun's Amount Challenge";
        Description = "Demonstrate your twice awesome puzzling skills!";
        SnippetReward = null;

        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.sentences = new string[] 
        {"Hey, good work. Think you can solve it twice?"
        };

        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.sentences = new string[]
        {
            "Hey, look at that! You did it twice! Great job!",
            "A reward? Sorry, maybe in the next demo."
        };

        completedDialogue.speakerName = "Blumun";
        completedDialogue.sentences = new string[]
        {
            "Yep, I'm still here. Hope no one sneaks up behind me."
        };


        Goals.Add(new AmountSolvedGoal(this, "Picross", "Solve the Picross puzzle Blumun gave you twice", false, 0, 2));

        Goals.ForEach(g => g.Init());
    }
}
