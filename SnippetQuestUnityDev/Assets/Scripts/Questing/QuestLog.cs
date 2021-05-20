/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 5/16/21
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

    public Quest activeQuest;
    public List<Quest> inProgressQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();



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

        DontDestroyOnLoad(this);
    }

    //Determines if the UI needs to be updated after a goal is completed.
    public void CheckUpdateAQID(QuestGoal q)
    {
        if (q.AssignedQuest == activeQuest)
        {
            UIController.Instance.UpdateActiveQuestInfo(activeQuest);
        }
    }


    //Copies the quest component into the questmanager and deletes the original instance
    public void AddQuestToManager(Quest q)
    {

    }

    public void SetActiveQuest(Quest q)
    {
        activeQuest = q;
        UIController.Instance.UpdateActiveQuestInfo(q);
    }

    public void ArchiveQuest(Quest q)
    {
        RemoveQuestFromIPQ(q);

        if (activeQuest == q)
            activeQuest = null;

        AddQuestToCompleted(q);

        if (inProgressQuests.Count != 0)
            SetActiveQuest(inProgressQuests[0]);
        else
            UIController.Instance.ClearActiveQuestInfo();

    }

    public void AddQuestToIPQ(Quest q)
    {
        inProgressQuests.Add(q);
        if (activeQuest == null)
            SetActiveQuest(q);
    }

    public void RemoveQuestFromIPQ(Quest q)
    {
        foreach (Quest quest in inProgressQuests)
        {
            if (quest == q)
            {
                Debug.Log("Found Quest, removing from log");
                inProgressQuests.Remove(quest);
                inProgressQuests.RemoveAll(item => item == null);
                return;
            }
        }
        Debug.LogError("Could not find Quest \"" + q.name + "\" in InProgressQuests");
    }

    public void AddQuestToCompleted(Quest q)
    {
        completedQuests.Add(q);
    }

    public void RemoveQuestFromCompleted(Quest q)
    {
        foreach (Quest quest in completedQuests)
        {
            if (quest == q)
            {
                Debug.Log("Found Quest in completedQuests, removing from list");
                completedQuests.Remove(quest);
                completedQuests.RemoveAll(item => item == null);
                return;
            }
        }
        Debug.LogError("Could not find Quest \"" + q.name + "\" in completedQuests");
    }

}
