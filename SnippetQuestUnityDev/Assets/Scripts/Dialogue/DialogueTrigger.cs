/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 5/12/21
 * 
 * A DialogueTrigger is simply a container that stores a Dialogue pack and sends it to the DialogueManager when necessary to display it onscreen.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Default dialogue
    public Dialogue dialogue;
    public ExpressionController faceReference;

    public void TriggerDialogue()
    {
        //Add methods to OnDialogueOver to restore control after conversation
        DialogueManager.OnDialogueOver += DialogueOver;

        //Disable player movement
        PlayerController.Instance.DisableAllMovement();

        //Begin Dialogue
        DialogueManager.Instance.StartDialogue(dialogue);

        //Begin facial animations
        faceReference.ChangeExpression(dialogue.eyesExpression, dialogue.mouthExpression);

        //Begin music changes
        AudioManager.Instance.BGMFocusActivity(1.5f);
    }

    public void TriggerDialogue(Dialogue d)
    {
        //Add methods to OnDialogueOver to restore control after conversation
        DialogueManager.OnDialogueOver += DialogueOver;

        //Disable player movement
        PlayerController.Instance.DisableAllMovement();

        //Begin Dialogue
        DialogueManager.Instance.StartDialogue(d);

        //Begin facial animations
        faceReference.ChangeExpression(d.eyesExpression, d.mouthExpression);

        //Begin music changes
        AudioManager.Instance.BGMFocusActivity(1.5f);
    }
    public void DialogueOver()
    {
        //Restore Player Movement
        PlayerController.Instance.EnableAllMovement();

        //Reset the facial expression
        faceReference.ClearExpression();

        //Change music back to exploration
        AudioManager.Instance.BGMFocusExploration(1.5f);

        //Remove these methods from OnDialogueOver
        DialogueManager.OnDialogueOver -= DialogueOver;
    }
}
