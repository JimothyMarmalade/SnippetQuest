using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PicrossCross : _PicrossPuzzle
{
    private int[,] puzzleSolution = new int[5, 5]
    {
        {1, 0, 1, 0, 1 },
        {0, 1, 1, 1, 0 },
        {1, 1, 1, 1, 1 },
        {0, 1, 1, 1, 0 },
        {1, 0, 1, 0, 1 },
    };

    private int puzzleGridSize = 5;

    public string PuzzleName = "Snowflake";

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Start()
    {
        PuzzleTitle = PuzzleName;


        //On build, set the base gridSize and the puzzle solution
        SetGridSize(puzzleGridSize);
        SetPuzzleSolution(puzzleSolution);

        //Build the Picross Board
        BuildPicrossBoard();


    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public override void OnPuzzleSolved()
    {
        Debug.Log("OnPuzzleSolved ran in PicrossCross");
        GameObject.FindGameObjectWithTag("UIController").GetComponent<GLD_UIController>().SpawnCheckmark(1);
    }

}
