/*
 * Created by Logan Edmund, 4/22/21
 * Last Modified by Logan Edmund, 4/22/21
 * 
 * Holds all snippet-related backend functionality -- OnSnippetSolved, OnSnippetCollected, etc..
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnippetEvents : MonoBehaviour
{
    public static SnippetEvents Instance { get; set; }

    //Create a new type of delegate event, SnippetEvent, that is passed in a snippetSlug
    public delegate void SnippetEvent(string snippetSlug);
    public static event SnippetEvent OnSnippetCompleted;
    public static event SnippetEvent OnSnippetSolved;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null && Instance != this)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    //SnippetSolved runs when a snippet puzzle is completed successfully FOR THE FIRST TIME
    public void SnippetSolved(string snippetSlug)
    {
        Debug.Log("SnippetEvents: Running SnippetSolved");
        //Do Whatever

        if (OnSnippetSolved != null)
        {
            OnSnippetSolved(snippetSlug);
        }
        else
        {
            Debug.Log("OnSnippetSolved event listener had no active events and did not run");
        }
    }

    //SnippetCompleted runs when a snippet puzzle is completed successfully any time other than the first time
    public void SnippetCompleted(string snippetSlug)
    {
        Debug.Log("SnippetEvents: Running SnippetCompleted");
        //Do Whatever


        if (OnSnippetCompleted != null)
        {
            OnSnippetCompleted(snippetSlug);
        }
        else
        {
            Debug.Log("OnSnippetCompleted event listener had no active events and did not run");
        }
    }


}
