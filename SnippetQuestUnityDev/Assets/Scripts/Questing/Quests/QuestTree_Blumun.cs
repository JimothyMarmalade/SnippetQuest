/*
 * Created by Logan Edmund, 4/27/21
 * Last Modified by Logan Edmund, 4/27/21
 * 
 * Blumun's QuestTree
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTree_Blumun : QuestTree
{
    public string Quest1 = "Quest_Blumun_Test";
    public string Quest2 = "Quest_Blumun_TwoPicross";
    private void Start()
    {
        
        FirstEncounterDialogue.speakerName = "Blumun";
        FirstEncounterDialogue.sentences = new string[]{
            "Howdy there, pardner! Mah name's Blumun!",
            "You may notice how I got that name due to my incurable hypothermia. That's a tale, I tell you what.",
            "But hey, you don't wanna hear all that. For now, why don't you interact with that bench over there and try to solve that Picross puzzle I gave you?",
            "I just labeled it as \"Picross 1.\" Take a seat and give 'er a try."
        };
        FirstEncounterDialogue.eyesExpression = "none";
        FirstEncounterDialogue.mouthExpression = "MouthMessageBox";
        
        //-------------------------------------------------------------------
        AllQuestsCompleteDialogue.speakerName = "Blumun";
        AllQuestsCompleteDialogue.sentences = new string[]
        {
            "Congrats again on figuring those puzzles out. You're pretty good at this!"
        };
        AllQuestsCompleteDialogue.eyesExpression = "isHappy";
        AllQuestsCompleteDialogue.mouthExpression = "MouthSmile";


        questTypes.Add(Quest1);
        questTypes.Add(Quest2);
    }
}
