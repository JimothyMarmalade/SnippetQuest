/*
 * Created by Logan Edmund, 3/4/21
 * Last Modified by Logan Edmund, 3/14/21
 * 
 * Function of NewUIController.cs is to handle all UI elements in Snippetquest, most notably the HUD during on-foot exploration and the
 * Snippet menu during Snippet interaction and selection. 
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Controls all UI Elements
public class NewUIController : MonoBehaviour
{
    public static NewUIController Instance { get; set; }

    //Panel that displays during on-foot gameplay
    [Header("Third Person Panel(s)")]
    public GameObject thirdPersonPanel;

    [Header("Active Quest Display")]
    public ActiveQuestInfoDisplay AQID;

    [Header("Snippet Obtained Popup")]
    public RectTransform SOPSpawnLocation1;
    public RectTransform SOPSpawnLocation2;
    public RectTransform SOPSpawnLocation3;
    private SnippetObtainedPopup SOP1;
    private SnippetObtainedPopup SOP2;
    private SnippetObtainedPopup SOP3;

    public GameObject SOPPrefab;

    //Master panel that displays during snippet selection
    //[snippetType]Buttons holds references to the buttons that activate the snippet panel
    [Header("Snippet Selection Panels")]
    public Animator snippetPanelAnimator;
    public GameObject snippetPanel;
    public Button LeaveSnippetPanelButton;

    public GameObject picrossSelectionPanel;
    public List<SnippetLoaderButton> picrossButtons;

    public GameObject futoshikiSelectionPanel;
    public List<SnippetLoaderButton> futoshikiButtons;

    public GameObject crosswordSelectionPanel;
    public List<SnippetLoaderButton> crosswordButtons;

    [Header("Snippet Gameplay Panels")]
    public Animator picrossGameplayAnimator;
    public PicrossSnippetBoard picrossBoard;

    public Animator futoshikiGameplayAnimator;
    public FutoshikiSnippetBoard futoshikiBoard;

    public Animator crosswordGameplayAnimator;
    public CrosswordSnippetBoard crosswordBoard;

    [Header("Dialog Panels")]
    public GameObject dialoguePanel;


    //masterPanels holds the primary display panels (HUD, Snippets)
    private List<GameObject> masterPanels = new List<GameObject>();
    //snippetSelectionPanels holds the different types of panels in the Snippet Selection menu
    private List<GameObject> snippetSelectionPanels = new List<GameObject>();
    //snippetGameplayPanels holds the panels that contain the actual gameboards used to view/solve panels
    private List<GameObject> snippetGameplayPanels = new List<GameObject>();

    //activeMasterPanelID instantiated to -1 for null value
    private int activeMasterPanelID = -1;
    //activeSnippetSelectionPanelID instantiated to -1 for null value
    private int activeSnippetSelectionPanelID = -1;
    //activeSnippetGameplayPanelID instantiated to -1 for null value
    private int activeSnippetGameplayPanelID = -1;

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

        //Add all panels to the appropriate lists for indexing
        masterPanels.Add(thirdPersonPanel);         //ID 0
        masterPanels.Add(snippetPanel);             //ID 1
        masterPanels.Add(dialoguePanel);            //ID 2

        snippetSelectionPanels.Add(picrossSelectionPanel);      //ID 0
        snippetSelectionPanels.Add(futoshikiSelectionPanel);    //ID 1

        ClearActiveQuestInfo();

        LockAllSnippets();
        FullCheckUnlockNewSnippets();
    }

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
        if (q.IsCompleted)
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

    //----------Methods for activating master panels
    public void ChangeActiveMasterPanel(int i)
    {
        DeactivateMasterPanel();
        SetActiveMasterPanel(i);
    }

    public void SetActiveMasterPanel(int i)
    {
        if (i != -1)
        {
            masterPanels[i].SetActive(true);
            activeMasterPanelID = i;
        }
    }

    public void DeactivateMasterPanel()
    {
        if (activeMasterPanelID > -1)
            masterPanels[activeMasterPanelID].SetActive(false);
        activeMasterPanelID = -1;
    }

    public void ActivateSnippetSelectionPanel()
    {
        ChangeActiveMasterPanel(1);
        snippetPanelAnimator.SetBool("IsOpen", true);
    }

    public void DeactivateSnippetSelectionPanel()
    {
        ChangeActiveMasterPanel(0);
        snippetPanelAnimator.SetBool("IsOpen", false);
    }

    public void ChangeSelectionPanel(int i)
    {
        //No panel == 0, Picross == 1, Futoshiki == 2
        //Deactivate old Panel
        Debug.Log("Running changeselection panel with i=" + i);

        if (activeSnippetSelectionPanelID != -1)
        {
            Debug.Log("SelectionID != -1");
            //If there's no active panel, do nothing
            if (activeSnippetSelectionPanelID == 1)
                snippetPanelAnimator.SetBool("ShowPicross", false);
            else if (activeSnippetSelectionPanelID == 2)
                snippetPanelAnimator.SetBool("ShowFutoshiki", false);
            else if (activeSnippetSelectionPanelID == 3)
                snippetPanelAnimator.SetBool("ShowCrossword", false);

        }
        //Activate new panel
        if (i == 1)
            snippetPanelAnimator.SetBool("ShowPicross", true);
        else if (i == 2)
            snippetPanelAnimator.SetBool("ShowFutoshiki", true);
        else if (i == 3)
            snippetPanelAnimator.SetBool("ShowCrossword", true);

        activeSnippetSelectionPanelID = i;
    }


    //----------Methods for loading and leaving the individual game panels
    public void LoadPicrossGame(string picrossSlug)
    {
        picrossGameplayAnimator.SetBool("IsOpen", true);
        //Run the necessary methods that load the picross puzzle into the game board, then set bool "puzzleLoaded" to true
        picrossGameplayAnimator.SetBool("PuzzleLoaded", picrossBoard.TryBuildPicrossBoard(SnippetDatabase.Instance.GetPicrossSnippet(picrossSlug)));
    }

    public void LeavePicrossGame()
    {
        picrossGameplayAnimator.SetBool("IsOpen", false);
    }

    public void LoadFutoshikiGame(string futoshikiSlug)
    {
        futoshikiGameplayAnimator.SetBool("IsOpen", true);
        futoshikiGameplayAnimator.SetBool("PuzzleLoaded", futoshikiBoard.TryBuildFutoshikiBoard(SnippetDatabase.Instance.GetFutoshikiSnippet(futoshikiSlug)));
    }

    public void LeaveFutoshikiGame()
    {
        futoshikiGameplayAnimator.SetBool("IsOpen", false);
    }

    public void LoadCrosswordGame(string crosswordSlug)
    {
        crosswordGameplayAnimator.SetBool("IsOpen", true);
        crosswordGameplayAnimator.SetBool("PuzzleLoaded", crosswordBoard.TryBuildCrosswordBoard(SnippetDatabase.Instance.GetCrosswordSnippet(crosswordSlug)));
    }

    public void LeaveCrosswordGame()
    {
        crosswordGameplayAnimator.SetBool("IsOpen", false);
    }

    //----------Methods for turning on/off loader buttons

    //Disables all snippets from being solved
    public void LockAllSnippets()
    {
        foreach (SnippetLoaderButton s in picrossButtons)
            s.TurnOff();
        foreach (SnippetLoaderButton s in futoshikiButtons)
            s.TurnOff();
        foreach (SnippetLoaderButton s in crosswordButtons)
            s.TurnOff();
    }


    //Does a full scan of each snippet in player's inventory and unlocks associated buttons
    public void FullCheckUnlockNewSnippets()
    {
        foreach (SnippetLoaderButton s in picrossButtons)
        {
            if (InventoryController.Instance.PlayerSnippetsSlugs.Contains(s.snippetSlug))
                s.TurnOn();
        }
        foreach (SnippetLoaderButton s in futoshikiButtons)
        {
            if (InventoryController.Instance.PlayerSnippetsSlugs.Contains(s.snippetSlug))
                s.TurnOn();
        }
        foreach (SnippetLoaderButton s in crosswordButtons)
        {
            if (InventoryController.Instance.PlayerSnippetsSlugs.Contains(s.snippetSlug))
                s.TurnOn();
        }
    }

    //Unlocks a specific snippet when passed a slug
    public void CheckUnlockNewSnippet(string snippetSlug)
    {
        Debug.Log("CheckUnlockNewSnippet() called...");
        foreach (SnippetLoaderButton s in picrossButtons)
        {
            if (s.snippetSlug == snippetSlug)
            {
                s.TurnOn();
                Debug.Log("Unlocked " + snippetSlug + " on selection panel!");
                return;
            }
        }
        foreach (SnippetLoaderButton s in futoshikiButtons)
        {
            if (s.snippetSlug == snippetSlug)
            {
                s.TurnOn();
                Debug.Log("Unlocked " + snippetSlug + " on selection panel!");
                return;
            }
        }
        foreach (SnippetLoaderButton s in crosswordButtons)
        {
            if (s.snippetSlug == snippetSlug)
            {
                s.TurnOn();
                Debug.Log("Unlocked " + snippetSlug + " on selection panel!");
                return;
            }
        }

        Debug.LogError("UI Controller could not find slug " + snippetSlug + " in any SnippetLoaderButtons!");
    }

    /*
    //----------Methods for activating SnippetSelection panels
    public void ChangeActiveSelectionPanel(int i)
    {
        DeactivateSelectionPanel();
        SetActiveSelectionPanel(i);
    }

    public void SetActiveSelectionPanel(int i)
    {
        snippetSelectionPanels[i].SetActive(true);
        activeSnippetSelectionPanelID = i;
    }

    public void DeactivateSelectionPanel()
    {
        snippetSelectionPanels[activeSnippetSelectionPanelID].SetActive(false);
        activeSnippetSelectionPanelID = -1;
    }

    //----------Methods for activating SnippetGameplay panels
    public void ChangeActiveGameplayPanel(int i)
    {
        DeactivateSelectionPanel();
        SetActiveSelectionPanel(i);
    }

    public void SetActiveGameplayPanel(int i)
    {
        snippetGameplayPanels[i].SetActive(true);
        activeSnippetGameplayPanelID = i;
    }

    public void DeactivateGameplayPanel()
    {
        snippetGameplayPanels[activeSnippetGameplayPanelID].SetActive(false);
        activeSnippetGameplayPanelID = -1;
    }
    */
}
