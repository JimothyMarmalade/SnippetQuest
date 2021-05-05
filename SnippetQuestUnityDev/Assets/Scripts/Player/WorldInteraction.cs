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

public class WorldInteraction : MonoBehaviour
{
    public List<string> acceptedTags = new List<string>();
    public bool canDoNewInteract;

    private void Start()
    {
        acceptedTags.Add("NPC");
        acceptedTags.Add("SerenePlace");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canDoNewInteract)
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
}
