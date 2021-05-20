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
    private string snippetSlug = "No Slug!";

    public override void CollectPickup()
    {
        //Find a reference to the slug in th snippetDatabase and add it to the player's inventory
        InventoryController.Instance.GiveSnippet(snippetSlug);
        Debug.Log("Added Snippet with slug " + snippetSlug + "to player Inventory");

        //Have the level controller update and save that this pickup has been collected.
        level.ItemCollected(snippetSlug);

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
