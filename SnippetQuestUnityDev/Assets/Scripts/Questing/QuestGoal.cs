/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * Controls the display of dialogue onscreen when fed a dialogue pack. Handles UI Elements 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal
{
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {
        //default initialization dealies
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
            Complete();
    }

    public void Complete()
    {
        Completed = true;
    }


}
