/*
 * Created by Logan Edmund, 5/10/21
 * Last Modified by Logan Edmund, 5/11/21
 * 
 * Holds the information needed for individual snippet select buttons on the UI
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnippetLoaderButton : MonoBehaviour
{
    public Button buttonRef;
    public enum snippetType {None, Crossword, Futoshiki, Picross};

    //Data is instantiated to dummy values to ensure they're filled in by a dev
    [Header("Snippet Data")]
    public snippetType type = snippetType.None;
    public string snippetSlug = "No Slug!";
    public int snippetMasterID = -1000;

    public void Awake()
    {
        if (buttonRef == null)
        {
            if (gameObject.GetComponent<Button>() != null)
                buttonRef = gameObject.GetComponent<Button>();
            else
                Debug.LogError("Warning: UI SnippetLoaderButton with slug " + snippetSlug + " does not have a button component.");
        }
    }

    public void LoadSnippet()
    {
        if (type == snippetType.None)
        {
            Debug.LogError("SnippetLoaderButton does not have an associated type!");
            return;
        }
        if (snippetSlug == "No Slug!")
        {
            Debug.LogError("SnippetLoaderButton does not have an associated slug!");
            return;
        }

        if (type == snippetType.Crossword)
            UIController.Instance.LoadCrosswordGame(snippetSlug);
        else if (type == snippetType.Picross)
            UIController.Instance.LoadPicrossGame(snippetSlug);
        else if (type == snippetType.Futoshiki)
            UIController.Instance.LoadFutoshikiGame(snippetSlug);
    }

    public void TurnOn()
    {
        buttonRef.interactable = true;
    }

    public void TurnOff()
    {
        buttonRef.interactable = false;
    }

}
