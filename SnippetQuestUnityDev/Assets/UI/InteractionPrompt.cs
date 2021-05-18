/*
 * Created by Logan Edmund, 5/17/21
 * Last Modified by Logan Edmund, 5/17/21
 * 
 * A pop-up that appears above interactables when the player can interact with them.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    public enum InteractType { None, Talk, SitDown, Interact};

    public InteractType playerInteraction;

    public TMP_Text InteractionText;

    private void Start()
    {
        SetText();
    }

    public void SetText()
    {
        switch (playerInteraction)
        {
            case (InteractType.None):
                InteractionText.text = "NO INTERACTION SET";
                break;
            case (InteractType.Talk):
                InteractionText.text = "E - Talk";
                break;
            case (InteractType.SitDown):
                InteractionText.text = "E - Sit Down";
                break;
            case (InteractType.Interact):
                InteractionText.text = "E - Interact";
                break;
        }
    }


}
