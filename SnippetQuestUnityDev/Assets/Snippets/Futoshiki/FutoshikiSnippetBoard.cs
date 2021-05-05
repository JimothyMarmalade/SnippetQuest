/*
 * Created by Logan Edmund, 3/12/21
 * Last Modified by Logan Edmund, 5/4/21
 * 
 * Used to generate UI Futoshiki boards on the fly when fed SnippetData of type Futoshiki. Holds all methods needed to handle gameplay and 
 * data modification/updating.
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FutoshikiSnippetBoard : MonoBehaviour
{
    [Header("Button/Display Prefabs")]
    public Button answerButtonPrefab;
    public Button clueButtonPrefab;
    public RectTransform gridPanel;

    [Header("Futoshiki Data")]
    public FutoshikiSnippet futoshikiPuzzleData;

    [Header("Size Data and Button Storage")]
    private _FutoshikiAnswerButton[] answerButtons;
    private List<GameObject> clueButtons;

    private int numDividers;
    private float answerButtonWidth;
    private float clueButtonWidth;
    private float clueButtonHeight;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Ensures all necessary data is accurate, then runs BuildPicrossBoard().
    public bool TryBuildFutoshikiBoard(FutoshikiSnippet s)
    {
        futoshikiPuzzleData = null;
        //Check to ensure the inserted Picross Snippet has all necessary information for the build, then loads the puzzle from data.
        if (s.CheckCriticalInformation())
        {
            futoshikiPuzzleData = s;
            BuildFutoshikiBoard();
            Debug.Log(s.snippetSlug + " Futoshiki Board Build completed successfully");
            return true;
        }
        else
        {
            Debug.LogError(s.snippetSlug + " failed CheckCriticalInformation! Terminating build attempt.");
            return false;
        }
    }
    //BuildFutoshikiBoard runs the methods, in order, that govern each aspect of board generation.
    private void BuildFutoshikiBoard()
    {
        SetSizes();
        InstantiateAnswers();
        InstantiateClues();
        HideCluesAndAnswers();
    }

    //SetSizes sets the size for answer buttons and clue buttons
    private void SetSizes()
    {
        int gridSize = futoshikiPuzzleData.gridSize;
        //Establish how many invisible "dividers" exist on the game board for construction purposes.
        //Dividers should always be size of grid (i.e. number count) times 2, plus 1
        numDividers = (gridSize * 2) + 1;

        //Determine the size of the answer buttons and the gaps between them. Answer button lengths combined are 75% of the board.
        answerButtonWidth = (gridPanel.sizeDelta.x * 0.75f) / gridSize;
        clueButtonWidth = (gridPanel.sizeDelta.x * 0.25f) / (gridSize + 1);

        //The width of the cluebuttons and gaps accounts for 25% of the total gameboard Length. Height is used for ensuring
        //The size of the text is appropriate and based off answer button size. 
        clueButtonHeight = (answerButtonWidth * 0.75f);

        answerButtons = new _FutoshikiAnswerButton[gridSize * gridSize];
        clueButtons = new List<GameObject>();
    }

    //InstantiateAnswers will generate all needed answer buttons based on data input and position them on the board.
    private void InstantiateAnswers()
    {
        for (int i = 0; i < futoshikiPuzzleData.snippetSolution.Length; i++)
        {
            //Instantiate the button
            Button b = Instantiate(answerButtonPrefab);
            _FutoshikiAnswerButton bLogic = b.GetComponent<_FutoshikiAnswerButton>();

            //Set the button's parent to the gameboard
            b.transform.SetParent(gridPanel.transform, false);

            //Set the size and position of the button
            b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(answerButtonWidth, answerButtonWidth);

            float xOffset = (i % futoshikiPuzzleData.gridSize) * (clueButtonWidth + answerButtonWidth);
            float yOffset = (i / futoshikiPuzzleData.gridSize) * (clueButtonWidth + answerButtonWidth);

            b.transform.localPosition = new Vector2((-gridPanel.sizeDelta.x /2 + clueButtonWidth + answerButtonWidth/2 + xOffset), 
                                                    (gridPanel.sizeDelta.y /2 - clueButtonWidth - answerButtonWidth/2 - yOffset));

            bLogic.SetPuzzleControllerReference(this, futoshikiPuzzleData.gridSize);
            answerButtons[i] = bLogic;
        }
    }

    //InstantiateClues will generate all needed clues based on data input and position them on the board.
    private void InstantiateClues()
    {
        //Begin with the horizontal clues -- those that are concerned with the answers directly above and below them.
        //For each clue, spawn the button in the desired location and rotate it +90 or -90 based on which answer should be higher/lower.
        for (int i = 0; i < futoshikiPuzzleData.gridSize*(futoshikiPuzzleData.gridSize-1); i++)
        {
            //Instantiate the Button
            Button b = Instantiate(clueButtonPrefab);
            //Set the button's parent to the gameboard
            b.transform.SetParent(gridPanel.transform, false);
            //Set the size and position of the button
            b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(clueButtonWidth, clueButtonWidth);

            //Position the button
            float xOffset = (i % futoshikiPuzzleData.gridSize) * (clueButtonWidth + answerButtonWidth);
            float yOffset = (i / futoshikiPuzzleData.gridSize) * (clueButtonWidth + answerButtonWidth);

            b.transform.localPosition = new Vector2((-gridPanel.sizeDelta.x/2 + clueButtonWidth + answerButtonWidth/2 + xOffset),
                                                     (gridPanel.sizeDelta.y / 2 - clueButtonWidth*1.5f - answerButtonWidth - yOffset));

            //Determine if the clue should be rotated or not. 90 if the item above is less, -90 if the item below is less.
            int c = futoshikiPuzzleData.snippetSolution[i]; //Top
            int d = futoshikiPuzzleData.snippetSolution[i+futoshikiPuzzleData.gridSize]; //Bottom

            if (c < d)
                b.transform.Rotate(new Vector3(0f, 0f, 90f));
            else
                b.transform.Rotate(new Vector3(0f, 0f, -90f));

            clueButtons.Add(b.gameObject);
        }
        //Repeat with vertical clues, comparing values to their right and left.
        int pointer = 0;
        for (int i = 0; i < futoshikiPuzzleData.gridSize * (futoshikiPuzzleData.gridSize - 1); i++)
        {
            //Instantiate the Button
            Button b = Instantiate(clueButtonPrefab);
            //Set the button's parent to the gameboard
            b.transform.SetParent(gridPanel.transform, false);
            //Set the size and position of the button
            b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(clueButtonWidth, clueButtonWidth);

            //Position the button
            float xOffset = (i % (futoshikiPuzzleData.gridSize-1)) * (clueButtonWidth + answerButtonWidth);
            float yOffset = (i / (futoshikiPuzzleData.gridSize-1)) * (clueButtonWidth + answerButtonWidth);

            b.transform.localPosition = new Vector2((-gridPanel.sizeDelta.x / 2 + clueButtonWidth*1.5f + answerButtonWidth + xOffset),
                                                    (gridPanel.sizeDelta.y / 2 - clueButtonWidth - answerButtonWidth / 2 - yOffset));

            //Determine if the clue should be rotated or not. 90 if the item above is less, -90 if the item below is less.
            //Debug.Log("pointer% gridSize-1 =" + (pointer % (futoshikiPuzzleData.gridSize-1)));

            if (i % (futoshikiPuzzleData.gridSize-1) == 0 && pointer != 0)
                pointer++;
            //Debug.Log("i=" + i + ", pointer=" + pointer);
            int c = futoshikiPuzzleData.snippetSolution[pointer]; //Left
            int d = futoshikiPuzzleData.snippetSolution[pointer+1]; //Right

            if (c < d)
                b.transform.Rotate(new Vector3(0f, 0f, 180f));

            pointer++;
            clueButtons.Add(b.gameObject);
        }
    }

    //HideCluesAndAnswers will reduce the number of visible clues and answers available on the board so the puzzle isn't immediately solvable
    private void HideCluesAndAnswers()
    {

    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Quick check compares the input solution to the snippet's coded solution and checks for equality. Futoshikis can only ever have one solution. 
    public void CheckWinCondition()
    {
        string temp = "";
        foreach (_FutoshikiAnswerButton b in answerButtons)
        {
            temp += b.currentVal.ToString();
        }
        if (temp.Equals(futoshikiPuzzleData.snippetSolution))
            Debug.Log("Correct Answer input!");
    }

    public void EndGame()
    {

    }

    //UnloadSnippet unloads all the buttons from the gameplay panel so they do not use resources. 
    public void UnloadSnippet()
    {
        foreach (_FutoshikiAnswerButton b in answerButtons)
            Destroy(b.gameObject);
        foreach (GameObject b in clueButtons)
            Destroy(b);
    }

    


}
