/*
 * Created by Logan Edmund, 3/4/21
 * Last Modified by Logan Edmund, 3/8/21
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
    //Panel that displays during on-foot gameplay
    [Header("Third Person Panel(s)")]
    public GameObject thirdPersonPanel;

    //Master panel that displays during snippet selection
    //[snippetType]Buttons holds references to the buttons that activate the snippet panel
    [Header("Snippet Selection Panels")]
    public Animator snippetPanelAnimator;
    public GameObject snippetPanel;

    public GameObject picrossSelectionPanel;
    public Button[] picrossButtons;

    public GameObject futoshikiSelectionPanel;
    public Button[] futoshikiButtons;

    [Header("Snippet Gameplay Panels")]
    public Animator picrossGameplayAnimator;
    public PicrossSnippetBoard picrossBoard;
    public PicrossSnippet testPicrossData;
    public GameObject futoshikiGameplayPanel;

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
        //Add all panels to the appropriate lists for indexing
        masterPanels.Add(thirdPersonPanel);         //ID 0
        masterPanels.Add(snippetPanel);             //ID 1
        masterPanels.Add(dialoguePanel);            //ID 2

        snippetSelectionPanels.Add(picrossSelectionPanel);      //ID 0
        snippetSelectionPanels.Add(futoshikiSelectionPanel);    //ID 1

        //snippetGameplayPanels.Add(picrossGameplayPanel);        //ID 0
        //snippetGameplayPanels.Add(futoshikiGameplayPanel);      //ID 1

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

    public void LoadPicrossGame(int picrossID)
    {
        picrossGameplayAnimator.SetBool("IsOpen", true);
        //Run the necessary methods that load the picross puzzle into the game board, then set bool "puzzleLoaded" to true
        picrossGameplayAnimator.SetBool("PuzzleLoaded", picrossBoard.TryBuildPicrossBoard(testPicrossData));
    }

    public void LeavePicrossGame()
    {
        picrossGameplayAnimator.SetBool("IsOpen", false);
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
