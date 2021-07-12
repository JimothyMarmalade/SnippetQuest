/*
 * Created by Logan Edmund, 7/11/21
 * Last Modified by Logan Edmund, 7/11/21
 * 
 * Holds dialog data for the DialogTrigger monobehavior including a casual identifier, speaker name, and spoken text. Can also hold Quest information
 * that can be given to the player based on dialogue choices.
 * 
 * DialogChoice allows for the player to make a choice that determines where the conversation will go.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New DialogChoice", menuName = "Dialog/New Empty DialogChoice")]

public class DialogChoice : Dialog
{

    [Header("Multiple Choice Dialog Paths")]
    public string PlayerReaction1;
    public Dialog DialogChoice1;
    public string PlayerReaction2;
    public Dialog DialogChoice2;
}
