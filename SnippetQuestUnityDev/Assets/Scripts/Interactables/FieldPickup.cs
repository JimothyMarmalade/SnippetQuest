/*
 * Created by Logan Edmund, 5/8/21
 * Last Modified by Logan Edmund, 5/11/21
 * 
 *    Inheritable class - used by objects that can be picked up in the field without any deliberate interaction by the player (don't need to press Interact
 * and whatnot). 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPickup : MonoBehaviour
{
    public string sfxName = "No SFX!";

    //CollectPickup() runs when an item is collected by the player
    public virtual void CollectPickup()
    {
        Debug.Log("Running CollectPickup()");
        if (AudioManager.Instance != null)
            AudioManager.Instance.Play(sfxName);
        else
            Debug.LogWarning("No AudioManager Instance exists!");

        Destroy(gameObject);
    }

    public virtual void DestroyPickup()
    {

    }
}
