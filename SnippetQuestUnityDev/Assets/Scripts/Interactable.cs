/*
 * Created by Logan Edmund, 2/28/21
 * Last Modified by Logan Edmund, 5/10/21
 * 
 * Base script/methods that all interactable objects will inherit from.
 * 
 * 
 */

using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Called Interact on " + gameObject.name + ", interacting with base Interactble class.");
        //Perform interact actions here or in extended classes
    }

    //Handles moving the player to a specific spot before the interaction takes place.
    //Will most obviously be used for Serene Places to make animation transition smoother.
    public virtual void MoveToInteraction()
    {

    }

    public void StartInteraction()
    {

    }

    public void EndInteraction()
    {

    }
}
