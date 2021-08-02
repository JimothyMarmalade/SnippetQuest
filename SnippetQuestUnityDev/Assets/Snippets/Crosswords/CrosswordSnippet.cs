/*
 * Created by Logan Edmund, 3/12/21
 * Last Modified by Logan Edmund, 5/20/21
 * 
 * Function of CrosswordSnippet.cs is to hold data for each individual Crossword Snippet that can be inserted into puzzle boards.
 * This data and inheriting classes are NOT INTENDED for gameplay purposes such as if it has been collected by the player or position in the world.
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Crossword Snippet", menuName = "Snippets/Crossword")]
public class CrosswordSnippet : Snippet 
{
    [Header("Crossword Readability Data")]
    //currentSolution is a FEN-esque notation string used to hold the player's current in-progress solution.
    public string currentSolution;
    //Description is a short blurb about what the crossword is themed around
    public string PuzzleDescription;

    //GridLength stores the puzzle's size. Crosswords are square.
    [Header("Crossword Build Data")]
    public int gridSizeHorizontal;
    public int gridSizeVertical;

    //WordsAcross holds all the horizontal words in the puzzle, and Clues holds all clues for those words.
    //WordsAcrossLoc holds all the starting locations of the words.
    [Header("Crossword Answers/Clues Across")]
    public CrosswordWord[] HorizontalWords;

    //WordsDown holds all the horizontal words in the puzzle, and Clues holds all clues for those words.
    //WordsDownLoc holds all the starting locations of the words.
    [Header("Crossword Answers/Clues Down")]
    public CrosswordWord[] VerticalWords;

    public CrosswordSnippet()
    {
        this.snippetSlug = "NULLSLUG";
        this.snippetType = SnippetType.Crossword;
        this.snippetName = "NO NAME";
        this.masterID = 0000;
        this.typeID = 000;
        this.snippetSolved = false;
        this.numTimesSolved = 0;
        this.bestTime = 0;

        this.currentSolution = "NO CURRENT SOLUTION";
        this.PuzzleDescription = "NO DESCRIPTION";
        this.gridSizeHorizontal = -10;
        this.gridSizeVertical = -10;

    }


    public override bool CheckCriticalInformation()
    {
        return true;
    }

    public void CapitalizeWordArray(string[] arr)
    {
        int AWordNum = arr.Length;
        for (int s = 0; s < AWordNum; s++)
        {
            arr[s] = arr[s].ToUpper();
        }
    }




}
