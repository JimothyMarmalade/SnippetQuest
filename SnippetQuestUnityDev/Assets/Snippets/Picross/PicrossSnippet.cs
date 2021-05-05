/*
 * Created by Logan Edmund, 3/4/21
 * Last Modified by Logan Edmund, 3/4/21
 * 
 * Function of PicrossSnippet.cs is to hold data for each individual picross Snippet that can be inserted into puzzle boards.
 * This data and inheriting classes are NOT INTENDED for gameplay purposes such as if it has been collected by the player or position in the world.
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicrossSnippet : Snippet
{
    //currentSolution is a FEN-esque notation string used to hold the player's current in-progress solution.
    public string currentSolution { get; set; }
    //Picross SnippetSolutions are stored as a single string of 0's and 1's, similar to FEN notation in chess
    public string snippetSolution { get; set; }
    //The horizontal and vertical grid size used by the board/solution
    public int horizontalGridSize { get; set; }
    public int verticalGridSize { get; set; }

    //----------
    [Newtonsoft.Json.JsonConstructor]
    public PicrossSnippet(string snippetSlug, string snippetType, string snippetName, int masterID,
                   int typeID, bool snippetSolved, int numTimesSolved, float bestTime,
                   
                   string currentSolution, string snippetSolution, int horizontalGridSize, int verticalGridSize)
    {
        this.snippetSlug = snippetSlug;
        this.snippetType = snippetType;
        this.snippetName = snippetName;
        this.masterID = masterID;
        this.typeID = typeID;
        this.snippetSolved = snippetSolved;
        this.numTimesSolved = numTimesSolved;
        this.bestTime = bestTime;

        this.currentSolution = currentSolution;
        this.snippetSolution = snippetSolution;
        this.horizontalGridSize = horizontalGridSize;
        this.verticalGridSize = verticalGridSize;
    }


    public bool CheckCriticalInformation()
    {
        if (snippetType != "Picross")
        {
            Debug.LogError("PicrossSnippet " + snippetSlug + " is not of type Picross!");
            return false;
        }
        if (snippetSolution == null)
        {
            Debug.LogError("PicrossSnippet " + snippetSlug + " has no solution!");
            return false;
        }
        if (horizontalGridSize <= 0 || verticalGridSize <= 0)
        {
            Debug.LogError("PicrossSnippet " + snippetSlug + " has invalid grid dimensions!");
            return false;
        }
        if (snippetSolution.Length != horizontalGridSize * verticalGridSize)
        {
            Debug.LogError("PicrossSnippet " + snippetSlug + " has invalid solution!");
            return false;
        }

        //No errors
        return true;
    }

    public void SaveCurrentSolution()
    {

    }

}
