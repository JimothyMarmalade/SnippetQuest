/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 4/22/21
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

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void TriggerDialogue(Dialogue d)
    {
        DialogueManager.Instance.StartDialogue(d);
    }
}
