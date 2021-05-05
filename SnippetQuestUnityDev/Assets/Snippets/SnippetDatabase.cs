/*
 * Created by Logan Edmund, 4/21/21
 * Last Modified by Logan Edmund, 4/22/21
 * 
 * Function of SnippetDatabase.cs is to hold references/data for EVERY SNIPPET in the game. This does not apply to gameplay needs such as world location
 * and is mainly to serve as a way for the SnippetController to load/reference Snippet data.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SnippetDatabase : MonoBehaviour
{
    public static SnippetDatabase Instance { get; set; }
    public TextAsset MasterDataReference;
    public TextAsset PicrossDataReference;
    public TextAsset FutoshikiDataReference;
    public TextAsset CrosswordDataReference;
    private List<Snippet> AllSnippets { get; set; }
    private List<PicrossSnippet> PicrossSnippets { get; set; }
    private List<FutoshikiSnippet> FutoshikiSnippets { get; set; }

    private List<CrosswordSnippet> CrosswordSnippets { get; set; }

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
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        AllSnippets = JsonConvert.DeserializeObject<List<Snippet>>(MasterDataReference.ToString());

        PicrossSnippets = JsonConvert.DeserializeObject<List<PicrossSnippet>>(PicrossDataReference.ToString());

        FutoshikiSnippets = JsonConvert.DeserializeObject<List<FutoshikiSnippet>>(FutoshikiDataReference.ToString());

        CrosswordSnippets = JsonConvert.DeserializeObject<List<CrosswordSnippet>>(CrosswordDataReference.ToString());

        //Debug.Log(PicrossSnippets[0].snippetSolution);
    }

    public Snippet GetSnippet(string slug)
    {
        foreach (Snippet s in AllSnippets)
        {
            Debug.Log("Slug: " + s.snippetSlug);
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
            Debug.Log("masterID: " + s.masterID);
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
            Debug.Log("Slug: " + s.snippetSlug);
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
            Debug.Log("Slug: " + s.snippetSlug);
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
            Debug.Log("Slug: " + s.snippetSlug);
            if (s.snippetSlug == slug)
                return s;
        }
        Debug.LogError("Could not find CrosswordSnippet with slug: " + slug);
        return null;
    }
}
