/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * Acts as the player's journal and stores information about all quests the player has taken on, completed or in-progress.
 * Also stores notes and other important information.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public static QuestLog Instance { get; set; }
    
    private List<Quest> questList = new List<Quest>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null && Instance != this)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(questList[0].Description);
        }
    }

    public void AddQuestToLog(Quest q)
    {
        questList.Add(q);
        q.SetQuestActive();
    }

    public void RemoveQuestFromLog(Quest q)
    {
        foreach (Quest quest in questList)
        {
            if (quest.name == q.name)
            {
                Debug.Log("Found activeQuest, removing from log");
                questList.Remove(quest);
                return;
            }
        }
        Debug.LogError("Could not find activeQuest \"" + q.name + "\" in QuestLog");
    }



}
