using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_FishBlu_FixBoat : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Whatever Floats Your Boat";
        Description = "";
        SnippetReward = new List<string>();

        givePlayerQuestDialogue.speakerName = "Blumun";
        givePlayerQuestDialogue.sentences = new string[]{
            "Oh, good. The threat of work didn't scare you off. You're alright, kiddo. Now, about the job...",
            "My old boat's motor died the other day. I bought a replacement for cheap, but I didn't realize I had to assemble it myself!",
            "The darn thing has all these different-sized caps that have to go on in a very, VERY particular order. The sheet came with instructions on " +
            "how to piece it together, but I spilled paint thinner on the diagram.",
            "If you stare at the diagram long enough, maybe you'll be able to see the solution? I've got it sitting here behind me, you're welcome to look."
        };
        givePlayerQuestDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        givePlayerQuestDialogue.mouthExpression = ExpressionController.MouthExpression.None;
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.sentences = new string[]
        {
            "Still working on it? 'Eaahhhhh, don't stress yourself.",
            "Life's all about the hard problems. Cracking down on them at your own pace until you find the solution that works for you.",
            "Just take your time. Me and the ol' girl will still be here."
        };
        inProgressDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        inProgressDialogue.mouthExpression = ExpressionController.MouthExpression.None;
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.sentences = new string[]
        {
            "You figured out the combination? Well, that's just super! I'll have this old girl's motor fixed in two waves of a salmon's wet flapper.",
            "Hold on just a second..."
        };
        rewardDialogue.eyesExpression = ExpressionController.EyesExpression.Happy;
        rewardDialogue.mouthExpression = ExpressionController.MouthExpression.Happy;
        //-------------------------------------------------------------------

        Goals.Add(new SnippetSolvedGoal(this, "Futoshiki_3", "Help Blumun solve the Futoshiki puzzle to fix his boat", false, 0, 1));

    }

    public override void ActivateQuest()
    {
        Goals.ForEach(g => g.Init());
    }
}
