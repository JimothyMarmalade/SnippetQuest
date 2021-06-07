/*
 * Created by Logan Edmund, 5/27/21
 * Last Modified by Logan Edmund, 5/27/21
 * 
 * 
 * Scripting for the main menu (at least the pre-alpha version)
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : LevelController
{
    public void GoToGeneralTesting()
    {
        GameManager.Instance.GoToScene(SceneHandler.Scene.Debug_GeneralTesting);
    }

    public void GoToLeadPark()
    {
        GameManager.Instance.GoToScene(SceneHandler.Scene.Level_LeadPark);
    }

    public override void LoadLevel()
    {
        //throw new System.NotImplementedException();
    }

    public override void SaveLevel()
    {
        //throw new System.NotImplementedException();
    }
}
