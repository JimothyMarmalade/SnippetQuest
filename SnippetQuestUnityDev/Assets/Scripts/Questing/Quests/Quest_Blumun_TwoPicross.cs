using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Blumun_TwoPicross : Quest
{
    private void Start()
    {
        QuestName = "Blumun's Amount Challenge";
        Description = "Demonstrate your twice awesome puzzling skills!";
        SnippetReward = new List<string>();
        SnippetReward.Add("Futoshiki_3");
        SnippetReward.Add("Picross_Leaf");

        givePlayerQuestDialogue.speakerName = "Blumun";
        givePlayerQuestDialogue.sentences = new string[]{
            "Ready for something else, huh? Well then, alright...",
            "How about you go ahead and complete that picross puzzle again, twice over? Seems easy enough.",
            "Especially considering you already know the solution."
        };
        givePlayerQuestDialogue.eyesExpression = "none";
        givePlayerQuestDialogue.mouthExpression = "MouthSmile";
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.sentences = new string[] 
        {"You haven't completed the quest yet! Keep solving picross puzzles.",
        "It's fine if you complete puzzles you've already solved, too."
        };
        inProgressDialogue.eyesExpression = "isAngry";
        inProgressDialogue.mouthExpression = "MouthOpen";
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.sentences = new string[]
        {
            "Hey, look at that! You did it twice! Great job!",
            "Here's another Picross puzzle I found, and one of those Futo-whatzits. You really seem to know what you're doing!"
        };
        rewardDialogue.eyesExpression = "IsHappy";
        rewardDialogue.mouthExpression = "MouthSmile";
        //-------------------------------------------------------------------
        completedDialogue.speakerName = "Blumun";
        completedDialogue.sentences = new string[]
        {
            "Yep, I'm still here. Hope no one sneaks up behind me."
        };
        completedDialogue.eyesExpression = "none";
        completedDialogue.mouthExpression = "none";
        //-------------------------------------------------------------------
        Goals.Add(new AmountSolvedGoal(this, "Picross", "Solve the Picross puzzle Blumun gave you twice", false, 0, 2));

        Goals.ForEach(g => g.Init());
    }
}
