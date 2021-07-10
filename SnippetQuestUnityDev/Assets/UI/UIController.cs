/*
 * Created by Logan Edmund, 3/4/21
 * Last Modified by Logan Edmund, 5/21/21
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
public class UIController : MonoBehaviour
{
    public static UIController Instance { get; set; }

    //Panel that displays during on-foot gameplay
    [Header("Exploration Panel")]
    public GameObject ExplorationPanelPrefab;
    public bool SceneRequiresExplorationPanel;
    private GameObject ExplorationPanel;
    public UI_ExplorationDisplay UI_EXP;

    [Header("Snippet Panel")]
    public GameObject SnippetPanelPrefab;
    public bool SceneRequiresSnippetPanel;
    private GameObject SnippetPanel;
    public UI_SnippetDisplay UI_SNIPPET;

    [Header("Dialogue Panel")]
    public GameObject DialoguePanelPrefab;
    public bool SceneRequiresDialoguePanel;
    private GameObject DialoguePanel;

    [Header("Blackout Panel")]
    public GameObject BlackoutPanel;

    /*
    //Master panel that displays during snippet selection
    //[snippetType]Buttons holds references to the buttons that activate the snippet panel
    [Header("Snippet Selection Panels")]
    public GameObject snippetPanel;
    public Animator snippetPanelAnimator;
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
    */


    [Header("Dialog Panels")]
    //public GameObject dialoguePanel;


    //masterPanels holds the primary display panels (HUD, Snippets)
    public List<GameObject> masterPanels = new List<GameObject>();
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

        /*
        //If the scene requires an exploration panel, load the panel.
        if (SceneRequiresExplorationPanel)
        {
            LoadExplorationUI();

            ClearActiveQuestInfo();
        }

        if (SceneRequiresSnippetPanel)
        {
            LoadSnippetUI();
        }

        if (SceneRequiresDialoguePanel)
        {
            LoadDialoguePanel();
        }
        */

        //Add all panels to the appropriate lists for indexing
        masterPanels.Add(ExplorationPanel);           //ID 0
        masterPanels.Add(SnippetPanel);               //ID 1
        //masterPanels.Add(dialoguePanel);            //ID 2

    }

    private void Start()
    {
        BlackoutPanel.SetActive(false);
    }

    public void ReloadHUD(bool needsExploration, bool needsSnippets, bool needsDialogue)
    {
        UnloadAllHUD();
        masterPanels.Clear();

        if (needsExploration)
        {
            SceneRequiresExplorationPanel = true;
            LoadExplorationUI();
            masterPanels.Add(ExplorationPanel);
        }
        if (needsSnippets)
        {
            SceneRequiresSnippetPanel = true;
            LoadSnippetUI();
            masterPanels.Add(SnippetPanel);
        }
        if (needsDialogue)
        {
            SceneRequiresDialoguePanel = true;
            LoadDialoguePanel();
        }

    }

    private void LoadExplorationUI()
    {
        ExplorationPanel = Instantiate(ExplorationPanelPrefab, this.transform);
        UI_EXP = ExplorationPanel.GetComponent<UI_ExplorationDisplay>();
        ClearActiveQuestInfo();
    }

    private void LoadSnippetUI()
    {
        SnippetPanel = Instantiate(SnippetPanelPrefab, this.transform);
        UI_SNIPPET = SnippetPanel.GetComponent<UI_SnippetDisplay>();

        LockAllSnippets();
        FullCheckUnlockNewSnippets();
    }

    private void LoadDialoguePanel()
    {
        DialoguePanel = Instantiate(DialoguePanelPrefab, this.transform);
    }

    private void UnloadExplorationUI()
    {
        Destroy(ExplorationPanel);
        SceneRequiresExplorationPanel = false;
    }

    private void UnloadSnippetUI()
    {
        Destroy(SnippetPanel);
        SceneRequiresSnippetPanel = false;
    }

    private void UnloadDialoguePanel()
    {
        Destroy(DialoguePanel);
        SceneRequiresDialoguePanel = false;
    }

    private void UnloadAllHUD()
    {
        UnloadExplorationUI();
        UnloadSnippetUI();
        UnloadDialoguePanel();
    }

    //----------Spawn a new Snippet Obtained Prefab
    public void SpawnSnippetObtainedPopup(string snippetType)
    {
        if (UI_EXP != null) 
            UI_EXP.SpawnSnippetObtainedPopup(snippetType);
        else
        {
            LoadExplorationUI();
            UI_EXP.SpawnSnippetObtainedPopup(snippetType);
        }

    }

    //----------Update the Active Quest Information
    public void UpdateActiveQuestInfo(Quest q)
    {
        if (UI_EXP != null)
            UI_EXP.UpdateActiveQuestInfo(q);

        else
        {
            LoadExplorationUI();
            UI_EXP.UpdateActiveQuestInfo(q);

        }
    }
    public void ClearActiveQuestInfo()
    {
        if (UI_EXP != null)
            UI_EXP.ClearActiveQuestInfo();

        else
        {
            LoadExplorationUI(); 
            UI_EXP.ClearActiveQuestInfo();
        }
    }
    public void StrikethroughObjectives(Quest q)
    {
        if (UI_EXP != null)
            UI_EXP.StrikethroughObjectives(q);

        else
        {
            LoadExplorationUI();
            UI_EXP.StrikethroughObjectives(q);
        }
    }

    public void ShowCompleteText()
    {
        if (UI_EXP != null)
            UI_EXP.ShowCompleteText();

        else
        {
            LoadExplorationUI();
            UI_EXP.ShowCompleteText();
        }
    }

    public void HideCompleteText()
    {
        if (UI_EXP != null)
            UI_EXP.HideCompleteText();

        else
        {
            LoadExplorationUI();
            UI_EXP.HideCompleteText();
        }
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

    //Methods for activating Snippet Panels
    public void ActivateSnippetSelectionPanel()
    {
        ChangeActiveMasterPanel(1);
        UI_SNIPPET.snippetPanelAnimator.SetBool("IsOpen", true);
    }

    public void DeactivateSnippetSelectionPanel()
    {
        ChangeActiveMasterPanel(0);
        UI_SNIPPET.snippetPanelAnimator.SetBool("IsOpen", false);
    }

    public void ChangeSnippetSelectionPanel(int i)
    {
        //No panel == 0, Picross == 1, Futoshiki == 2
        //Deactivate old Panel
        Debug.Log("Running changeselection panel with i=" + i);

        if (activeSnippetSelectionPanelID != -1)
        {
            Debug.Log("SelectionID != -1");
            //If there's no active panel, do nothing
            if (activeSnippetSelectionPanelID == 1)
                UI_SNIPPET.snippetPanelAnimator.SetBool("ShowPicross", false);
            else if (activeSnippetSelectionPanelID == 2)
                UI_SNIPPET.snippetPanelAnimator.SetBool("ShowFutoshiki", false);
            else if (activeSnippetSelectionPanelID == 3)
                UI_SNIPPET.snippetPanelAnimator.SetBool("ShowCrossword", false);

        }
        //Activate new panel
        if (i == 1)
            UI_SNIPPET.snippetPanelAnimator.SetBool("ShowPicross", true);
        else if (i == 2)
            UI_SNIPPET.snippetPanelAnimator.SetBool("ShowFutoshiki", true);
        else if (i == 3)
            UI_SNIPPET.snippetPanelAnimator.SetBool("ShowCrossword", true);

        activeSnippetSelectionPanelID = i;
    }

    #region Methods for Loading/Leaving individual game Panels
    //----------Methods for loading and leaving the individual game panels
    public void LoadPicrossGame(string picrossSlug)
    {
        UI_SNIPPET.picrossGameplayAnimator.SetBool("IsOpen", true);
        //Run the necessary methods that load the picross puzzle into the game board, then set bool "puzzleLoaded" to true
        UI_SNIPPET.picrossGameplayAnimator.SetBool("PuzzleLoaded", UI_SNIPPET.picrossBoard.TryBuildPicrossBoard(SnippetDatabase.Instance.GetPicrossSnippet(picrossSlug)));
    }

    public void LeavePicrossGame()
    {
        UI_SNIPPET.picrossGameplayAnimator.SetBool("IsOpen", false);
    }

    public void LoadFutoshikiGame(string futoshikiSlug)
    {
        UI_SNIPPET.futoshikiGameplayAnimator.SetBool("IsOpen", true);
        UI_SNIPPET.futoshikiGameplayAnimator.SetBool("PuzzleLoaded", UI_SNIPPET.futoshikiBoard.TryBuildFutoshikiBoard(SnippetDatabase.Instance.GetFutoshikiSnippet(futoshikiSlug)));
    }

    public void LeaveFutoshikiGame()
    {
        UI_SNIPPET.futoshikiGameplayAnimator.SetBool("IsOpen", false);
    }

    public void LoadCrosswordGame(string crosswordSlug)
    {
        UI_SNIPPET.crosswordGameplayAnimator.SetBool("IsOpen", true);
        UI_SNIPPET.crosswordGameplayAnimator.SetBool("PuzzleLoaded", UI_SNIPPET.crosswordBoard.TryBuildCrosswordBoard(SnippetDatabase.Instance.GetCrosswordSnippet(crosswordSlug)));
    }

    public void LeaveCrosswordGame()
    {
        UI_SNIPPET.crosswordGameplayAnimator.SetBool("IsOpen", false);
    }

    #endregion

    #region Methods for turning on/off the buttons for individual snippets
    //----------Methods for turning on/off loader buttons

    //Disables all snippets from being solved
    public void LockAllSnippets()
    {
        foreach (SnippetLoaderButton s in UI_SNIPPET.picrossButtons)
            s.TurnOff();
        foreach (SnippetLoaderButton s in UI_SNIPPET.futoshikiButtons)
            s.TurnOff();
        foreach (SnippetLoaderButton s in UI_SNIPPET.crosswordButtons)
            s.TurnOff();
    }

    //Does a full scan of each snippet in player's inventory and unlocks associated buttons
    public void FullCheckUnlockNewSnippets()
    {
        try
        {
            foreach (SnippetLoaderButton s in UI_SNIPPET.picrossButtons)
            {
                if (InventoryController.Instance.PlayerSnippetsSlugs.Contains(s.snippetSlug))
                    s.TurnOn();
            }
            foreach (SnippetLoaderButton s in UI_SNIPPET.futoshikiButtons)
            {
                if (InventoryController.Instance.PlayerSnippetsSlugs.Contains(s.snippetSlug))
                    s.TurnOn();
            }
            foreach (SnippetLoaderButton s in UI_SNIPPET.crosswordButtons)
            {
                if (InventoryController.Instance.PlayerSnippetsSlugs.Contains(s.snippetSlug))
                    s.TurnOn();
            }
        }
        catch
        {
            Debug.LogWarning("The Snippet HUD was not able to be accessed!");
        }
    }

    //Unlocks a specific snippet when passed a slug
    public void CheckUnlockNewSnippet(string snippetSlug)
    {
        Debug.Log("CheckUnlockNewSnippet() called...");
        foreach (SnippetLoaderButton s in UI_SNIPPET.picrossButtons)
        {
            if (s.snippetSlug == snippetSlug)
            {
                s.TurnOn();
                Debug.Log("Unlocked " + snippetSlug + " on selection panel!");
                return;
            }
        }
        foreach (SnippetLoaderButton s in UI_SNIPPET.futoshikiButtons)
        {
            if (s.snippetSlug == snippetSlug)
            {
                s.TurnOn();
                Debug.Log("Unlocked " + snippetSlug + " on selection panel!");
                return;
            }
        }
        foreach (SnippetLoaderButton s in UI_SNIPPET.crosswordButtons)
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

    #endregion
}
