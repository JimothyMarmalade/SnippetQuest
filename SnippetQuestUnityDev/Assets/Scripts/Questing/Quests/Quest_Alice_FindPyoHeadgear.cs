using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Alice_FindPyoHeadgear : Quest
{
    public override void LoadQuestData()
    {
        QuestName = "Girl, 7, loses homemade artillery cannon";
        Description = "";
        SnippetReward = new List<string>();

        givePlayerQuestDialogue.speakerName = "Alice";
        givePlayerQuestDialogue.dialogLine = new string[]{
            "You want to help? Oh! Well, in that case...",
            "My sister lost a helmet she made. It has a very powerful artillery cannon strapped to the front of it. She fired it without fastening it first," +
            " and...well, it just flew off.",
            "I'd be really grateful if you could spare some time to help look for it. " +
            "I think it flew off in the direction of the mountain. I have no idea where it is exactly, though.",
        };
        givePlayerQuestDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        givePlayerQuestDialogue.mouthExpression = ExpressionController.MouthExpression.None;
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Alice";
        inProgressDialogue.dialogLine = new string[]
        {
            "...oh, still no luck finding it? That's okay. I think I can keep her calmed down until you track it down.",
            "Like I said, I think it flew off somewhere towards the mountain."
        };
        inProgressDialogue.eyesExpression = ExpressionController.EyesExpression.None;
        inProgressDialogue.mouthExpression = ExpressionController.MouthExpression.None;
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Alice";
        rewardDialogue.dialogLine = new string[]
        {
            "Oh! Oh, you actually found it! That's amazing!",
            "Thank you so much! Here, you can have this scrap of newspaper I found earlier.",
            "I know it's not much of a reward, but there's some kind of puzzle on it. You can try and solve it!"
        };
        rewardDialogue.eyesExpression = ExpressionController.EyesExpression.Happy;
        rewardDialogue.mouthExpression = ExpressionController.MouthExpression.Happy;
        //-------------------------------------------------------------------

        Goals.Add(new ItemCollectedGoal(this, "Pyos_Headgear", "Find Pyo's missing artillery helmet", false, 1));

    }

    public override void ActivateQuest()
    {
        Goals.ForEach(g => g.Init());
    }
}
