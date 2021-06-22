/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Handles the loading of new scenes and the transitioning between them
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler 
{
    public enum Scene
    {
        GameInitialization, LoadingScreen,
        Menu_SnippetQuestMainMenu,
        Debug_GeneralTesting,
        Level_LeadPark,
        Minigame_LeadParkTicTacToe
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {

        SceneManager.LoadScene(Scene.LoadingScreen.ToString());

        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };


    }

    public static void LoadScreenCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

}
