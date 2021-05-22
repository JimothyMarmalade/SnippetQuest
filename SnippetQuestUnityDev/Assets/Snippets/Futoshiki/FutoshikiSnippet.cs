/*
 * Created by Logan Edmund, 3/12/21
 * Last Modified by Logan Edmund, 5/20/21
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

[CreateAssetMenu(fileName = "New Futoshiki Snippet", menuName = "Snippets/Futoshiki")]
public class FutoshikiSnippet : Snippet
{
    [Header("Futoshiki Variables")]
    //Futoshikis are always a perfect square, meaning they only need one variable for gridSize
    public int gridSize;

    //currentSolution is a FEN-esque notation string used to hold the player's current in-progress solution.
    public string currentSolution;
    //Futoshiki SnippetSolutions are stored as a string of numbers from top left, going right, then repeating for each row.
    public string snippetSolution;

    //visibleClues and visibleAnswers are FEN-esque strings used to denote which lesserthan clues are visible at the start of the puzzle
    //and which answer boxes are pre-filled. 0 indicates it is not shown, 1 indicates it is shown.
    public string visibleAnswers;
    //Visible clues contains data for ALL clues used in the puzzle. The first half of the string tracks top/bottom clues, the back half tracks
    // left/right clues.
    public string visibleClues;


    public FutoshikiSnippet()
    {
        this.snippetSlug = "NULLSLUG";
        this.snippetType = SnippetType.Futoshiki;
        this.snippetName = "NO NAME";
        this.masterID = 0000;
        this.typeID = 000;
        this.snippetSolved = false;
        this.numTimesSolved = 0;
        this.bestTime = 0;

        this.gridSize = -10;
        this.currentSolution = "NO CURRENT SOLUTION";
        this.snippetSolution = "NO SNIPPET SOLUTION";
        this.visibleAnswers = "NO SET VISIBLE ANSWERS";
        this.visibleClues = "NO SET VISIBLE CLUES";
    }

    public override bool CheckCriticalInformation()
    {
        if (snippetType != Snippet.SnippetType.Futoshiki)
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
        if (visibleAnswers.Length != gridSize*gridSize)
        {
            Debug.LogError("FutoshikiSnippet " + snippetSlug + " has invalid visibleAnswers/gridSize!");
            return false;
        }
        if (visibleClues.Length != (2 * ((gridSize * gridSize) - gridSize)))
        {
            Debug.LogError("FutoshikiSnippet " + snippetSlug + " has invalid visibleClues/gridSize!");
            Debug.LogError("visibleClues.length = " + visibleClues.Length + ", should be " + (2 * ((gridSize * gridSize) - gridSize)));
            return false;
        }

        //No errors
        return true;
    }
}
