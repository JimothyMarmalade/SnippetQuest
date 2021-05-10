/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 5/10/21
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
        DialogueManager.Instance.StartDialogue(dialogue);
        faceReference.ChangeExpression(dialogue.eyesExpression, dialogue.mouthExpression, dialogue.dialogIdentifier);
    }

    public void TriggerDialogue(Dialogue d)
    {
        DialogueManager.OnDialogueOver += DialogueOver;
        DialogueManager.OnDialogueOver += PlayerController.Instance.EnableAllMovement;


        PlayerController.Instance.DisableAllMovement();
        DialogueManager.Instance.StartDialogue(d);
        faceReference.ChangeExpression(d.eyesExpression, d.mouthExpression, d.dialogIdentifier);
    }
    public void DialogueOver()
    {
        faceReference.ClearExpression();

        DialogueManager.OnDialogueOver -= PlayerController.Instance.EnableAllMovement;
        DialogueManager.OnDialogueOver -= DialogueOver;
    }
}
