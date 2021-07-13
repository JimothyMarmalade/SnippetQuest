/*
 * Created by Logan Edmund, 5/21/21
 * Last Modified by Logan Edmund, 5/21/21
 * 
 * UI_ExplorationPanel is an extension of the General UI. It will contain and display everything shown to the player during exploration gameplay
 * including the map, snippet collection popups, Active quest information, and information about collectables.
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ExplorationDisplay : MonoBehaviour
{
    [Header("Exploration Panel")]
    public GameObject thirdPersonPanel;

    [Header("Active Quest Display")]
    public ActiveQuestInfoDisplay AQID;

    [Header("Snippet Obtained Popup")]
    public GameObject SOPPrefab;
    public RectTransform SOPSpawnLocation1;
    public RectTransform SOPSpawnLocation2;
    public RectTransform SOPSpawnLocation3;
    private SnippetObtainedPopup SOP1;
    private SnippetObtainedPopup SOP2;
    private SnippetObtainedPopup SOP3;

    //----------Spawn a new Snippet Obtained Prefab
    public void SpawnSnippetObtainedPopup(string snippetType)
    {
        if (SOP1 == null)
        {
            SOP1 = Instantiate(SOPPrefab, SOPSpawnLocation1).GetComponent<SnippetObtainedPopup>();
            SOP1.Init(snippetType);
        }
        else if (SOP2 == null)
        {
            SOP2 = Instantiate(SOPPrefab, SOPSpawnLocation2).GetComponent<SnippetObtainedPopup>();
            SOP2.Init(snippetType);
        }
        else if (SOP3 == null)
        {
            SOP3 = Instantiate(SOPPrefab, SOPSpawnLocation3).GetComponent<SnippetObtainedPopup>();
            SOP3.Init(snippetType);
        }
        else
        {
            //IF there's no room for a new popup, save the information until a new popup can be displayed
        }
    }

    //----------Update the Active Quest Information
    public void UpdateActiveQuestInfo(Quest q)
    {
        AQID.QuestTitle.text = q.QuestName;
        switch (q.Goals.Count)
        {
            case (1):
                AQID.Objective1.text = q.Goals[0].Description;
                AQID.Objective2.text = "";
                AQID.Objective3.text = "";
                break;
            case (2):
                AQID.Objective1.text = q.Goals[0].Description;
                AQID.Objective2.text = q.Goals[1].Description;
                AQID.Objective3.text = "";
                break;
            case (3):
                AQID.Objective1.text = q.Goals[0].Description;
                AQID.Objective2.text = q.Goals[1].Description;
                AQID.Objective3.text = q.Goals[2].Description;
                break;
        }

        StrikethroughObjectives(q);
        if (q.CurrentState == Quest.QuestState.Completed) 
            ShowCompleteText();
        else
            HideCompleteText();
    }
    public void ClearActiveQuestInfo()
    {
        AQID.QuestTitle.text = "No Active Quest";
        AQID.Objective1.text = "No Active Quest";
        AQID.Objective2.text = "No Active Quest";
        AQID.Objective3.text = "No Active Quest";
        HideCompleteText();
    }
    public void StrikethroughObjectives(Quest q)
    {
        switch (q.Goals.Count)
        {
            case (1):
                if (q.Goals[0].Completed)
                    AQID.Objective1.text = "<s>" + AQID.Objective1.text + "</s>";
                break;
            case (2):
                if (q.Goals[0].Completed)
                    AQID.Objective1.text = "<s>" + AQID.Objective1.text + "</s>";
                if (q.Goals[1].Completed)
                    AQID.Objective2.text = "<s>" + AQID.Objective2.text + "</s>";
                break;
            case (3):
                if (q.Goals[0].Completed)
                    AQID.Objective1.text = "<s>" + AQID.Objective1.text + "</s>";
                if (q.Goals[1].Completed)
                    AQID.Objective2.text = "<s>" + AQID.Objective2.text + "</s>";
                if (q.Goals[2].Completed)
                    AQID.Objective3.text = "<s>" + AQID.Objective3.text + "</s>";
                break;
        }
    }

    public void ShowCompleteText()
    {
        AQID.CompleteText.gameObject.SetActive(true);
    }

    public void HideCompleteText()
    {
        AQID.CompleteText.gameObject.SetActive(false);
    }
}
