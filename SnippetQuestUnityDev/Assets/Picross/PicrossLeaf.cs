using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PicrossLeaf : _PicrossPuzzle
{
    private int[,] puzzleSolution = new int[10, 10]
    {
        { 0 , 0 , 0 , 0 , 1 , 1 , 1 , 1 , 1 , 1 },
        { 0 , 0 , 0 , 1 , 0 , 1 , 0 , 1 , 0 , 1 },
        { 0 , 0 , 1 , 1 , 0 , 1 , 0 , 1 , 1 , 0 },
        { 0 , 1 , 0 , 1 , 0 , 1 , 1 , 0 , 1 , 0 },
        { 0 , 1 , 0 , 1 , 0 , 1 , 1 , 0 , 1 , 0 },
        { 0 , 1 , 0 , 1 , 1 , 0 , 0 , 0 , 1 , 0 },
        { 0 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 0 },
        { 0 , 0 , 1 , 0 , 0 , 0 , 0 , 1 , 0 , 0 },
        { 0 , 1 , 0 , 1 , 1 , 1 , 1 , 0 , 0 , 0 },
        { 0 , 1 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
    };

    private int puzzleGridSize = 10;

    public string PuzzleName = "Leaf";


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
        GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().SpawnCheckmark(2);
    }
}
