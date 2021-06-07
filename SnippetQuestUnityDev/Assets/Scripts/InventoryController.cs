/*
 * Created by Logan Edmund, 4/22/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Handles the addition/removal/editing of items in the player's inventory.
 * Works in conjunction with SnippetController.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }
    public List<Snippet> PlayerSnippets = new List<Snippet>();
    public List<string> PlayerSnippetsSlugs = new List<string>();

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

    public void GiveSnippet(string snippetSlug)
    {
        Debug.Log("InventoryController: Attempting to add Snippet with slug " + snippetSlug);
        if (PlayerSnippetsSlugs.Contains(snippetSlug))
        {
            Debug.LogWarning("PlayerInventory already has this snippet.");
            return;
        }

        PlayerSnippets.Add(SnippetDatabase.Instance.GetSnippet(snippetSlug));
        PlayerSnippetsSlugs.Add(snippetSlug);

        //Send information to the UI and update relevant information
        UIController.Instance.CheckUnlockNewSnippet(snippetSlug);
        UIController.Instance.SpawnSnippetObtainedPopup(SnippetDatabase.Instance.GetSnippet(snippetSlug).snippetType.ToString());
    }

    //Same as GiveSnippet, but doesn't prompt any sort of UI Notification when triggered. Used when loading the player's inventory.
    public void GiveSnippetSilent(string snippetSlug)
    {
        Debug.Log("InventoryController: Attempting to add Snippet with slug " + snippetSlug);
        PlayerSnippets.Add(SnippetDatabase.Instance.GetSnippet(snippetSlug));
        PlayerSnippetsSlugs.Add(snippetSlug);

        //Send information to the UI and update relevant information
        UIController.Instance.CheckUnlockNewSnippet(snippetSlug);
    }

    public void RemoveSnippet(string snippetSlug)
    {
        Debug.Log("InventoryController: Attempting to remove Snippet with slug " + snippetSlug);
        foreach (Snippet s in PlayerSnippets)
        {
            if (s.snippetSlug == snippetSlug)
            {
                PlayerSnippets.Remove(s);
                PlayerSnippetsSlugs.Remove(s.snippetSlug);
                Debug.Log("Inventory Controller: Removed snippet with slug " + snippetSlug);
                return;
            }
        }
        Debug.LogError("InventoryController: Did not find Snippet with slug " + snippetSlug);
    }


    //Save everything in the inventory to an external file
    public void SaveInventory()
    {
        Debug.Log("Saving Player Inventory...");
        SaveSystem.SavePlayerInventory(this);
    }

    //Load the inventory from an external file
    public void LoadInventory()
    {
        Debug.Log("Loading Player Inventory...");

        InventoryData data = SaveSystem.LoadPlayerInventory();
        if (data != null)
        {
            PlayerSnippetsSlugs.Clear();

            foreach (string s in data.inventorySnippetSlugs)
                PlayerSnippetsSlugs.Add(s);
            if (UIController.Instance != null)
                UIController.Instance.FullCheckUnlockNewSnippets();
        }
        else
        {
            Debug.LogWarning("PlayerInventory save file could not be found, creating new blank data.");
            SaveInventory();
        }
    }

}
