/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * GameManager.cs handles all the overhead for loading/saving, instigating scene transitions, spawning critical objects.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        PlayerEnteredNewScene();
    }

    //Called when the player enters a new scene
    public void PlayerEnteredNewScene()
    {
        //Load the Player's inventory
        InventoryController.Instance.LoadInventory();

        //Load the Scene according to the saved data
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>().LoadLevel();
    }

    //Called when the player leaves a scene
    public void PlayerExitingScene()
    {
        //Save the player's inventory
        InventoryController.Instance.SaveInventory();

        //Save the current level
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>().SaveLevel();
    }
}
