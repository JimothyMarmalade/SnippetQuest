using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Controls all UI Elements
public class UIController : MonoBehaviour
{
    public GameObject ThirdPersonPanel;
    public GameObject SnippetSelectionPanel;
    public GameObject PicrossPanel1;
    public GameObject PicrossPanel2;
    public GameObject PicrossPanel3;

    public GameObject FutoshikiPanel1;

    public GameObject PicrossButton1;
    public GameObject PicrossButton2;
    public GameObject PicrossButton3;
    public GameObject FutoshikiButtonRandom;

    public GameObject Checkmark;

    public TextMeshProUGUI HUDMessage;

    public AudioSource UISFX;
    public AudioClip PuzzleSolved;


    private List<GameObject> AllPanels = new List<GameObject>();
    private List<GameObject> AllButtons = new List<GameObject>();
    //ActivePanelID instantiated to -1 for null value
    private int ActivePanelID = -1;

    private void Awake()
    {
        AllPanels.Add(ThirdPersonPanel);
        AllPanels.Add(SnippetSelectionPanel);
        AllPanels.Add(PicrossPanel1);
        AllPanels.Add(PicrossPanel2);
        AllPanels.Add(PicrossPanel3);
        AllPanels.Add(FutoshikiPanel1);

        AllButtons.Add(PicrossButton1);
        AllButtons.Add(PicrossButton2);
        AllButtons.Add(PicrossButton3);
        AllButtons.Add(FutoshikiButtonRandom);

        DeactivateAllButtons();

        HideAllPanels();
        ShowPanel(0);

    }

    public void ActivateSnippetButton(int ID)
    {
        SetButtonInteractable(ID-1);
    }


    public void ShowPanel(int panelID)
    {
        if (ActivePanelID != -1)
            HidePanel(ActivePanelID);

        AllPanels[panelID].SetActive(true);
        ActivePanelID = panelID;
    }

    public void HidePanel(int panelID)
    {
        AllPanels[panelID].SetActive(false);
    }

    public void ShowButton(int buttonID)
    {
        AllButtons[buttonID].SetActive(true);
    }

    public void HideButton(int buttonID)
    {
        AllButtons[buttonID].SetActive(false);
    }

    public void HideAllPanels()
    {
        foreach (GameObject obj in AllPanels)
            obj.SetActive(false);
        ActivePanelID = -1;
    }

    public void HideAllButtons()
    {
        foreach (GameObject obj in AllButtons)
            obj.SetActive(false);
    }

    public void DeactivateAllButtons()
    {
        foreach (GameObject obj in AllButtons)
        {
            obj.GetComponent<Button>().interactable = false;

        }
    }

    public void ActivateAllButtons()
    {
        foreach (GameObject obj in AllPanels)
            obj.GetComponent<Button>().interactable = true;
    }

    public void SetButtonInteractable(int buttonID)
    {
        AllButtons[buttonID].GetComponent<Button>().interactable = true;
    }

    public void SpawnCheckmark(int puzzleID)
    {
        UISFX.PlayOneShot(PuzzleSolved);

        GameObject cm = Instantiate(Checkmark);
        RectTransform reference = null;


        if (puzzleID == 1)
        {
            reference = PicrossButton1.GetComponent<RectTransform>();
            cm.transform.SetParent(PicrossButton1.transform);
        }
        else if (puzzleID == 2)
        {
            reference = PicrossButton2.GetComponent<RectTransform>();
            cm.transform.SetParent(PicrossButton2.transform);
        }
        else if (puzzleID == 3)
        {
            reference = PicrossButton3.GetComponent<RectTransform>();
            cm.transform.SetParent(PicrossButton3.transform);
        }
        if (reference != null)
        {
            cm.GetComponent<RectTransform>().localPosition = new Vector2(0 + reference.sizeDelta.x/2, 0 + reference.sizeDelta.y/2);
            cm.SetActive(true);
        }

    }

    public void UpdateHUDMessage(string s)
    {
        HUDMessage.text = s;
    }

}
