using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Pinkie_SadPinkie : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Pinkie's Dilemma";
        Description = "Find a way to cheer up Pinkie.";
        SnippetReward = null;

        givePlayerQuestDialogue.speakerName = "Pinkie";
        givePlayerQuestDialogue.sentences = new string[]{
            "You really want to lend a hand, huh? Well, I guess I can't stop you...",
            "I wish I could see a nice, smiling face. Something that would tell me things are alright in the world."
        };
        givePlayerQuestDialogue.eyesExpression = ExpressionController.EyesExpression.Sad;
        givePlayerQuestDialogue.mouthExpression = ExpressionController.MouthExpression.Sad;
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Pinkie";
        inProgressDialogue.sentences = new string[]
        {
            "No luck so far, huh?",
            "It's okay. Maybe I'll just be sad forever..."
        };
        inProgressDialogue.eyesExpression = ExpressionController.EyesExpression.Sad;
        inProgressDialogue.mouthExpression = ExpressionController.MouthExpression.Sad;
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Pinkie";
        rewardDialogue.sentences = new string[]
        {
            "Wha...what? Why, look at that! It's a cute little face!",
            "Thanks, sweetie. I think I'm going to be okay for now."
        };
        rewardDialogue.eyesExpression = ExpressionController.EyesExpression.Happy;
        rewardDialogue.mouthExpression = ExpressionController.MouthExpression.Happy;
        //-------------------------------------------------------------------


        Goals.Add(new SnippetSolvedGoal(this, "Picross_SmileyFace", "Find a way to cheer up Pinkie", false, 0, 1));

    }

    public override void ActivateQuest()
    {
        Goals.ForEach(g => g.Init());
    }
}
