/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * Handles the obtaining of info from interactable objects in the world
 * Before creation of this script, this action was handled entirely within AdvancedThirdPersonMovement.cs
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteraction : PlayerAction
{
    public List<string> acceptedTags = new List<string>();
    public bool canDoNewInteract;

    private void Start()
    {
        acceptedTags.Add("NPC");
        acceptedTags.Add("SerenePlace");
        acceptedTags.Add("MinigameLoader");
    }


    // Update is called once per frame
    void Update()
    {
        //Cast the interaction ray in order to determine if something needs to display the interact prompt.
        if (canPerform)
            InteractionRay();

        if (Input.GetKeyDown(KeyCode.E) && canPerform)
            GetInteraction();
    }

    void GetInteraction()
    {
        Ray interactionRay = new Ray(transform.position, transform.forward);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, 3))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            Debug.Log("Interacting with " + interactionInfo.collider.gameObject.name);
            if (acceptedTags.Contains(interactedObject.tag))
            {
                if (interactedObject.tag == "NPC")
                {
                    interactedObject.gameObject.GetComponentInParent<Interactable>().Interact();
                }
                else
                {
                    interactedObject.GetComponent<Interactable>().Interact();
                }
            }
        }
    }

    //Ensures the interaction ray is always present and active
    void InteractionRay()
    {
        Ray interactionRay = new Ray(transform.position, transform.forward);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, 3))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.gameObject.GetComponentInParent<Interactable>() != null)
            {
                interactedObject.gameObject.GetComponentInParent<Interactable>().TriggerInteractionPrompt();
            }
            else if (interactedObject.GetComponent<Interactable>() != null)
            {
                interactedObject.GetComponent<Interactable>().TriggerInteractionPrompt();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called from " + gameObject.name);

        if (other.gameObject.GetComponent<FieldPickup>() != null)
        {
            Debug.Log("FieldPickup");
            other.GetComponent<FieldPickup>().CollectPickup();
        }
    }

}
