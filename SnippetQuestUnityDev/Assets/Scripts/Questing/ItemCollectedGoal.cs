/*
 * Created by Logan Edmund, 6/10/21
 * Last Modified by Logan Edmund, 6/10/21
 * 
 * A quest goal for quests -- tracks if an item has been added to or is currently located in the player's inventory
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemCollectedGoal", menuName = "Quests/New ItemCollectedGoal")]
public class ItemCollectedGoal : QuestGoal
{
    //Refers to the slug of the item this goal is concerned with
    [Header("Item to Find")]
    public string ItemSlug;

    public ItemCollectedGoal(Quest quest, string itemSlug, string description, bool completed, int requiredAmount)
    {
        this.AssignedQuest = quest;
        this.ItemSlug = itemSlug;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = InventoryController.Instance.ItemAmountInInventory(itemSlug);
        this.RequiredAmount = requiredAmount;
    }

    public override void Init(Quest q)
    {
        base.Init(q);

        //Evaluate to see if the player has the required amount already.
        this.CurrentAmount = InventoryController.Instance.ItemAmountInInventory(this.ItemSlug);
        if (!Evaluate())
            InventoryController.OnItemCollected += NewItemCollected;
    }

    void NewItemCollected(string itemSlug)
    {
        if (itemSlug == this.ItemSlug)
        {
            this.CurrentAmount = InventoryController.Instance.ItemAmountInInventory(itemSlug);
            if (Evaluate())
                InventoryController.OnItemCollected -= NewItemCollected;
        }
    }


}
