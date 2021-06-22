/*
 * Created by Logan Edmund, 6/10/21
 * Last Modified by Logan Edmund, 6/10/21
 * 
 *    Class for Non-Snippet items that are collected during exploration
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFieldPickup : FieldPickup
{
    private LevelController level;
    public string itemSlug = "No Slug!";

    public override void CollectPickup()
    {
        //Find a reference to the slug in th snippetDatabase and add it to the player's inventory
        InventoryController.Instance.AddItem(itemSlug);
        Debug.Log("Added Item with slug " + itemSlug + "to player Inventory");

        //Have the level controller update and save that this pickup has been collected.
        if (level != null)
        {
            level.ItemCollected(itemSlug);
        }
        else
        {
            Debug.LogWarning("Item Pickup did not have a reference to this level's levelcontroller and may exist as a permanent part of the scene.");
        }

        base.CollectPickup();
    }

}
