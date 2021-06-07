using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadPark_LevelController : LevelController
{
    public Transform PlayerSpawnLocation;



    private void Start()
    {

        LoadLevel();
        //Set the available snippets up in the Snippet UI
        UIController.Instance.FullCheckUnlockNewSnippets();
        //After the level has been loaded, spawn the player in the desired postion.
        SpawnPlayerAtLocation(PlayerSpawnLocation.position, PlayerSpawnLocation.rotation);
    }



    public override void LoadLevel()
    {
        Debug.Log("Lead park does not have a load level function yet!");
    }

    public override void SaveLevel()
    {
        Debug.Log("Lead park does not have a save level function yet!");

    }






}
