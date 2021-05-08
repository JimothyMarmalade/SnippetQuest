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

        givePlayerQuestDialogue.speakerName = "Blumun";
        givePlayerQuestDialogue.sentences = new string[]{
            "Howdy there, pardner! Mah name's Blumun!",
            "You may notice how I got that name due to my incurable hypothermia. That's a tale, I tell you what.",
            "But hey, you don't wanna hear all that. For now, why don't you interact with that bench over there and try to solve that Picross puzzle I gave you?"
        };
        givePlayerQuestDialogue.eyesExpression = "none";
        givePlayerQuestDialogue.mouthExpression = "MouthMessageBox";
        //-------------------------------------------------------------------
        inProgressDialogue.speakerName = "Blumun";
        inProgressDialogue.sentences = new string[] 
        {"Still working on it, huh? Don't worry. What's important is that you take your time and " +
            "figure it out on your own. That's where the real reward is."
        };
        inProgressDialogue.eyesExpression = "none";
        inProgressDialogue.mouthExpression = "MouthCurious";
        //-------------------------------------------------------------------
        rewardDialogue.speakerName = "Blumun";
        rewardDialogue.sentences = new string[]
        {
            "Hey, look at that! You did it! Great job!",
            "Come back to me when you feel you need another challenge!"
        };
        rewardDialogue.eyesExpression = "IsSurprised";
        rewardDialogue.mouthExpression = "MouthSurprised";
        //-------------------------------------------------------------------

        completedDialogue.speakerName = "Blumun";
        completedDialogue.sentences = new string[]
        {
            "Yep, I'm still here. Hope no one sneaks up behind me."
        };
        //-------------------------------------------------------------------


        Goals.Add(new SnippetSolvedGoal(this, "Picross_TestHeart", "Solve the Picross puzzle Blumun gave you", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }
}
