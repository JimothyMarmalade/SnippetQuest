/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 7/17/21
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
    public List<Quest> acceptedQuests = new List<Quest>();

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
            foreach(Quest q in acceptedQuests)
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

    //Sets the activeQuest displayed on the HUD to the input quest and updates UI accordingly.
    public void SetActiveQuest(Quest q)
    {

        activeQuest = q;
        UIController.Instance.UpdateActiveQuestInfo(q);
    }

    //Moves a Quest from InProgressQuests to CompletedQuests
    public void CompleteQuestAndGiveRewards(string QuestID)
    {
        Quest q = GetQuestReference(QuestID);

        q.CurrentState = Quest.QuestState.Completed;

        q.GiveRewards();

        if (activeQuest == q)
            activeQuest = null;

        foreach (Quest newAQ in acceptedQuests)
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

    public void AddToAcceptedQuests(Quest q)
    {
        acceptedQuests.Add(q);
        if (activeQuest == null)
            SetActiveQuest(q);
    }


    #region Quest Reference and Information Lookup

    //Given the Quest's ID string, returns a reference to the ScriptableObject currently in the log.
    public Quest GetQuestReference(string QuestID)
    {
        Quest q = acceptedQuests.Find(item => item.QuestID == QuestID);
        if (q != null)
            return q;

        return null;
    }

    public bool CheckIfQuestAccepted(string QuestID)
    {
        if (GetQuestReference(QuestID) == null)
            return false;
        else return true;
    }

    //Returns the current state of the quest
    public Quest.QuestState GetQuestState(string QuestID)
    {
        Quest q = GetQuestReference(QuestID);

        if (q != null)
            return q.CurrentState;

        else return Quest.QuestState.Unaccepted;
    }

    #endregion








}
