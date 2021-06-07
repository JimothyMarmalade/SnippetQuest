/*
 * Created by Logan Edmund, 3/4/21
 * Last Modified by Logan Edmund, 5/20/21
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

[CreateAssetMenu(fileName = "New Picross Snippet", menuName = "Snippets/Picross")]
public class PicrossSnippet : Snippet
{
    [Header("Picross Variables")]

    //The horizontal and vertical grid size used by the board/solution
    public int horizontalGridSize;
    public int verticalGridSize;
    //currentSolution is a FEN-esque notation string used to hold the player's current in-progress solution.
    public string currentSolution;
    //Picross SnippetSolutions are stored as a single string of 0's and 1's, similar to FEN notation in chess
    public string snippetSolution;

    //---------- Constructor
    public PicrossSnippet()
    {
        this.snippetSlug = "NULLSLUG";
        this.snippetType = SnippetType.Picross;
        this.snippetName = "NO NAME";
        this.masterID = 0000;
        this.typeID = 000;
        this.snippetSolved = false;
        this.numTimesSolved = 0;
        this.bestTime = 0;

        this.currentSolution = "NO CURRENT SOLUTION";
        this.snippetSolution = "NO SNIPPET SOLUTION";
        this.horizontalGridSize = -10;
        this.verticalGridSize = -10;
    }


    public override bool CheckCriticalInformation()
    {
        if (snippetType != Snippet.SnippetType.Picross)
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
