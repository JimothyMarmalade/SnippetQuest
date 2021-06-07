/*
 * Created by Logan Edmund, 5/21/21
 * Last Modified by Logan Edmund, 5/21/21
 * 
 * UI_SnippetDisplay is the UI that appears whenever the player interacts with a serene place and prepares to solve a Snippet. It holds means of accessing
 * every Snippet in the game, locked and unlocked.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SnippetDisplay : MonoBehaviour
{
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


    //activeSnippetSelectionPanelID instantiated to -1 for null value
    private int activeSnippetSelectionPanelID = -1;

    //snippetSelectionPanels holds the different types of panels in the Snippet Selection menu
    private List<GameObject> snippetSelectionPanels = new List<GameObject>();

    private void Awake()
    {
        snippetSelectionPanels.Add(picrossSelectionPanel);      //ID 0
        snippetSelectionPanels.Add(futoshikiSelectionPanel);    //ID 1
        snippetSelectionPanels.Add(crosswordSelectionPanel);
    }



    #region Methods for activating Snippet Selection Panels

    //Methods for activating Snippet Panels
    public void ActivateSnippetSelectionPanel()
    {
        UIController.Instance.ChangeActiveMasterPanel(1);
        snippetPanelAnimator.SetBool("IsOpen", true);
    }

    public void DeactivateSnippetSelectionPanel()
    {
        UIController.Instance.ChangeActiveMasterPanel(0);
        snippetPanelAnimator.SetBool("IsOpen", false);
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


    #endregion



    #region Methods for Loading/Leaving individual game Panels
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

    #endregion

    #region Methods for turning on/off the buttons for individual snippets
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

    #endregion

}
