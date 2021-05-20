/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Controls the GeneralTesting scene.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneralTesting_LevelController : LevelController
{
    public bool SnippetPickup_PicrossTestCross_Collected;
    public bool SnippetPickup_Futoshiki2_Collected;

    public Transform PlayerSpawnLocation;

    public Transform PicrossTestCrossLocation;
    public Transform Futoshiki2Location;

    #region Start, Save/Load

    private void Start()
    {
        //LoadLevel();
        //SpawnPlayerAtLocation(PlayerSpawnLocation.position, PlayerSpawnLocation.rotation);
    }
    public override void LoadLevel()
    {
        Debug.Log("Running LoadLevel() in GeneralTesting_LevelController...");
        GeneralTesting_LevelData d = SaveSystem.LoadGeneralTestingData();
        if (d == null)
        {
            SaveLevel();
            LoadLevel();
            return;
        }

        SnippetPickup_PicrossTestCross_Collected = d.SnippetPickup_PicrossTestCross_Collected;
        if (!SnippetPickup_PicrossTestCross_Collected)
        {
            SpawnSnippetPickup(PicrossTestCrossLocation.position, "Picross_TestCross");
        }

        SnippetPickup_Futoshiki2_Collected = d.SnippetPickup_Futoshiki2_Collected;
        if (!SnippetPickup_Futoshiki2_Collected)
        {
            SpawnSnippetPickup(Futoshiki2Location.position, "Futoshiki_2");

        }

        //When everything else has been done to load the level, spawn the player where they're supposed to be.
        SpawnPlayerAtLocation(PlayerSpawnLocation.position, PlayerSpawnLocation.rotation);
    }

    public override void SaveLevel()
    {
        Debug.Log("Saving GeneralTesting Level Data...");
        SaveSystem.SaveGeneralTestingData(this);
    }

    #endregion

    public override void ItemCollected(string itemSlug)
    {
        base.ItemCollected(itemSlug);
        bool saveNeeded = false;

        if (itemSlug == "Picross_TestCross")
        {
            SnippetPickup_PicrossTestCross_Collected = true;
            saveNeeded = true;
        }
        else if (itemSlug == "Futoshiki_2")
        {
            SnippetPickup_Futoshiki2_Collected = true;
            saveNeeded = true;
        }

        if (saveNeeded)
            SaveSystem.SaveGeneralTestingData(this);

    }

}
