using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _PicrossPuzzle : MonoBehaviour
{
    //Master file used to house and handle all methods pertaining to Picross Puzzles

    [Header("Button/Display Prefabs and References")]
    public Button answerButtonPrefab;
    public Button clueButtonPrefab;
    public RectTransform gridPanel;
    public TextMeshProUGUI PuzzleHeader;

    [Header("Other Needed Variables")]
    private int gridSize = 5;

    private int[,] puzzleSolution;

    private Button[,] answerButtonsArray;
    private Button[,] clueButtonsArrayVertical;
    private Button[,] clueButtonsArrayHorizontal;

    public bool PuzzleSolved = false;

    protected string PuzzleTitle;


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //Builds the buttons needed for the puzzle based on the GridSize
    public void BuildPicrossBoard()
    {
        //Firstly, calculate the size of the grid (with the buttons in it) relative to the Overall panel and position it in the panel
        Vector2 masterPanelDimensions = this.GetComponent<RectTransform>().sizeDelta;
        gridPanel.sizeDelta = new Vector2(masterPanelDimensions.x * 0.75f, masterPanelDimensions.y * 0.75f);
        gridPanel.localPosition = new Vector2((masterPanelDimensions.y * 0.615f),
                                              (masterPanelDimensions.y / 2) - masterPanelDimensions.y * 0.615f);





        //First, calculate the size of the buttons and gaps between them based on the size of the grid
        Vector2 gridDimensions = gridPanel.sizeDelta;
        float gridWidth = gridDimensions.x;
        float gridHeight = gridDimensions.y;

        int numGaps = gridSize + 1;
        float gapWidth = gridWidth * 0.01f;

        float answerButtonWidth = (-0.01f * gridWidth) + ((0.99f * gridWidth) / gridSize);

        //Initialize the answerButtonsArray and clueButtonArrays
        SetAnswerButtonsArray(gridSize);

        //Instantiate the buttons, place them where they need to go, add this as their controller
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                Button b = Instantiate(answerButtonPrefab);
                _PicrossAnswerButton bLogic = b.GetComponent<_PicrossAnswerButton>();

                //Set the button as a child of the grid, NOT the overall board
                b.transform.SetParent(gridPanel.transform, false);

                //Set the size and position of the button
                b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(answerButtonWidth, answerButtonWidth);

                b.transform.localPosition = new Vector2((-gridWidth / 2) + ((col + 1) * gapWidth) + (col * answerButtonWidth) + (answerButtonWidth / 2),
                                                        (gridWidth / 2) - ((row + 1) * gapWidth) - (row * answerButtonWidth) - (answerButtonWidth / 2));

                //Calls the button's script to set its place in the board and a reference to this as the puzzle it is being used to solve
                bLogic.SetPuzzleControllerReference(this);


                answerButtonsArray[row, col] = b;
            }
        }

        //Now that every answer button has been created, we need to build the clues that are needed to indicate the puzzle solution
        //First, calculate the maximum amount of clues that can exist for any given row/column (gridsize/2, rounding up)
        int maxClues = 0;
        if (gridSize % 2 == 0)
            maxClues = gridSize / 2;
        else
            maxClues = (gridSize + 1) / 2;



        Debug.Log("maxClues: " + maxClues);

        //Initialize the clue button arrays
        SetClueButtonsArrays(gridSize, maxClues);

        //Set the positions of all clue buttons in the horizontal space
        for (int row = 0; row < maxClues; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                //Set references to button and logic
                Button b = Instantiate(clueButtonPrefab);
                _PicrossClueButton bLogic = b.GetComponent<_PicrossClueButton>();

                //Set the button as a child of the grid, NOT the overall board
                b.transform.SetParent(gridPanel.transform, false);

                float heightOffset = (masterPanelDimensions.x * 0.23f);
                //Set size and position of the button -- for horizontal buttons
                b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(answerButtonWidth + gapWidth, heightOffset / maxClues);

                b.transform.localPosition = new Vector2((-gridWidth / 2) + ((col + 1) * gapWidth) + (col * answerButtonWidth) + (answerButtonWidth / 2),
                                                        (gridWidth / 2) + heightOffset - (heightOffset / maxClues / 2) - (row * heightOffset / maxClues));

                //Calls the button's script to set its place in the board and a reference to this as the puzzle it is being used to solve

                bLogic.SetPuzzleControllerReference(this);


                clueButtonsArrayHorizontal[row, col] = b;
            }
        }

        //Set the positions of all clue buttons in the vertical space
        for (int col = 0; col < maxClues; col++)
        {
            for (int row = 0; row < gridSize; row++)
            {
                //Set references to button and logic
                Button b = Instantiate(clueButtonPrefab);
                _PicrossClueButton bLogic = b.GetComponent<_PicrossClueButton>();

                //Set the button as a child of the grid, NOT the overall board
                b.transform.SetParent(gridPanel.transform, false);

                //Set size and position of the button -- for vertical buttons
                float heightOffset = (masterPanelDimensions.x * 0.23f);
                b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(heightOffset / maxClues, answerButtonWidth + gapWidth);

                b.transform.localPosition = new Vector2((-gridWidth / 2) - heightOffset + (heightOffset / maxClues / 2) + (col * heightOffset / maxClues),
                                                        (gridWidth / 2) - ((row + 1) * gapWidth) - (row * answerButtonWidth) - (answerButtonWidth / 2));

                //Calls the button's script to set its place in the board and a reference to this as the puzzle it is being used to solve

                bLogic.SetPuzzleControllerReference(this);


                clueButtonsArrayVertical[row, col] = b;
            }
        }

        //Puzzle Generation was successful. Hide Title and begin clue generation.
        PuzzleHeader.text = "";

        BuildClues();
    }

    //Builds the clues needed for the player to solve the puzzle based on the answer grid
    public void BuildClues()
    {
        //Now that every answer button has been created, we need to build the clues that are needed to indicate the puzzle solution
        //First, calculate the maximum amount of clues that can exist for any given row/column (gridsize/2, rounding up)
        int maxClues = 0;
        if (gridSize % 2 == 0)
            maxClues = gridSize / 2;
        else
            maxClues = (gridSize + 1) / 2;


        //Starting out, we know the maximum amount of clues that can exist for any given puzzle based off of the gridSize,
        //And all the buttons have already been built. So we need to determine what

        //Start with vertical clues
        for (int col = 0; col < gridSize; col++)
        {
            Debug.Log("Running Horizontal Clue Column " + col);

            //First, for each column, we need to create an array detailing which buttons are supposed to be on/off
            List<bool> rawAnsList = new List<bool>();
            string debugRawAnsListStorage = "";
            for (int row = 0; row < gridSize; row++)
            {
                bool t;
                if (puzzleSolution[row, col] == 1)
                    t = true;
                else
                    t = false;

                rawAnsList.Add(t);
                debugRawAnsListStorage += t;
            }
            Debug.Log("debugRawAnsListStorage: " + debugRawAnsListStorage);

            //Create a List to store the individual answer lengths
            List<int> workingAnsList = new List<int>();
            int currentAnswerLength = 0;
            for (int z = 0; z < rawAnsList.Count; z++)
            {
                //If the value is true (button should be on)
                if (rawAnsList[z])
                {
                    //Increment the current answer length and continue
                    currentAnswerLength++;
                    //if this is the final button in the row, add it to the list
                    if (z == rawAnsList.Count - 1)
                        workingAnsList.Add(currentAnswerLength);
                }
                else if (!rawAnsList[z])
                {
                    //if the next item IS NOT a toggled button, we add the counter to the workingList (if !=0), reset the value, then continue.
                    if (currentAnswerLength != 0)
                        workingAnsList.Add(currentAnswerLength);
                    currentAnswerLength = 0;
                }
            }

            //We now have a compiled list of the answers in the column. We can work backwards, adding them into the clue buttons.
            for (int row = maxClues - 1; row >= 0; row--)
            {
                //if there are no clues at all for the row/column, then set the first clue to 0
                if (row == maxClues - 1 && workingAnsList.Count <= 0)
                {
                    Debug.Log("Storing value 0 at verticalClueColumn[" + row + ", " + col + "]");
                    //Write the value to the button
                    _PicrossClueButton bLogic = clueButtonsArrayHorizontal[row, col].GetComponent<_PicrossClueButton>();
                    bLogic.SetButtonText(0);
                }
                else if (workingAnsList.Count > 0)
                {
                    //Store the value
                    int clue = workingAnsList[workingAnsList.Count - 1];
                    Debug.Log("Storing value + " + clue + " at verticalClueColumn[" + row + ", " + col + "]");
                    //Delete the value from the list
                    workingAnsList.RemoveAt(workingAnsList.Count - 1);
                    //Write the value to the button
                    _PicrossClueButton bLogic = clueButtonsArrayHorizontal[row, col].GetComponent<_PicrossClueButton>();
                    bLogic.SetButtonText(clue);
                }
                //If there are no more clues, then the remaining buttons need to be destroyed
                else
                {
                    Debug.Log("Destroying button at verticalClueColumn [" + row + ", " + col + "]");
                    Destroy(clueButtonsArrayHorizontal[row, col].gameObject);
                    clueButtonsArrayHorizontal[row, col] = null;
                }
            }

        }

        //Now do the same for Horizontal Clues
        //Start with vertical clues
        for (int row = 0; row < gridSize; row++)
        {
            Debug.Log("Running Vertical Clue Column " + row);

            //First, for each column, we need to create an array detailing which buttons are supposed to be on/off
            List<bool> rawAnsList = new List<bool>();
            string debugRawAnsListStorage = "";
            for (int col = 0; col < gridSize; col++)
            {
                bool t;
                if (puzzleSolution[row, col] == 1)
                    t = true;
                else
                    t = false;

                rawAnsList.Add(t);
                debugRawAnsListStorage += t;
            }
            Debug.Log("debugRawAnsListStorage: " + debugRawAnsListStorage);

            //Create a List to store the individual answer lengths
            List<int> workingAnsList = new List<int>();
            int currentAnswerLength = 0;
            for (int z = 0; z < rawAnsList.Count; z++)
            {
                //If the value is true (button should be on)
                if (rawAnsList[z])
                {
                    //Increment the current answer length and continue
                    currentAnswerLength++;
                    //if this is the final button in the row, add it to the list
                    if (z == rawAnsList.Count - 1)
                        workingAnsList.Add(currentAnswerLength);
                }
                else if (!rawAnsList[z])
                {
                    //if the next item IS NOT a toggled button, we add the counter to the workingList (if !=0), reset the value, then continue.
                    if (currentAnswerLength != 0)
                        workingAnsList.Add(currentAnswerLength);
                    currentAnswerLength = 0;
                }
            }

            //We now have a compiled list of the answers in the column. We can work backwards, adding them into the clue buttons.
            for (int col = maxClues - 1; col >= 0; col--)
            {
                //if there are no clues at all for the row/column, then set the first clue to 0
                if (col == maxClues - 1 && workingAnsList.Count <= 0)
                {
                    Debug.Log("Storing value 0 at horizontalClueColumn[" + row + ", " + col + "]");
                    //Write the value to the button
                    _PicrossClueButton bLogic = clueButtonsArrayHorizontal[row, col].GetComponent<_PicrossClueButton>();
                    bLogic.SetButtonText(0);
                }
                else if (workingAnsList.Count > 0)
                {
                    //Store the value
                    int clue = workingAnsList[workingAnsList.Count - 1];
                    Debug.Log("Storing value + " + clue + " at horizontalClueColumn[" + row + ", " + col + "]");
                    //Delete the value from the list
                    workingAnsList.RemoveAt(workingAnsList.Count - 1);
                    //Write the value to the button
                    _PicrossClueButton bLogic = clueButtonsArrayVertical[row, col].GetComponent<_PicrossClueButton>();
                    bLogic.SetButtonText(clue);
                }
                //If there are no more clues, then the remaining buttons need to be destroyed
                else
                {
                    Debug.Log("Destroying button at horizontalClueColumn [" + row + ", " + col + "]");
                    Destroy(clueButtonsArrayVertical[row, col].gameObject);
                    clueButtonsArrayVertical[row, col] = null;
                }
            }

        }

    }


    //Checks if all the buttons are pressed properly for a win condition to be satisfied
    public void CheckWinCondition()
    {
        bool puzzleCleared = true;

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                _PicrossAnswerButton bLogic = answerButtonsArray[row, col].GetComponent<_PicrossAnswerButton>();

                int bState;
                if (bLogic.GetToggle())
                    bState = 1;
                else
                    bState = 0;

                if (puzzleSolution[row, col] != bState)
                {
                    puzzleCleared = false;
                }

            }
        }

        if (puzzleCleared)
        {
            EndGame();
        }

    }

    //Ends the game by making all answer buttons non-interactable
    public void EndGame()
    {
        foreach (Button b in answerButtonsArray)
        {
            if (b != null)
                b.interactable = false;
        }

        PuzzleSolved = true;

        PuzzleHeader.text = PuzzleTitle;
        OnPuzzleSolved();
    }

    //A "Dummy" Method intended to be inhereted by individual puzzles so that solving any one puzzle can produce any desired effect.
    public virtual void OnPuzzleSolved()
    {
        Debug.Log("OnPuzzleSolved ran in _PicrossPuzzle");
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void SetAnswerButtonsArray(int i)
    {
        answerButtonsArray = new Button[i, i];
    }

    public Button[,] GetAnswerButtonsArray()
    {
        return answerButtonsArray;
    }

    public void SetClueButtonsArrays(int size, int max)
    {
        clueButtonsArrayHorizontal = new Button[max, size];
        clueButtonsArrayVertical = new Button[size, max];
    }

    public void SetPuzzleSolution(int[,] i)
    {
        puzzleSolution = i;
    }

    public void SetGridSize(int i)
    {
        gridSize = i;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~




}
