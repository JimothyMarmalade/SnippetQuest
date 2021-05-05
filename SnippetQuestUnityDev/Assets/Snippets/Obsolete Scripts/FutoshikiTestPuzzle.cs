using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FutoshikiTestPuzzle : _FutoshikiPuzzle
{
    [Header("Gridsize accounts for needed answer input space")]
    public int gridSizeFutoshikiTestPuzzle = 6;

    [Header("Puzzle Beginning Logic (Clues, preset spaces)")]
    private int[,] presetAnswerKeyFutoshikiTestPuzzle = new int[6, 6]
    {
        {0, 0, 6, 0, 0, 0},
        {3, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {6, 0, 0, 0, 0, 0},
    };

    private char[,] presetClueKeyFutoshikiTestPuzzle = new char[11, 6]
    {
          {'n','n','n','n','n','n'},
        {'n','n','n','n','n','n'},
          {'n','n','n','n','n','n'},
        {'n','n','n','n','n','n'},
          {'l','l','n','n','n','n'},
        {'n','n','n','n','n','n'},
          {'r','l','n','n','n','n'},
        {'d','d','n','n','d','d'},
          {'n','n','n','n','n','n'},
        {'n','u','n','n','u','n'},
          {'n','l','n','n','n','n'},
    };

    public void Start()
    {
        //Constructors for setting variables based on this class' values
        SetGridSize(gridSizeFutoshikiTestPuzzle);
        SetAnswerButtonsArray(gridSizeFutoshikiTestPuzzle);
        SetClueButtonsArray(gridSizeFutoshikiTestPuzzle);

        SetPresetAnswerKey(presetAnswerKeyFutoshikiTestPuzzle);
        SetPresetClueKey(presetClueKeyFutoshikiTestPuzzle);

        BuildFutoshikiBoard();
        AssignAllButtonsController(this);
    }

}