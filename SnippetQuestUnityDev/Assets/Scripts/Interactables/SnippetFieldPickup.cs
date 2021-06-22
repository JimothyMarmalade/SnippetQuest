/*
 * Created by Logan Edmund, 5/8/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 *    Inherits from FieldPickup. Holds a Snippet that is added to the player's inventory upon collecting.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnippetFieldPickup : FieldPickup
{
    private LevelController level;
    public string snippetSlug = "No Slug!";

    public override void CollectPickup()
    {
        //Find a reference to the slug in th snippetDatabase and add it to the player's inventory
        InventoryController.Instance.AddSnippet(snippetSlug);
        Debug.Log("Added Snippet with slug " + snippetSlug + "to player Inventory");

        //Have the level controller update and save that this pickup has been collected.
        if (level != null)
        {
            level.ItemCollected(snippetSlug);
        }
        else
        {
            Debug.LogWarning("Snippet Pickup did not have a reference to this level's levelcontroller and may exist as a permanent part of the scene.");
        }

        base.CollectPickup();
    }

    public string GetPickupSlug()
    {
        return snippetSlug;
    }

    public void InitializePickup(LevelController level, string slug)
    {
        this.level = level;
        snippetSlug = slug;
    }
}
