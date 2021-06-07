using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GLD_MainMenu : MonoBehaviour
{
    public GameObject HelpPanel1, HelpPanel2, HelpPanel3;
    public GameObject CreditsPanel;

    private GameObject ActivePanel = null;

    public void Awake()
    {
        HelpPanel1.SetActive(false);
        HelpPanel2.SetActive(false);
        HelpPanel3.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void SetActivePanel(int i)
    {
        
        if (i == 1)
        {
            ActivePanel = HelpPanel1;
        }
        else if (i == 2)
        {
            ActivePanel = HelpPanel2;
        }
        else if (i == 3)
        {
            ActivePanel = HelpPanel3;
        }
        else if (i == 4)
        {
            ActivePanel = CreditsPanel;
        }
        else if (i == 0)
        {
            ActivePanel = null;
        }
    }

    public void ShowPanel(int i)
    {
        if (i == 1)
        {
            if (ActivePanel != null)
                ActivePanel.SetActive(false);

            HelpPanel1.SetActive(true);
        }
        else if (i == 2)
        {
            if (ActivePanel != null)
                ActivePanel.SetActive(false);
            HelpPanel2.SetActive(true);

        }
        else if (i == 3)
        {
            if (ActivePanel != null)
                ActivePanel.SetActive(false);
            HelpPanel3.SetActive(true);

        }
        else if (i == 4)
        {
            if (ActivePanel != null)
                ActivePanel.SetActive(false);
            CreditsPanel.SetActive(true);

        }
        else if (i == 0)
        {
            if (ActivePanel != null)
                ActivePanel.SetActive(false);

        }
        SetActivePanel(i);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }













}
