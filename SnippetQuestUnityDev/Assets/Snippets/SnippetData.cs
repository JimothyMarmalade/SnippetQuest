/*
 * Created by Logan Edmund, 5/20/21
 * Last Modified by Logan Edmund, 5/20/21
 * 
 * Used to save the player's snippet data between game boots. Done with a special data type "SnipInfo" which stores the relevant data.
 * 
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SnippetData
{
    //For each snippet, need to save the best time, isSolved, how many times completed, and the slug they're a part of.

    public SnipInfo[] AllSnippetInfo;


    public SnippetData (SnippetDatabase database)
    {
        AllSnippetInfo = new SnipInfo[database.AllSnippets.Count];
        int i = 0;

        foreach (Snippet s in database.AllSnippets)
        {
            AllSnippetInfo[i] = new SnipInfo(s.snippetSlug, s.snippetSolved, s.numTimesSolved, s.bestTime);
            i++;
        }
    }

}

//SnipInfo is a data structure that holds information about the snippets.
[System.Serializable]
public class SnipInfo
{
    public string snippetSlug;
    public bool isSolved;
    public int timesCompleted;
    public float bestTime;
    public string currentSolution;

    public SnipInfo(string s, bool b, int i, float f)
    {
        this.snippetSlug = s;
        this.isSolved = b;
        this.timesCompleted = i;
        this.bestTime = f;
    }
}