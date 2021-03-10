/*
 * Created by Logan Edmund, 3/8/21
 * Last Modified by Logan Edmund, 3/9/21
 * 
 * Used to generate UI picross boards on the fly when fed SnippetData of type Picross. Holds all methods needed to handle gameplay and 
 * data modification/updating.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PicrossSnippetBoard : MonoBehaviour
{
    [Header("Button/Display Prefabs and References")]
    public Button answerButtonPrefab;
    public Button clueButtonPrefab;
    public RectTransform gridPanel;
    public TMP_Text PuzzleHeader;

    [Header("Picross Data")]
    public PicrossSnippet picrossPuzzleData;

    [Header("Size Data and Button Storage")]
    float gridWidth;
    float gridHeight;
    float gapWidth;
    float gapHeight;
    float answerButtonWidth;
    float answerButtonHeight;
    float clueButtonWidthHoriz;
    float clueButtonHeightHoriz;
    float clueButtonWidthVert;
    float clueButtonHeightVert;

    private _PicrossAnswerButton[] answerButtons;
    private _PicrossClueButton[] clueButtonsHorizontal;
    private _PicrossClueButton[] clueButtonsVertical;
    int maxNumHorizontalClues;
    int maxNumVerticalClues;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //Builds the buttons needed for the puzzle based on the GridSize
    public bool TryBuildPicrossBoard(PicrossSnippet s)
    {
        picrossPuzzleData = null;
        //Check to ensure the inserted Picross Snippet has all necessary information for the build, then loads the puzzle from data.
        if (s.CheckCriticalInformation())
        {
            picrossPuzzleData = s;
            BuildPicrossBoard();
            return true;
        }
        else
        {
            Debug.LogError(s.name + " failed CheckCriticalInformation! Terminating build attempt.");
            return false;
        }
    }

    //BuildPicrossBoard runs the methods, in order, that govern each aspect of board generation.
    private void BuildPicrossBoard()
    {
        SetSizes();
        InstantiateAnswers();
        InstantiateClues();
        FillCluesHorizontal();
        FillCluesVertical();
    }

    //SetSizes() calculates the size of the input grid, input buttons, input gaps, and clue buttons.
    private void SetSizes()
    {
        //Calculate the size of the input grid relative to the Overall panel and position it inside the panel.
        Vector2 picrossBoardDimensions = this.GetComponent<RectTransform>().sizeDelta;
        gridPanel.sizeDelta = new Vector2(picrossBoardDimensions.x * 0.80f, picrossBoardDimensions.y * 0.80f);
        gridPanel.localPosition = new Vector2((picrossBoardDimensions.x * 0.1f), (picrossBoardDimensions.y * -0.1f));

        //Calculate the Size of the input Buttons and gaps between them based on input grid size
        gridWidth  = gridPanel.sizeDelta.x;
        gridHeight = gridPanel.sizeDelta.y;
        gapWidth   = gridWidth * 0.01f;
        gapHeight  = gridHeight * 0.01f;

        //ABW and ABH = Percentage of board taken up by the answer buttons divided by number of answers in a row/column.
        answerButtonWidth  = (-0.01f * gridWidth) + ((0.99f * gridWidth) / picrossPuzzleData.horizontalGridSize);
        answerButtonHeight = (-0.01f * gridHeight) + ((0.99f * gridHeight) / picrossPuzzleData.verticalGridSize);

        //Calculate the size of the Clue Buttons and the gaps between them based on size of Picross
        //Horizontal (left-side) clues
        if (picrossPuzzleData.horizontalGridSize % 2 == 0)
            maxNumHorizontalClues = picrossPuzzleData.horizontalGridSize / 2;
        else
            maxNumHorizontalClues = (picrossPuzzleData.horizontalGridSize + 1) / 2;

        //Clues will use the remainer of the board space available in the margins
        clueButtonWidthHoriz = (picrossBoardDimensions.x * 0.20f) / maxNumHorizontalClues;
        clueButtonHeightHoriz = (picrossBoardDimensions.y * 0.80f) / picrossPuzzleData.verticalGridSize;


        //Vertical (top-side) clues
        if (picrossPuzzleData.verticalGridSize % 2 == 0)
            maxNumVerticalClues = picrossPuzzleData.verticalGridSize / 2;
        else
            maxNumVerticalClues = (picrossPuzzleData.verticalGridSize + 1) / 2;

        clueButtonWidthVert = (picrossBoardDimensions.x * 0.80f) / picrossPuzzleData.horizontalGridSize;
        clueButtonHeightVert = (picrossBoardDimensions.y * 0.20f) / maxNumVerticalClues;
    }

    //InstantiateAnswerButtons() generates each answer button needed for the puzzle, places them on the board, and holds references to them in an array.
    private void InstantiateAnswers()
    {
        answerButtons = new _PicrossAnswerButton[picrossPuzzleData.snippetSolution.Length];

        for (int i = 0; i < picrossPuzzleData.snippetSolution.Length; i++)
        {
            Button b = Instantiate(answerButtonPrefab);
            b.transform.SetParent(gridPanel.transform, false);
            //Set the size of the button
            b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(answerButtonWidth, answerButtonHeight);

            //Set the position. Move the button to the starting position (space 1-1)
            //Space 1-1 = (-gridSize.x/2 + gapWidth + answerButtonWidth/2), (gridSize.y/2 - gapHeight -answerbuttonHeight/2)
            //Then Increment X by the column the button should be in. (i % data.horizGridSize).
            //Also Increment Y by the row it should be in.
            float xOffset = (i % picrossPuzzleData.horizontalGridSize) * (gapWidth + answerButtonWidth);
            float yOffset = (i / picrossPuzzleData.horizontalGridSize) * (gapWidth + answerButtonHeight);
            b.transform.localPosition = new Vector2((-gridWidth / 2 + gapWidth  + answerButtonWidth  / 2) + xOffset, 
                                                    (gridHeight / 2 - gapHeight - answerButtonHeight / 2) - yOffset);

            //Assign the answerbutton's reference to this and add it to the array
            _PicrossAnswerButton bLogic = b.GetComponent<_PicrossAnswerButton>();
            answerButtons[i] = bLogic;
            bLogic.SetPuzzleControllerReference(this);
        }
    }

    //InstantiateClues() generates all clues needed to solve the puzzle, places them on the board, and holds references to each in separate arrays.
    //This method DOES NOT fill in the clues with information, it only instantiates and positions them.
    private void InstantiateClues()
    {
        //Instantiate both Clue button arrays. Empty clue placement needs to be done before assigning clue values.
        clueButtonsHorizontal = new _PicrossClueButton[(maxNumHorizontalClues * picrossPuzzleData.verticalGridSize)];
        clueButtonsVertical = new _PicrossClueButton[(maxNumVerticalClues * picrossPuzzleData.horizontalGridSize)];

        //Instantiate and place Horizontal Clues
        for (int i = 0; i < clueButtonsHorizontal.Length; i++)
        {
            Button b = Instantiate(clueButtonPrefab);
            b.transform.SetParent(gridPanel.transform, false);
            //Set the size of the button
            b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(clueButtonWidthHoriz, clueButtonHeightHoriz);

            //Set the position. Move the button to the starting position (space 1-1)
            //Space 1-1 = (-gridWidth/2 - cluebuttonHorizWidth/2 - (clueButtonHorizWidth * maxHorizClues-1), (gridSize.y/2 - gapHeight -answerbuttonHeight/2)
            //Then Increment X by the column the button should be in. (i % data.horizGridSize).
            //Also Increment Y by the row it should be in.
            float xOffset = (i % maxNumHorizontalClues) * (clueButtonWidthHoriz);
            float yOffset = (i / maxNumHorizontalClues) * (clueButtonHeightHoriz);
            b.transform.localPosition = new Vector2((-gridWidth / 2 - clueButtonWidthHoriz/2 - (clueButtonWidthHoriz*(maxNumHorizontalClues-1))) + xOffset,
                                                    (gridHeight / 2 - clueButtonHeightHoriz/2) - yOffset);

            //Assign the answerbutton's reference to this and add it to the array
            _PicrossClueButton bLogic = b.GetComponent<_PicrossClueButton>();
            clueButtonsHorizontal[i] = bLogic;
        }

        //Instantiate and place Vertical Clues
        for (int i = 0; i < clueButtonsVertical.Length; i++)
        {
            Button b = Instantiate(clueButtonPrefab);
            b.transform.SetParent(gridPanel.transform, false);
            //Set the size of the button
            b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(clueButtonWidthVert, clueButtonHeightVert);

            //Set the position. Move the button to the starting position (space 1-1)
            //Space 1-1 = (-gridWidth/2 - cluebuttonHorizWidth/2 - (clueButtonHorizWidth * maxHorizClues-1), (gridSize.y/2 - gapHeight -answerbuttonHeight/2)
            //Then Increment X by the column the button should be in. (i % data.horizGridSize).
            //Also Increment Y by the row it should be in.
            float xOffset = (i / maxNumVerticalClues) * (clueButtonWidthVert);
            float yOffset = (i % maxNumVerticalClues) * (clueButtonHeightVert);
            b.transform.localPosition = new Vector2((-gridWidth / 2 + clueButtonWidthVert/2) + xOffset,
                                                    (gridHeight / 2 + clueButtonHeightVert/2 + (clueButtonHeightVert*(maxNumVerticalClues-1)) - yOffset));

            //Assign the answerbutton's reference to this and add it to the array
            _PicrossClueButton bLogic = b.GetComponent<_PicrossClueButton>();
            clueButtonsVertical[i] = bLogic;
        }

    }

    //FillCluesHorizontal() and Vertical() determine which clues are needed for the puzzle, which are useless, and when to fill one in with a number clue.
    private void FillCluesHorizontal()
    {
        //Starting out, we know the maximum amount of clues that can exist for any given puzzle based off of the gridSize,
        //And all the buttons have already been built. So we need to determine what goes in each button.

        //Start with horizontal (left-most) clues
        for (int i = 0; i < picrossPuzzleData.verticalGridSize; i++)
        {
            //First we obtain the stretch of the snippetSolution used to generate the clues.
            string answerSubstring = picrossPuzzleData.snippetSolution.Substring((i * picrossPuzzleData.horizontalGridSize), picrossPuzzleData.horizontalGridSize);

            //From this substring, we derive a list of answers for the column (For example, if the substring is 10110, the answer list is 1,2)
            List<int> clueStorage = new List<int>();
            int currentClue = 0;            
            for (int j = 0; j < answerSubstring.Length; j++)
            {
                //If space should be filled
                if (answerSubstring[j].Equals('1'))
                {
                    //Increment the current clue size and continue
                    currentClue++;
                    //if this is the final button in the row, add it to the list
                    if (j == answerSubstring.Length - 1)
                        clueStorage.Add(currentClue);
                }
                else if (!answerSubstring[j].Equals('1'))
                {
                    //if the next item IS NOT meant to be filled, we add the counter to the workingList (if !=0), reset the value, then continue.
                    if (currentClue != 0)
                        clueStorage.Add(currentClue);
                    currentClue = 0;
                }
            }
            //With the compiled list of answers, we can work backwards through the list and fill the buttons.
            for (int k = maxNumHorizontalClues-1; k >= 0; k--)
            {
                //if there are no clues at all for the row/column, then set the first clue to 0
                if (k == maxNumHorizontalClues - 1 && clueStorage.Count <= 0)
                {
                    //Write the value 0 to the button
                    clueButtonsHorizontal[i*maxNumHorizontalClues + k].GetComponent<_PicrossClueButton>().SetButtonText(0);
                }
                //If there are still values in clueStorage
                else if (clueStorage.Count > 0)
                {
                    //Grab the value
                    int clue = clueStorage[clueStorage.Count - 1];
                    //Delete the value from the list
                    clueStorage.RemoveAt(clueStorage.Count - 1);
                    //Write the value to the button
                    clueButtonsHorizontal[i * maxNumHorizontalClues + k].GetComponent<_PicrossClueButton>().SetButtonText(clue);
                }
                //If there are no more clues, then the remaining buttons need to be destroyed
                else
                {
                    Destroy(clueButtonsHorizontal[i * maxNumHorizontalClues + k].gameObject);
                    clueButtonsHorizontal[i * maxNumHorizontalClues + k] = null;
                }
            }
        }
    }

    private void FillCluesVertical()
    {
        //Now to the same for Vertical (top-most) clues
        for (int i = 0; i < picrossPuzzleData.horizontalGridSize; i++)
        {
            //First we obtain the stretch of the snippetSolution used to generate the clues. Due to the solution being stored in a string,
            //doing this will require "creativity."
            string answerSubstring = "";
            for (int a = 0; a < picrossPuzzleData.verticalGridSize; a++)
            {
                answerSubstring += picrossPuzzleData.snippetSolution[i + a*picrossPuzzleData.verticalGridSize];
            }
            //Debug.Log("answerSubstring for run " + i + ": " + answerSubstring);


            //From this substring, we derive a list of answers for the column (For example, if the substring is 10110, the answer list is 1,2)
            List<int> clueStorage = new List<int>();
            int currentClue = 0;
            for (int j = 0; j < answerSubstring.Length; j++)
            {
                //If space should be filled
                if (answerSubstring[j].Equals('1'))
                {
                    //Increment the current clue size and continue
                    currentClue++;
                    //if this is the final button in the row, add it to the list
                    if (j == answerSubstring.Length - 1)
                        clueStorage.Add(currentClue);
                }
                else if (!answerSubstring[j].Equals('1'))
                {
                    //if the next item IS NOT meant to be filled, we add the counter to the workingList (if !=0), reset the value, then continue.
                    if (currentClue != 0)
                        clueStorage.Add(currentClue);
                    currentClue = 0;
                }
            }


            //With the compiled list of answers, we can work backwards through the list and fill the buttons.
            for (int k = maxNumVerticalClues - 1; k >= 0; k--)
            {
                //if there are no clues at all for the row/column, then set the first clue to 0
                if (k == maxNumVerticalClues - 1 && clueStorage.Count <= 0)
                {
                    //Write the value 0 to the button
                    clueButtonsVertical[i * maxNumVerticalClues + k].GetComponent<_PicrossClueButton>().SetButtonText(0);
                }
                //If there are still values in clueStorage
                else if (clueStorage.Count > 0)
                {
                    //Grab the value
                    int clue = clueStorage[clueStorage.Count - 1];
                    //Delete the value from the list
                    clueStorage.RemoveAt(clueStorage.Count - 1);
                    //Write the value to the button
                    clueButtonsVertical[i * maxNumVerticalClues + k].GetComponent<_PicrossClueButton>().SetButtonText(clue);
                }
                //If there are no more clues, then the remaining buttons need to be destroyed
                else
                {
                    Destroy(clueButtonsVertical[i * maxNumVerticalClues + k].gameObject);
                    clueButtonsVertical[i * maxNumVerticalClues + k] = null;
                }
            }
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //Compares the current solution to the correct solution and ends the game if accurate.
    public void CheckWinCondition()
    {
        //Rebuild the CurrentSolution stored in the data object
        string temp = "";
        foreach (_PicrossAnswerButton b in answerButtons)
        {
            temp += b.currentValue;
        }

        //If the current solution is sufficiently close to the answer (all 1's filled, all 0's not filled) then the puzzle is solved
        bool solved = true;
        for (int i = 0; i < temp.Length; i++)
        {
            if (picrossPuzzleData.snippetSolution[i] == '1' && temp[i] != '1')
            {
                solved = false;
                break;
            }

            if (picrossPuzzleData.snippetSolution[i] == '0' && temp[i] == '1')
            {
                solved = false;
                break;
            }
        }
        Debug.Log("CurrentSolution/Solved: " + temp + "/" + solved);
        if (solved)
            EndGame();
    }

    //Ends the game by making all answer buttons non-interactable
    public void EndGame()
    {
        foreach (_PicrossAnswerButton b in answerButtons)
        {
            if (b.transform.GetComponent<Button>() != null)
            {
                //b.SetState("Blank");
                b.transform.GetComponent<Button>().interactable = false;
            }
        }

        OnPuzzleSolved();
    }

    //A "Dummy" Method intended to be inhereted by individual puzzles so that solving any one puzzle can produce any desired effect.
    public virtual void OnPuzzleSolved()
    {
        Debug.Log("OnPuzzleSolved ran in PicrossSnippetBoard");
    }
}
