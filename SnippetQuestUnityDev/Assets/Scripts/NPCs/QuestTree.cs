/*
 * Created by Logan Edmund, 4/27/21
 * Last Modified by Logan Edmund, 4/27/21
 * 
 * QuestTree.cs holds information about All possible quests that an NPC can hold for the player. This class is meant to be inherited by individual
 * scripts that hold information on a per-NPC basis. Holds all methods necessary to retreive current quest progression/dialogue.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTree : MonoBehaviour
{
    public List<string> questTypes = new List<string>();
    public Queue<Quest> QuestList = new Queue<Quest>();
    public List<Quest> CompletedQuestList = new List<Quest>();

    public int questPointer = -1;
    public string ActiveQuestType;

    public Dialog FirstEncounterDialogue = new Dialog();
    public Dialog AllQuestsCompleteDialogue = new Dialog();


    //Method for shifting the reference for the active quest to the next quest in the list
    public void TrySetNextQuestActive()
    {
        if (CheckNewQuestExists())
        {
            questPointer++;
            ActiveQuestType = questTypes[questPointer];
        }
    }

    public string GetActiveQuest()
    {
        if (ActiveQuestType != null)
            return ActiveQuestType;
        else if (CheckNewQuestExists())
        {
            TrySetNextQuestActive();
            return ActiveQuestType;
        }

        Debug.LogError("Could not retreive an active quest from " + name);
        return null;
    }
    public bool CheckNewQuestExists()
    {
        if (questPointer >= questTypes.Count)
            return false;
        else return true;
    }

    public void LoadQuestData()
    {
        Debug.Log("Loading Quest data for " + name);
        foreach (string s in questTypes)
        {
            QuestList.Enqueue((Quest)gameObject.AddComponent(System.Type.GetType(s)));        
        }
    }
}
