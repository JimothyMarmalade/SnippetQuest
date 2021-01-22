using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _FutoshikiPuzzle : MonoBehaviour
{
    [Header("Button/Display Prefabs")]
    public Button answerButtonPrefab;
    public Button clueButtonPrefab;

    [Header("Reference to completion status")]
    public TextMeshProUGUI conditionsMetText;

    [Header("Gridsize accounts for needed answer input space")]
    private int gridSize;

    //answerButtonsArray should be [gridSize, gridSize]
    private Button[,] answerButtonsArray;

    //clueButtonsArray should be [gridSize+gridSize-1, gridSize]
    private Button[,] clueButtonsArray;

    [Header("Puzzle Beginning Logic (Clues, preset spaces)")]
    private int[,] presetAnswerKey;
    private char[,] presetClueKey;

    private bool puzzleSolved = false;

    //Used for randomly-generated puzzles to keep track of how many have been solved
    private int timesPuzzleSolved = 0;

    //---------------------------------------------------------

    public void BuildFutoshikiBoard()
    {
        //Establish how many invisible "dividers" exist on the game board for construction purposes.
        //Dividers should always be size of grid (i.e. number count) times 2, plus 1
        int dividers = (gridSize * 2) + 1;

        //Determine the size of the answer buttons and the gaps between them. Answer button lengths combined are 75% of the board.
        float gameboardDimensionX = this.transform.GetComponent<RectTransform>().sizeDelta.x;
        float answerButtonLength = (gameboardDimensionX * 0.75f) / gridSize;
        float emptySpaceLength = (gameboardDimensionX * 0.25f) / (gridSize+1);

        //The width of the cluebuttons and gaps accounts for 25% of the total gameboard Length. Height is used for ensuring
        //The size of the text is appropriate and based off answer button size. 
        float clueButtonWidth = emptySpaceLength;
        float clueButtonHeight = (answerButtonLength * 0.75f);

        //Size of buttons accounted for, now they can be instantiated and placed on the gameboard. After instantiation each answerbutton
        //is stored in answerButtonsArray, and clues are stored in clueButtonsArray.

        //Start with Answer buttons, instantiating from the prefab.  (Remember C# handles arrays as [Row, Column]
        //to clarify -- I refers to the row (the y in a grid) and j refers to the column (the x in a grid)

        //Rows
        for (int i = 0; i < gridSize; i++)
        {
            //Columns
            for (int j = 0; j < gridSize; j++)
            {
                //Instantiate the button
                Button b = Instantiate(answerButtonPrefab);
                _FutoshikiAnswerButton bLogic = b.GetComponent<_FutoshikiAnswerButton>();

                //Set the button's parent to the gameboard
                b.transform.SetParent(this.transform, false);

                //Set the size and position of the button
                b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(answerButtonLength, answerButtonLength);

                b.transform.localPosition = new Vector2((-gameboardDimensionX / 2) + ((j+1)*emptySpaceLength)+(j*answerButtonLength)+(answerButtonLength/2),
                                                  (gameboardDimensionX/2) - ((i+1)*emptySpaceLength) - (i*answerButtonLength) - (answerButtonLength/2));

                //Calls the button's script to set its place in the board and a reference to this as the puzzle it is being used to solve
                bLogic.setGridSpace(j, i);
                bLogic.SetPuzzleControllerReference(this);

                //If the button has a preset clue, marks it on the board
                if (presetAnswerKey[i, j] != 0)
                    bLogic.setNumberPermanent(presetAnswerKey[i, j]);

                //Sets a reference to this button on AnswerButtonsArray
                answerButtonsArray[i, j] = b;
            }
        } //answerButtons all instantiated!

        //Next, the clue buttons can be instantiated and placed on the board.
        //Rows
        for (int i = 0; i < gridSize; i++)
        {
            //Columns
            for (int j = 0; j < (gridSize*2)-1; j++)
            {
                //Read the presetClueKey array for the clue. If no clue exists, there's no reason to do the calculations.
                if (presetClueKey[j, i] != 'n')
                {
                    //Debug.Log("i, j: " + i + ", " + j);

                    //Instantiate the button and set this as its parent
                    Button b = Instantiate(clueButtonPrefab);
                    _FutoshikiClueButton bLogic = b.GetComponent<_FutoshikiClueButton>();
                    b.transform.SetParent(this.transform, false);

                    //Set the size before calculating position
                    b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(clueButtonWidth, clueButtonHeight);

                    //Calculate where the clue goes, considering which row on the board it belongs to.
                    //If the row is even (0, 2, 4) then there are only gridSize-1 squares inbetween slots horizontally.
                    if (j % 2 == 0 && i != gridSize - 1)
                    {
                        b.transform.localPosition = new Vector2(((-gameboardDimensionX / 2) + ((i + 1) * emptySpaceLength) + ((i+1) * answerButtonLength) + (emptySpaceLength / 2)),
                                                                (gameboardDimensionX / 2) - (((j/2)+1) * emptySpaceLength) - ((j/2) * answerButtonLength) - (answerButtonLength/2));
                    }
                    //If the row is odd, then the potential slots are equal to the gridSize
                    else if (j % 2 != 0)
                    {
                        b.transform.localPosition = new Vector2((-gameboardDimensionX / 2) + ((i+1)*emptySpaceLength) + (i*answerButtonLength) + (answerButtonLength/2),
                                                            (gameboardDimensionX / 2) - (((j+1)/2) * emptySpaceLength) - (((j+1)/2) * answerButtonLength) - (emptySpaceLength/2));
    }

                    //Now that position has been set, internal logic for the cluebutton can be established
                    bLogic.setGridSpace(j, i);
                    bLogic.setOrientation(presetClueKey[j, i]);
                    clueButtonsArray[j, i] = b;
                }
                //If there is no needed clue, set the value in the clue array to null
                else
                    clueButtonsArray[j, i] = null;
            }
        }

        //Clues have been generated and placed on board.
        
        //Initialize the conditionsmettext
        UpdateConditionsMetText(false, false, true);

    } // End Board Generation

    //Assigns every answer button to this Controller so other games are not affected
    public void AssignAllButtonsController(_FutoshikiPuzzle puzzleData)
    {
        foreach (Button b in answerButtonsArray)
        {
            if (b != null)
                b.GetComponent<_FutoshikiAnswerButton>().SetPuzzleControllerReference(puzzleData);
        }
    }

    //Checks the board after a button has been pressed to determine if the puzzle has been solved
    public void checkWinConditions()
    {
        //Start by setting all answers to not incorrect. Incorrect answers will be redrawn in red after answer checking.
        foreach (Button b in answerButtonsArray)
            b.GetComponent<_FutoshikiAnswerButton>().markedIncorrect = false;


        Debug.Log("Checking for win conditions...");

        //First check if every space on the board has been filled
        bool emptyButtonFound = false;
        foreach (Button b in answerButtonsArray)
        {
            //If a button is blank, automatic incomplete
            if (b.GetComponent<_FutoshikiAnswerButton>().buttonText.text == " ")
            {
                emptyButtonFound = true;
            }
        }
        Debug.Log("emptyButtonFound = " + emptyButtonFound);

        //If all conditions met, check for duplicate answers in all rows/columns
        bool duplicateAnswerFound = false;

        //Check Rows
        for (int row = 0; row < gridSize; row++)
        {
            //Create a new list of tuples to store answer values and the column they were found in
            List<(int, int)> foundValues = new List<(int, int)>();

            //Next, add every item in the row to the list.
            for (int col = 0; col < gridSize; col++)
            {
                _FutoshikiAnswerButton bLogic = answerButtonsArray[row, col].GetComponent<_FutoshikiAnswerButton>();
                (int, int) valLoc = (bLogic.GetAnswerInt(), col);
                foundValues.Add(valLoc);
            }

            var hashset = new HashSet<int>();
            foreach (var ans in foundValues)
            {
                //If statement is triggered if there is a duplicate item found in the row
                if (!hashset.Add(ans.Item1) && ans.Item1 != 0)
                {
                    duplicateAnswerFound = true;
                    //Each flawed answer must be marked as incorrect
                    foreach (var inc in foundValues)
                        if (inc.Item1 == ans.Item1)
                            if (presetAnswerKey[row, inc.Item2] == 0)
                            answerButtonsArray[row, inc.Item2].GetComponent<_FutoshikiAnswerButton>().markedIncorrect = true;
                }
            }
        }

        //Check columns
        for (int col = 0; col < gridSize; col++)
        {
            //Create a new list of tuples to store answer values and the column they were found in
            List<(int, int)> foundValues = new List<(int, int)>();

            //Next, add every item in the row to the list.
            for (int row = 0; row < gridSize; row++)
            {
                _FutoshikiAnswerButton bLogic = answerButtonsArray[row, col].GetComponent<_FutoshikiAnswerButton>();
                (int, int) valLoc = (bLogic.GetAnswerInt(), row);
                foundValues.Add(valLoc);
            }
            var hashset = new HashSet<int>();
            foreach (var ans in foundValues)
            {
                //If statement triggered if there is a duplicate answer found in this column
                if (!hashset.Add(ans.Item1) && ans.Item1 != 0)
                {
                    duplicateAnswerFound = true;
                    //Each flawed answer must be marked as incorrect
                    foreach (var inc in foundValues)
                        if (inc.Item1 == ans.Item1)
                            if (presetAnswerKey[inc.Item2, col] == 0)
                            answerButtonsArray[inc.Item2, col].GetComponent<_FutoshikiAnswerButton>().markedIncorrect = true;
                }
            }
        }
        Debug.Log("duplicateAnswerFound = " + duplicateAnswerFound);

        //Check each space for condition satisfaction (Lesser/Higher than)
        bool failedConditionFound = false;
        //Rows
        for (int i = 0; i < gridSize; i++)
        {
            //Columns
            for (int j = 0; j < gridSize; j++)
            {
                //Get reference to the answer itself
                _FutoshikiAnswerButton bLogic = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>();
                int answer = bLogic.GetAnswerInt();

                //Set the button text color to black as a default
                //bLogic.SetTextColor(Color.black);

                //Skip calculations if there's no answer
                if (answer != 0)
                {

                    //Check for top clue. If at top row, must be nonexistent
                    if (i != 0)
                    {
                        //Set reference to clue button, skip if clue does not exist
                        if (clueButtonsArray[i + i - 1, j] != null)
                        {
                            //Top button exists, get reference.
                            _FutoshikiClueButton clue = clueButtonsArray[i + i - 1, j].GetComponent<_FutoshikiClueButton>();

                            //Top button can only be up or down
                            //If pointing up, answer above must be less than this answer
                            if (clue.cluePointer == 'u')
                            {
                                int mustBeLower = answerButtonsArray[i - 1, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher !=0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", top clue, " +
                                        "MBH" + mustBeHigher + ", MBL " + mustBeLower);
                                }
                            }
                            //If pointing down, answer above must be greater than this answer
                            if (clue.cluePointer == 'd')
                            {
                                int mustBeLower = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i - 1, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher != 0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", top clue, " +
                                        "MBH" + mustBeHigher + ", MBL " + mustBeLower);
                                }
                            }
                        }
                    }

                    //Check for bottom clue. If at bottom row, must be nonexistent
                    if (i != gridSize - 1)
                    {
                        //Set reference to clue button, skip if clue does not exist
                        if (clueButtonsArray[i + i + 1, j] != null)
                        {
                            //Bottom button exists, get reference.
                            _FutoshikiClueButton clue = clueButtonsArray[i + i + 1, j].GetComponent<_FutoshikiClueButton>();

                            //Bottom button can only be up or down
                            //If pointing up, answer below must be greater than this answer
                            if (clue.cluePointer == 'u')
                            {
                                int mustBeLower = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i + 1, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher != 0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", bottom clue, " +
                                        "MBH" + mustBeHigher + ", MBL " + mustBeLower);
                                }
                            }
                            //If pointing down, answer below must be less than this answer
                            if (clue.cluePointer == 'd')
                            {
                                int mustBeLower = answerButtonsArray[i + 1, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher != 0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", bottom clue, " +
                                        "MBH" + mustBeHigher + ", MBL " + mustBeLower);
                                }
                            }
                        }
                    }

                    //Check for left clue. If at left row, must be nonexistent
                    if (j != 0)
                    {
                        //Set reference to clue button, skip if clue does not exist
                        if (clueButtonsArray[i + i, j - 1] != null)
                        {
                            //Left button exists, get reference.
                            _FutoshikiClueButton clue = clueButtonsArray[i + i, j - 1].GetComponent<_FutoshikiClueButton>();

                            //Left button can only be left or right
                            //If pointing left, answer must be greater than left answer
                            if (clue.cluePointer == 'l')
                            {
                                int mustBeLower = answerButtonsArray[i, j - 1].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher != 0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", left clue ");
                                }
                            }
                            //If pointing right, answer must be less than left answer
                            if (clue.cluePointer == 'r')
                            {
                                int mustBeLower = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i, j - 1].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher != 0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", left clue ");
                                }
                            }
                        }
                    }

                    //Check for right clue. If at right row, must be nonexistent
                    if (j != gridSize - 1)
                    {
                        //Set reference to clue button, skip if clue does not exist
                        if (clueButtonsArray[i + i, j] != null)
                        {
                            //right button exists, get reference.
                            _FutoshikiClueButton clue = clueButtonsArray[i + i, j].GetComponent<_FutoshikiClueButton>();

                            //right button can only be left or right
                            //If pointing left, answer must be less than right answer
                            if (clue.cluePointer == 'l')
                            {
                                int mustBeLower = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i, j + 1].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher != 0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", right clue ");
                                }
                            }
                            //If pointing right, answer must be greater than right answer
                            if (clue.cluePointer == 'r')
                            {
                                int mustBeLower = answerButtonsArray[i, j + 1].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();
                                int mustBeHigher = answerButtonsArray[i, j].GetComponent<_FutoshikiAnswerButton>().GetAnswerInt();

                                if (mustBeLower != 0 && mustBeHigher != 0 && ((mustBeLower >= mustBeHigher || mustBeHigher <= mustBeLower)))
                                {
                                    failedConditionFound = true;
                                    //Change the color of the answer to indicate it is incorrect. Ignore preset values.
                                    if (presetAnswerKey[i, j] == 0)
                                        bLogic.markedIncorrect = true;

                                    Debug.Log("Failed condition found at " + clue.GetLoc() + ", right clue ");
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("failedConditionFound = " + failedConditionFound);

        //Updates HUD text with player's status
        UpdateConditionsMetText(emptyButtonFound, duplicateAnswerFound, failedConditionFound);

        //All buttons flagged as incorrect are shown in red
        foreach(Button b in answerButtonsArray)
            b.GetComponent<_FutoshikiAnswerButton>().SetTextColor();
        

        //If every condition has been met, puzzle has been solved
        if (!emptyButtonFound && !duplicateAnswerFound && !failedConditionFound)
            puzzleSolved = true;

        //If puzzle has been solved, game has been won and is no longer playable
        if (puzzleSolved)
        {
            EndGame();
            //Outputs the puzzle's solution to the console
            PrintSolution();
        }

    }

    //Updates the conditionsMetText to show how many conditions the player has satisfied
    public void UpdateConditionsMetText(bool emptyButtonFound, bool duplicateAnswerFound, bool failedConditionFound)
    {
        string newText = "";

        if (emptyButtonFound || duplicateAnswerFound || failedConditionFound)
        {
            if (!emptyButtonFound)
                newText += "All Spaces filled!\n";
            else
                newText += "All spaces must be filled!\n";

            if (!duplicateAnswerFound)
                newText += "No duplicate answers found!\n";
            else
                newText += "Duplicate answer found in row or column!\n";

            if (!failedConditionFound)
                newText += "Every LesserThan condition has been met!";
            else
                newText += "A LesserThan condition has not been met!";
        }
        else
        {
            newText = "Puzzle Complete!";
        }


        conditionsMetText.text = newText;

    }

    //Effectively ends the game by making all buttons on the board non-interactable
    public void EndGame()
    {
        foreach (Button b in answerButtonsArray)
        {
            if (b != null)
                b.interactable = false;
        }

        foreach (Button b in clueButtonsArray)
        {
            if (b != null)
                b.interactable = false;
        }
    }

    //Outputs a winning solution to the console
    public void PrintSolution()
    {
        string sol = "";
        int i = 0;
        foreach (Button b in answerButtonsArray)
        {
            if (i > gridSize - 1)
            {
                sol += ", ";
                i = 0;
            }
            string num = b.GetComponent<_FutoshikiAnswerButton>().buttonText.text;
            sol += num;
            i++;
        }
        Debug.Log("Solution: " + sol);
    }

    //Resets the puzzle completely, flagging as incomplete and clearing board
    public void ResetPuzzle()
    {
        puzzleSolved = false;

        foreach (Button b in answerButtonsArray)
            Destroy(b.gameObject);

        foreach (Button b in clueButtonsArray)
            Destroy(b.gameObject);


        BuildFutoshikiBoard();
    }

    public void DestroyPuzzle()
    {
        //Kills the puzzle completely (used for generating a new puzzle)
        puzzleSolved = false;

        foreach (Button b in answerButtonsArray)
        {
            if (b != null)
                Destroy(b.gameObject);
        }

        foreach (Button b in clueButtonsArray)
        {
            if (b != null)
                Destroy(b.gameObject);
        }
    }


    //---------------------------------------------------------


    //Getters and Setters for fields used primarily outside of the puzzle itself
    public void SetPuzzleSolved(bool b)
    {
        puzzleSolved = b;
    }

    public bool GetPuzzleSolved()
    {
        return puzzleSolved;
    }

    //Getters and Setters for necessary fields in masterclass
    public void SetGridSize(int x)
    {
        gridSize = x;
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public void SetAnswerButtonsArray(int x)
    {
        answerButtonsArray = new Button[x, x];
    }

    public Button[,] GetanswerButtonsArray()
    {
        return answerButtonsArray;
    }

    public void SetClueButtonsArray(int x)
    {
        clueButtonsArray = new Button[(x*2)-1, x];
    }

    public Button[,] GetClueButtonsArray()
    {
        return clueButtonsArray;
    }

    public void SetPresetAnswerKey(int[,] key)
    {
        presetAnswerKey = key;
    }

    public int[,] GetPresetAnswerKey()
    {
        return presetAnswerKey;
    }

    public void SetPresetClueKey(char[,] key)
    {
        presetClueKey = key;
    }

    public char[,] GetPresetClueKey()
    {
        return presetClueKey;
    }






}
