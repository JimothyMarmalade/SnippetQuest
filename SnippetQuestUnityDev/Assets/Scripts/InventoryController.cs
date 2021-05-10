/*
 * Created by Logan Edmund, 4/22/21
 * Last Modified by Logan Edmund, 4/22/21
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
    }

    public void GiveSnippet(string snippetSlug)
    {
        Debug.Log("InventoryController: Attempting to add Snippet with slug " + snippetSlug);
        PlayerSnippets.Add(SnippetDatabase.Instance.GetSnippet(snippetSlug));
        PlayerSnippetsSlugs.Add(snippetSlug);
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



}
