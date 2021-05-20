/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Stores all data pertaining to item pickups, player impact, and other forms of level progression in the GeneralTesting level for use
 * when saving/loading out/in of the scene.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneralTesting_LevelData
{
    public bool SnippetPickup_PicrossTestCross_Collected;
    public bool SnippetPickup_Futoshiki2_Collected;

    public GeneralTesting_LevelData(GeneralTesting_LevelController level)
    {
        SnippetPickup_PicrossTestCross_Collected = level.SnippetPickup_PicrossTestCross_Collected;
        SnippetPickup_Futoshiki2_Collected = level.SnippetPickup_Futoshiki2_Collected;
    }
}
