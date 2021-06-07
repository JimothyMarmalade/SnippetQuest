/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 5/20/21
 * 
 * Function of SnippetDatabase.cs is to hold references/data for EVERY SNIPPET in the game. This does not apply to gameplay needs such as world location
 * and is mainly to serve as a way for the SnippetController to load/reference Snippet data.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SnippetDatabase : MonoBehaviour
{
    public static SnippetDatabase Instance;

    public List<Snippet> AllSnippets;

    public List<PicrossSnippet> PicrossSnippets;

    public List<FutoshikiSnippet> FutoshikiSnippets;

    public List<CrosswordSnippet> CrosswordSnippets;

    public bool DatabaseBuilt = false;

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

        BuildDatabase();
    }

    public void BuildDatabase()
    {
        Debug.Log("Running BuildDatabase()...");
        //Only allow the database to be built once during runtime
        if (!DatabaseBuilt)
        {
            //Load Snippet Data from file and modify snippets accordingly
            foreach (Snippet s in AllSnippets)
            {
                if (!s.CheckCriticalInformation())
                    Debug.LogWarning("Critical Information Check returned false for Snippet " + s.snippetSlug);

                s.ResetPlayerInformation();
                switch (s.snippetType)
                {
                    case Snippet.SnippetType.Picross:
                        PicrossSnippets.Add(s as PicrossSnippet);
                        break;

                    case Snippet.SnippetType.Futoshiki:
                        FutoshikiSnippets.Add(s as FutoshikiSnippet);
                        break;

                    case Snippet.SnippetType.Crossword:
                        CrosswordSnippets.Add(s as CrosswordSnippet);
                        break;
                }
            }
            DatabaseBuilt = true;
        }
        else
        {
            Debug.LogWarning("SnippetDatabase was not built because it has already been built.");
        }
    }


    public void SaveSnippetInfo()
    {
        Debug.Log("Saving snippet information...");
        SaveSystem.SaveSnippetData(this);
    }

    public void LoadSnippetInfo()
    {
        Debug.Log("Loading Snippet Information...");
        SnippetData data = SaveSystem.LoadSnippetData();

        if (data != null)
        {
            foreach (SnipInfo info in data.AllSnippetInfo)
            {
                foreach (Snippet s in AllSnippets)
                {
                    if (s.snippetSlug == info.snippetSlug)
                    {
                        s.snippetSolved = info.isSolved;
                        s.bestTime = info.bestTime;
                        s.numTimesSolved = info.timesCompleted;
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("No Saved SnippetData found. Creating new save data...");
            SaveSnippetInfo();
        }
    }

    public Snippet GetSnippet(string slug)
    {
        foreach (Snippet s in AllSnippets)
        {
           // Debug.Log("Slug: " + s.snippetSlug);
            if (s.snippetSlug == slug)
                return s;
        }
        Debug.LogError("Could not find snippet with slug: " + slug);
        return null;
    }

    public Snippet GetSnippet(int masterID)
    {
        foreach (Snippet s in AllSnippets)
        {
            //Debug.Log("masterID: " + s.masterID);
            if (s.masterID == masterID)
                return s;
        }
        Debug.LogError("Could not find snippet with masterID: " + masterID);
        return null;
    }

    public PicrossSnippet GetPicrossSnippet(string slug)
    {
        foreach (PicrossSnippet s in PicrossSnippets)
        {
            //Debug.Log("Slug: " + s.snippetSlug);
            if (s.snippetSlug == slug)
                return s;
        }
        Debug.LogError("Could not find PicrossSnippet with slug: " + slug);
        return null;
    }

    public FutoshikiSnippet GetFutoshikiSnippet(string slug)
    {
        foreach (FutoshikiSnippet s in FutoshikiSnippets)
        {
            //Debug.Log("Slug: " + s.snippetSlug);
            if (s.snippetSlug == slug)
                return s;
        }
        Debug.LogError("Could not find FutoshikiSnippet with slug: " + slug);
        return null;
    }

    public CrosswordSnippet GetCrosswordSnippet(string slug)
    {
        foreach (CrosswordSnippet s in CrosswordSnippets)
        {
            //Debug.Log("Slug: " + s.snippetSlug);
            if (s.snippetSlug == slug)
                return s;
        }
        Debug.LogError("Could not find CrosswordSnippet with slug: " + slug);
        return null;
    }
}
