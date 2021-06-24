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
    public string Interaction;

    public TMP_Text InteractionText;

    private void Start()
    {
        SetText();
    }

    public void SetText()
    {
        InteractionText.text = "E - " + Interaction;
    }


}
