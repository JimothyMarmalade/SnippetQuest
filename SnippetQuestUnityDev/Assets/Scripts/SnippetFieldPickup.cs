/*
 * Created by Logan Edmund, 5/8/21
 * Last Modified by Logan Edmund, 5/8/21
 * 
 *    Inherits from FieldPickup. Holds a Snippet that is added to the player's inventory upon collecting.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnippetFieldPickup : FieldPickup
{
    public string snippetSlug = "No Slug!";

    public override void CollectPickup()
    {
        //Find a reference to the slug in th snippetDatabase and add it to the player's inventory
        InventoryController.Instance.GiveSnippet(snippetSlug);
        Debug.Log("Added Snippet with slug " + snippetSlug + "to player Inventory");



        base.CollectPickup();
    }
}
