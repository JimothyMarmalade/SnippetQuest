/*
 * Created by Logan Edmund, 2/28/21
 * Last Modified by Logan Edmund, 5/17/21
 * 
 * Base script/methods that all interactable objects will inherit from.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactionPrompt;

    private bool showingInteractionPrompt = false;

    private void Start()
    {
        interactionPrompt.SetActive(showingInteractionPrompt);
    }

    public virtual void Interact()
    {
        Debug.Log("Called Interact on " + gameObject.name + ", interacting with base Interactble class.");
        //Perform interact actions here or in extended classes
    }

    //Handles moving the player to a specific spot before the interaction takes place.
    //Will most likely be used for Serene Places to make animation transition smoother.
    public virtual void MoveToInteraction()
    {

    }

    public void StartInteraction()
    {

    }

    public void EndInteraction()
    {

    }

    public void TriggerInteractionPrompt()
    {
        showingInteractionPrompt = true;
        interactionPrompt.SetActive(showingInteractionPrompt);
        StopAllCoroutines();
        StartCoroutine(InteractionPromptTimer());
    }

    private IEnumerator InteractionPromptTimer()
    {
        showingInteractionPrompt = false;

        yield return new WaitForSeconds(0.1f);

        interactionPrompt.SetActive(showingInteractionPrompt);
    }
}
