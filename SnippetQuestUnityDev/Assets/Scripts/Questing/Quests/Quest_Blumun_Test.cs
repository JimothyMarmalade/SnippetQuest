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

        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.sentences = new string[] 
        {"Still working on it, huh? Don't worry. What's important is that you take your time and " +
            "figure it out on your own. That's where the real reward is."
        };

        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.sentences = new string[]
        {
            "Hey, look at that! You did it! Great job!",
            "A reward? Sorry, maybe in the next demo."
        };

        completedDialogue.speakerName = "Blumun";
        completedDialogue.sentences = new string[]
        {
            "Yep, I'm still here. Hope no one sneaks up behind me."
        };


        Goals.Add(new SnippetSolvedGoal(this, "Picross_TestHeart", "Solve the Picross puzzle Blumun gave you", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }
}