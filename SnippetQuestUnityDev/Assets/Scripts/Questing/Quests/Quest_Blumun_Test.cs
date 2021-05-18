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

        givePlayerQuestDialogue.speakerName = "Blumun";
        givePlayerQuestDialogue.sentences = new string[]{
            "Alright, it's time for your first quest!",
            "Why don't you interact with that bench over there and try to solve that Picross puzzle I gave you?",
            "I just labeled it as \"Picross 1.\" Take a seat and give 'er a try."
        };
        givePlayerQuestDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        givePlayerQuestDialogue.mouthExpression = ExpressionController.MouthExpression.MessageBox;
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.sentences = new string[] 
        {"Still working on it, huh? Don't worry. What's important is that you take your time and " +
            "figure it out on your own. That's where the real reward is."
        };
        inProgressDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        inProgressDialogue.mouthExpression = ExpressionController.MouthExpression.Curious;
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.sentences = new string[]
        {
            "Hey, look at that! You did it! Great job!",
            "You should probably take this. You're looking for more of these little puzzles, right? I don't have much use for it.",
            "Come back to me when you feel you need another challenge!"
        };
        rewardDialogue.eyesExpression = ExpressionController.EyesExpression.Surprised;
        rewardDialogue.mouthExpression = ExpressionController.MouthExpression.Surprised;
        //-------------------------------------------------------------------

        
    }

    public override void ActivateQuest()
    {
        Goals.Add(new SnippetSolvedGoal(this, "Picross_TestHeart", "Complete Picross 1", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }
}
