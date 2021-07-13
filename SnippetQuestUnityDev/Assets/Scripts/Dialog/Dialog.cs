/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 7/13/21
 * 
 * Holds base dialog data for the DialogTrigger monobehavior including a casual identifier, speaker name, and spoken text. 
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog: ScriptableObject
{
    //Used as a casual Identifier for dialog triggers for programmers to keep track of what's what
    public string dialogIdentifier = "";

    //Name of the person speaking in this dialog segment
    public string speakerName = "";

    //Name of the eyes expression used when saying this dialogue
    public ExpressionController.EyesExpression eyesExpression;

    //name of the mouth expression used when saying this dialogue
    public ExpressionController.MouthExpression mouthExpression;

    //name of the body animation used when saying this dialogue
    public string bodyAnimation = "";
    
    //The sentences that will be spoken
    [TextArea(3, 10)]
    public string dialogLine;

    public virtual void DisplayDialog()
    {
        //method should be inherited, scripting goes there.
    }


}
