using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadPark_LevelController : LevelController
{
    public Transform PlayerSpawnLocation;

    private void Awake()
    {
        AudioManager.Instance.LoadSoundCollection(AudioManager.LoadedSoundCollection.Level_LeadPark);
        AudioManager.Instance.SetDynamicBGMSounds("BGM_Forest_Exploring", "BGM_Forest_Snippet", "BGM_Forest_Activity");

        UIController.Instance.ReloadHUD(true, true, true);
    }

    private void Start()
    {
        Debug.Log("Running Start() for LeadPark_LevelController...");
        LoadLevel();
        //Set the available snippets up in the Snippet UI
        UIController.Instance.FullCheckUnlockNewSnippets();
        //After the level has been loaded, spawn the player in the desired postion.
        SpawnPlayerAtLocation(PlayerSpawnLocation.position, PlayerSpawnLocation.rotation);

        AudioManager.Instance.StartBGM(AudioManager.CurrentBGM.Exploration);
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
