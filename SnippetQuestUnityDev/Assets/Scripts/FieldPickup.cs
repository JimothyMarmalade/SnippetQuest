/*
 * Created by Logan Edmund, 5/8/21
 * Last Modified by Logan Edmund, 5/8/21
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
    //Pickup items will have a sound that plays when they are collected
    public AudioSource pickupSFX;
    public AudioClip pickupSound;


    //CollectPickup() runs when an item is collected by the player
    public virtual void CollectPickup()
    {
        Debug.Log("Running CollectPickup()");
        //Play Sound
        //Do needed action
        Destroy(gameObject);
    }

    public virtual void DestroyPickup()
    {

    }
}
