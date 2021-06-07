/*
 * Created by Logan Edmund, 5/17/21
 * Last Modified by Logan Edmund, 5/17/21
 * 
 * UI Notification that is displayed when the player collects a new Snippet
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SnippetObtainedPopup : MonoBehaviour
{
    public TMP_Text displayText;
    public float SelfDestructTimer = 2.5f;

    public void Init(string snippetType)
    {
        displayText.text = "Collected new " + snippetType + " Snippet!";
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(SelfDestructTimer);

        Destroy(this.gameObject);
    }
}
