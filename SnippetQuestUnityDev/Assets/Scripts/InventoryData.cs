/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Stores data concerning what is held in the player's inventory for saving/loading and scene transitions
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public string[] inventorySnippetSlugs;

    public InventoryData(InventoryController inventory)
    {
        inventorySnippetSlugs = new string[inventory.PlayerSnippetsSlugs.Count];

        int i = 0;
        foreach (string s in inventory.PlayerSnippetsSlugs)
        {
            inventorySnippetSlugs[i] = s;
            i++;
        }

    }
}
