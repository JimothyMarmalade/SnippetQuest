using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Last edited by Logan Edmund, 2/10/21

[CreateAssetMenu(fileName = "New Crossword", menuName = "Puzzles/CrosswordData")]
public class _CrosswordPuzzleData : ScriptableObject
{
    [Header("Puzzle Readability Data")]
    public string PuzzleName;
    public string PuzzleDescription;
    public int PuzzleID;

    [Header("Puzzle Answers/Clues Across")]
    public string[] WordsAcross;
    public string[] CluesAcross;
    public Vector2Int[] WordsAcrossLoc;
    
    [Header("Puzzle Answers/Clues Down")]
    public string[] WordsDown;
    public string[] CluesDown;
    public Vector2Int[] WordsDownLoc;


    [Header("Puzzle Build Data")]
    public int GridLength;


    public void CapitalizeWordArray(string[] arr)
    {
        int AWordNum = arr.Length;
        for (int s = 0; s < AWordNum; s++)
        {
            arr[s] = arr[s].ToUpper();
        }
    }




}
