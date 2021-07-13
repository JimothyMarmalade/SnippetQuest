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

    //ActiveQuest is a reference to the player's current active quest (on the HUD)
    public Quest activeQuest;
    //playerAcceptedQuests holds all quests accepted by the player in whatever state they're in.
    public List<Quest> playerAcceptedQuests = new List<Quest>();
    //public List<Quest> inProgressQuests = new List<Quest>();
    //public List<Quest> completedQuests = new List<Quest>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            //Debug -- Change the current active quest.
            foreach(Quest q in playerAcceptedQuests)
            {
                if (activeQuest != q && q.CurrentState != Quest.QuestState.Completed)
                {
                    SetActiveQuest(q);
                    break;
                }
            }
        }
    }

    //Determines if the UI needs to be updated after a goal is completed.
    public void CheckUpdateAQID(QuestGoal q)
    {
        if (q.AssignedQuest == activeQuest)
        {
            UIController.Instance.UpdateActiveQuestInfo(activeQuest);
        }
    }

    public void SetActiveQuest(Quest q)
    {

        activeQuest = q;
        UIController.Instance.UpdateActiveQuestInfo(q);
    }

    //Moves a Quest from InProgressQuests to CompletedQuests
    public void MarkQuestAsComplete(Quest q)
    {
        q.CurrentState = Quest.QuestState.Completed;
        q.GiveRewards();

        if (activeQuest == q)
            activeQuest = null;



        foreach (Quest newAQ in playerAcceptedQuests)
        {
            if (activeQuest != newAQ && newAQ.CurrentState != Quest.QuestState.Completed)
            {
                SetActiveQuest(newAQ);
                break;
            }
            else
                UIController.Instance.ClearActiveQuestInfo();
        }

    }

    public void MarkQuestAsComplete(string QuestID)
    {
        Quest q = playerAcceptedQuests.Find(item => item.QuestID == QuestID);

        q.CurrentState = Quest.QuestState.Completed;

        q.GiveRewards();

        if (activeQuest == q)
            activeQuest = null;


        foreach (Quest newAQ in playerAcceptedQuests)
        {
            if (activeQuest != newAQ && newAQ.CurrentState != Quest.QuestState.Completed)
            {
                SetActiveQuest(newAQ);
                break;
            }
            else
                UIController.Instance.ClearActiveQuestInfo();
        }

    }

    public void AddToPlayerAcceptedQuests(Quest q)
    {
        playerAcceptedQuests.Add(q);
        if (activeQuest == null)
            SetActiveQuest(q);
    }

    /*public void RemoveFromPlayerAcceptedQuests(Quest q)
    {
        foreach (Quest quest in playerAcceptedQuests)
        {
            if (quest == q)
            {
                Debug.Log("Found Quest, removing from log");
                playerAcceptedQuests.Remove(quest);
                playerAcceptedQuests.RemoveAll(item => item == null);
                return;
            }
        }
        Debug.LogError("Could not find Quest \"" + q.name + "\" in InProgressQuests");
    }
    */

    //Returns true or false depending on wether the player has accepted the input quest.
    public bool CheckIfQuestAccepted(string QuestID)
    {
        if (playerAcceptedQuests.Find(item => item.QuestID == QuestID))
        {
            return true;
        }
        else
            return false;
    }

    //Returns the current state of the quest
    public Quest.QuestState GetQuestState(string QuestID)
    {
        Quest q = playerAcceptedQuests.Find(item => item.QuestID == QuestID);
        if (q == null)
            q = playerAcceptedQuests.Find(item => item.QuestID == QuestID);
        return q.CurrentState;
    }


}
