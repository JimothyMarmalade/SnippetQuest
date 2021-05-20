/*
 * Created by Logan Edmund, 5/19/21
 * Last Modified by Logan Edmund, 5/19/21
 * 
 * Inheritable class. Stores common methods for level controllers.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LevelController : MonoBehaviour
{
    //Create a new type of delegate event, SnippetEvent, that is passed in a snippetSlug
    public delegate void LevelEvent(string itemSlug);
    public static event LevelEvent OnItemCollected;

    public GameObject PlayerAndCameraPrefab;
    public GameObject SnippetPickupPrefab;

    public abstract void LoadLevel();

    public abstract void SaveLevel();

    public void SpawnPlayerAtLocation(Vector3 location, Quaternion rotation)
    {
        Debug.Log("SpawnPlayerAtLocation()...");
        //Spawn player at the specified location
        GameObject p = Instantiate(PlayerAndCameraPrefab, location, rotation);
        //Unchild the Player, camera, and Cinemachine brain
        foreach (Transform child in this.transform)
        {
            child.transform.parent = null;
        }
    }

    public void SpawnSnippetPickup(Vector3 location, string slug)
    {
        //Spawn the Pickup at the location specified
        Quaternion q = new Quaternion(0f, 0f, 0f, 0f);
        SnippetFieldPickup s = Instantiate(SnippetPickupPrefab, location, q).GetComponent<SnippetFieldPickup>();
        s.InitializePickup(this, slug);
    }

    public virtual void ItemCollected(string itemSlug)
    {
        //Does something on a per-level basis when an item is collected
    }


}
