/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * Holds dialog data for the DialogTrigger monobehavior including a casual identifier, speaker name, and spoken text. Can also hold Quest information
 * that can be given to the player based on dialogue choices
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //Used as a casual Identifier for dialog triggers for programmers to keep track of what's what
    public string dialogIdentifier = "";

    //Name of the person speaking in this dialog segment
    public string speakerName = "";

    //The sentences that will be spoken
    [TextArea(3, 10)]
    public string[] sentences;
}
