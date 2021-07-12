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

        givePlayerQuestDialogue.speakerName = "Blumun";
        givePlayerQuestDialogue.dialogLine = new string[]{
            "Ready for something else, huh? Well then, alright...",
            "How about you go ahead and complete two Picross puzzles and a Futoshiki puzzle?",
            "That should give you a healthy challenge."
        };
        givePlayerQuestDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        givePlayerQuestDialogue.mouthExpression = ExpressionController.MouthExpression.Happy;
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.dialogLine = new string[] 
        {"You haven't completed the quest yet! Keep solving.",
        "It's fine if you complete puzzles you've already solved, too."
        };
        inProgressDialogue.eyesExpression = ExpressionController.EyesExpression.Angry;
        inProgressDialogue.mouthExpression = ExpressionController.MouthExpression.Open_O;
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.dialogLine = new string[]
        {
            "Hey, look at that! You did all my challenges! Great job!",
            "Here's another Picross puzzle I found, and one of those Futo-whatzits. You really seem to know what you're doing!"
        };
        rewardDialogue.eyesExpression = ExpressionController.EyesExpression.Happy;
        rewardDialogue.mouthExpression = ExpressionController.MouthExpression.Happy;
        //-------------------------------------------------------------------

        Goals.Add(new AmountSolvedGoal(this, Snippet.SnippetType.Picross, "Complete two Picross Puzzles", false, 0, 2));
        Goals.Add(new AmountSolvedGoal(this, Snippet.SnippetType.Futoshiki, "Complete one Futoshiki Puzzle", false, 0, 1));
    }

    public override void ActivateQuest()
    {

        Goals.ForEach(g => g.Init());
    }
}
