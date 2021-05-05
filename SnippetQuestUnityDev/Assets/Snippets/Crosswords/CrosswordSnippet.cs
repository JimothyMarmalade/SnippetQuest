using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Last edited by Logan Edmund, 3/30/21

public class CrosswordSnippet : Snippet 
{
    //currentSolution is a FEN-esque notation string used to hold the player's current in-progress solution.
    public string currentSolution;

    [Header("Puzzle Readability Data")]
    public string PuzzleDescription;

    //GridLength stores the puzzle's size. Crosswords are square.
    [Header("Puzzle Build Data")]
    public int GridLength;

    //WordsAcross holds all the horizontal words in the puzzle, and Clues holds all clues for those words.
    //WordsAcrossLoc holds all the starting locations of the words.
    [Header("Puzzle Answers/Clues Across")]
    public string[] WordsAcross;
    public string[] CluesAcross;
    public Vector2Int[] WordsAcrossLoc;

    //WordsDown holds all the horizontal words in the puzzle, and Clues holds all clues for those words.
    //WordsDownLoc holds all the starting locations of the words.
    [Header("Puzzle Answers/Clues Down")]
    public string[] WordsDown;
    public string[] CluesDown;
    public Vector2Int[] WordsDownLoc;


    public bool CheckCriticalInformation()
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
