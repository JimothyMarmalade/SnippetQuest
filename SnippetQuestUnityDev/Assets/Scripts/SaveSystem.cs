/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Handles the saving and loading of all data for the game across scene transitions and between game boots.
 * 
 */

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    #region PlayerData Save/Load Methods
    public static void SavePlayerInventory(InventoryController inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerInventory.nbl";
        Debug.Log("Saving Inventory at " + path);

        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData inventoryData = new InventoryData(inventory);

        formatter.Serialize(stream, inventoryData);
        stream.Close();
    }

    public static InventoryData LoadPlayerInventory()
    {
        string path = Application.persistentDataPath + "/playerInventory.nbl";

        if (File.Exists(path))
        {
            Debug.Log("Loading Inventory from " + path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData inventoryData = formatter.Deserialize(stream) as InventoryData;
            stream.Close();

            return inventoryData;
        }
        else
        {
            Debug.LogError("Inventory save file not found in " + path);
            return null;
        }

    }

    #endregion

    #region SceneData Save/Load Methods

    public static void SaveGeneralTestingData(GeneralTesting_LevelController level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Scene_GeneralTesting_Data.nbl";
        Debug.Log("Saving GeneralTesting Data at " + path);

        FileStream stream = new FileStream(path, FileMode.Create);

        GeneralTesting_LevelData levelData = new GeneralTesting_LevelData(level);

        formatter.Serialize(stream, levelData);
        stream.Close();
    }

    public static GeneralTesting_LevelData LoadGeneralTestingData()
    {
        string path = Application.persistentDataPath + "/Scene_GeneralTesting_Data.nbl";

        if (File.Exists(path))
        {
            Debug.Log("Loading levelData from " + path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GeneralTesting_LevelData levelData = formatter.Deserialize(stream) as GeneralTesting_LevelData;
            stream.Close();

            return levelData;
        }
        else
        {
            Debug.LogError("Level save file not found in " + path);
            return null;
        }
    }

    #endregion


}