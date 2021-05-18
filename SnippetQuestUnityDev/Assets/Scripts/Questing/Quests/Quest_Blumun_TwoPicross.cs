using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Blumun_TwoPicross : Quest
{
    public override void LoadQuestData()
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
        givePlayerQuestDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        givePlayerQuestDialogue.mouthExpression = ExpressionController.MouthExpression.Happy;
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.sentences = new string[] 
        {"You haven't completed the quest yet! Keep solving picross puzzles.",
        "It's fine if you complete puzzles you've already solved, too."
        };
        inProgressDialogue.eyesExpression = ExpressionController.EyesExpression.Angry;
        inProgressDialogue.mouthExpression = ExpressionController.MouthExpression.Open_O;
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.sentences = new string[]
        {
            "Hey, look at that! You did it twice! Great job!",
            "Here's another Picross puzzle I found, and one of those Futo-whatzits. You really seem to know what you're doing!"
        };
        rewardDialogue.eyesExpression = ExpressionController.EyesExpression.Happy;
        rewardDialogue.mouthExpression = ExpressionController.MouthExpression.Happy;
        //-------------------------------------------------------------------
    }

    public override void ActivateQuest()
    {
        Goals.Add(new AmountSolvedGoal(this, "Picross", "Complete two Picross Puzzles", false, 0, 2));

        Goals.ForEach(g => g.Init());
    }
}
