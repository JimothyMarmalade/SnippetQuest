/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 5/27/21
 * 
 * Holds data for NPCs such as ID and name.
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData : MonoBehaviour
{
    public int NPCId;
    public string NPCName;

    public bool isQuestGiver;
    public bool MetPlayer;
    public bool HasQuestInProgress;
    public bool AllQuestsCompleted;

    //Initializes the data by loading information from the save file.
    public void InitNPC()
    {
        Debug.LogWarning("NPCData.InitNPC() ran, but does not do anything.");

    }

    public void InitQuestGiver()
    {
        //Try to get information from save.
        Debug.LogWarning("NPCData.InitQuestGiver() is not fully implemented. Building NPCData as new.");
        //If save file does not exist or cannot be retreived, throw error and initialize the NPC as new.
        NPCId = -10;
        NPCName = "No Name Loaded";
        isQuestGiver = true;
        MetPlayer = false;
        HasQuestInProgress = false;
        AllQuestsCompleted = false;
    }

}
