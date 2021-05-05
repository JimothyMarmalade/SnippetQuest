/*
 * Created by Logan Edmund, 3/12/21
 * Last Modified by Logan Edmund, 4/21/21
 * 
 * Function of FutoshikiSnippet.cs is to hold data for each individual Futoshiki Snippet that can be inserted into puzzle boards.
 * This data and inheriting classes are NOT INTENDED for gameplay purposes such as if it has been collected by the player or position in the world.
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutoshikiSnippet : Snippet
{
    //currentSolution is a FEN-esque notation string used to hold the player's current in-progress solution.
    public string currentSolution;
    //Futoshiki SnippetSolutions are stored as a string of numbers from top left, going right, then repeating for each row.
    public string snippetSolution;
    //Futoshikis are always a perfect square, meaning they only need one variable for gridSize
    public int gridSize;

    public bool CheckCriticalInformation()
    {
        if (snippetType != "Futoshiki")
        {
            Debug.LogError("FutoshikiSnippet " + snippetSlug + " is not of type Futoshiki!");
            return false;
        }
        if (snippetSolution == null)
        {
            Debug.LogError("FutoshikiSnippet " + snippetSlug + " has no solution!");
            return false;
        }
        if (snippetSolution.Length != gridSize * gridSize)
        {
            Debug.LogError("FutoshikiSnippet " + snippetSlug + " has invalid solution/gridSize!");
            return false;
        }

        //No errors
        return true;
    }
}
