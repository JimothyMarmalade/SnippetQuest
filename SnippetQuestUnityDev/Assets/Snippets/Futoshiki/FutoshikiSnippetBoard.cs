/*
 * Created by Logan Edmund, 3/12/21
 * Last Modified by Logan Edmund, 5/11/21
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
    private List<_FutoshikiAnswerButton> answerButtons;
    private List<GameObject> clueButtons;

    private int numDividers;
    private float answerButtonWidth;
    private float clueButtonWidth;
    private float clueButtonHeight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < answerButtons.Count; i++)
                answerButtons[i].SetCurrentValue(futoshikiPuzzleData.snippetSolution[i]-48);
        }
    }

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
        InstantiateAnswerButtons();
        InstantiateClues();
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

        answerButtons = new List<_FutoshikiAnswerButton>();
        clueButtons = new List<GameObject>();
    }

    //InstantiateAnswers will generate all needed answer buttons based on data input and position them on the board.
    private void InstantiateAnswerButtons()
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
            
            bLogic.InstAnswerButton(this, futoshikiPuzzleData.gridSize);

            if (futoshikiPuzzleData.visibleAnswers[i] == '1')
                bLogic.SetValuePermanent(futoshikiPuzzleData.snippetSolution[i]-48);

            answerButtons.Add(bLogic);
        }
    }

    //InstantiateClues will generate all needed clues based on data input and position them on the board.
    private void InstantiateClues()
    {
        //Begin with the horizontal clues -- those that are concerned with the answers directly above and below them.
        //For each clue, spawn the button in the desired location and rotate it +90 or -90 based on which answer should be higher/lower.
        int visibleCluesReference = 0;
        for (int i = 0; i < futoshikiPuzzleData.gridSize*(futoshikiPuzzleData.gridSize-1); i++)
        {
            if (futoshikiPuzzleData.visibleClues[visibleCluesReference] == '1')
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

                b.transform.localPosition = new Vector2((-gridPanel.sizeDelta.x / 2 + clueButtonWidth + answerButtonWidth / 2 + xOffset),
                                                         (gridPanel.sizeDelta.y / 2 - clueButtonWidth * 1.5f - answerButtonWidth - yOffset));

                //Determine if the clue should be rotated or not. 90 if the item above is less, -90 if the item below is less.
                int c = futoshikiPuzzleData.snippetSolution[i]; //Top
                int d = futoshikiPuzzleData.snippetSolution[i + futoshikiPuzzleData.gridSize]; //Bottom

                if (c < d)
                    b.transform.Rotate(new Vector3(0f, 0f, 90f));
                else
                    b.transform.Rotate(new Vector3(0f, 0f, -90f));

                clueButtons.Add(b.gameObject);
            }
            visibleCluesReference++;
        }
        //Repeat with vertical clues, comparing values to their right and left.
        int pointer = 0;
        for (int i = 0; i < futoshikiPuzzleData.gridSize * (futoshikiPuzzleData.gridSize - 1); i++)
        {
            if (i % (futoshikiPuzzleData.gridSize - 1) == 0 && pointer != 0)
                pointer++;

            if (futoshikiPuzzleData.visibleClues[visibleCluesReference] == '1')
            {
                //Instantiate the Button
                Button b = Instantiate(clueButtonPrefab);
                //Set the button's parent to the gameboard
                b.transform.SetParent(gridPanel.transform, false);
                //Set the size and position of the button
                b.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(clueButtonWidth, clueButtonWidth);

                //Position the button
                float xOffset = (i % (futoshikiPuzzleData.gridSize - 1)) * (clueButtonWidth + answerButtonWidth);
                float yOffset = (i / (futoshikiPuzzleData.gridSize - 1)) * (clueButtonWidth + answerButtonWidth);

                b.transform.localPosition = new Vector2((-gridPanel.sizeDelta.x / 2 + clueButtonWidth * 1.5f + answerButtonWidth + xOffset),
                                                        (gridPanel.sizeDelta.y / 2 - clueButtonWidth - answerButtonWidth / 2 - yOffset));

                //Determine if the clue should be rotated or not. 90 if the item above is less, -90 if the item below is less.
                //Debug.Log("pointer% gridSize-1 =" + (pointer % (futoshikiPuzzleData.gridSize-1)));

                //Debug.Log("i=" + i + ", pointer=" + pointer);
                int c = futoshikiPuzzleData.snippetSolution[pointer]; //Left
                int d = futoshikiPuzzleData.snippetSolution[pointer + 1]; //Right

                if (c < d)
                    b.transform.Rotate(new Vector3(0f, 0f, 180f));

                clueButtons.Add(b.gameObject);
            }
            
            pointer++;
            visibleCluesReference++;           
        }
    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Quick check compares the input solution to the snippet's coded solution and checks for equality. Futoshikis can only ever have one solution.
    //Method also runs through the current solution to highlight errors in logic
    public void CheckWinCondition()
    {
        //Possible Logic Errors:
        //1. An empty button exists
        //2. A row/column has a number twice
        //3. a LesserThan is not fulfilled
        bool AllAnswersFilled = true;
        bool NoDuplicateAnswers = true;
        bool AllConditionsSatisfied = true;

        string temp = "";
        foreach (_FutoshikiAnswerButton b in answerButtons)
        {
            temp += b.GetCurrentVal().ToString();
            if (b.GetCurrentVal().Equals(0))
                AllAnswersFilled = false;
            b.SetConflicting(false);
        }
        Debug.Log("FutoshikiCurrentSolution: " + temp);

        //Check for duplicates in rows
        List<int> rownums = new List<int>();
        int val;
        int idx;
        for (int i = 0; i < answerButtons.Count; i+= futoshikiPuzzleData.gridSize)
        {
            rownums.Clear();

            for (int j = 0; j < futoshikiPuzzleData.gridSize; j++)
            {
                val = (int)temp[i + j];

                if (val == 0 || !rownums.Contains(val))
                {
                    rownums.Add(val);
                }
                else
                {
                    rownums.Add(val);
                    NoDuplicateAnswers = false;
                    answerButtons[i + j].SetConflicting(true);
                    answerButtons[i + j - (j-rownums.IndexOf(val))].SetConflicting(true);
                }
            }
        }
        //Check for duplicates in columns
        for (int i = 0; i < futoshikiPuzzleData.gridSize; i++)
        {
            rownums.Clear();

            for (int j = 0; j < futoshikiPuzzleData.gridSize; j++)
            {
                idx = i + (j * futoshikiPuzzleData.gridSize);
                val = (int)temp[idx];

                if (val == 0 || !rownums.Contains(val))
                {
                    rownums.Add(val);
                }
                else
                {
                    rownums.Add(val);
                    NoDuplicateAnswers = false;
                    answerButtons[idx].SetConflicting(true);
                    answerButtons[i + (rownums.IndexOf(val) * futoshikiPuzzleData.gridSize)].SetConflicting(true);
                }
            }
        }

        //Check for condition failures
        //First half of clues are concerned with above/below
        int length = ((futoshikiPuzzleData.gridSize * futoshikiPuzzleData.gridSize) - futoshikiPuzzleData.gridSize);
        for (int i = 0; i < length; i++)
        {
            int a = (int)temp[i];                                       //Top
            int b = (int)temp[i + futoshikiPuzzleData.gridSize];        //Bottom
            if (a != 48 && b != 48)
            {
                //Draw from answer string if the above value should be greater/lesser.
                int c = futoshikiPuzzleData.snippetSolution[i]; //Top
                int d = futoshikiPuzzleData.snippetSolution[i + futoshikiPuzzleData.gridSize]; //Bottom
                //Debug.Log("A = " + a + ", B = " + b + ", C = " + c + ", D = " + d);
                //Compare top/bottom values.
                if ((c > d) && !(a > b))
                {
                    AllConditionsSatisfied = false;
                    answerButtons[i].SetConflicting(true);
                    answerButtons[i + futoshikiPuzzleData.gridSize].SetConflicting(true);
                }
                else if ((c < d) && !(a < b))
                {
                    AllConditionsSatisfied = false;
                    answerButtons[i].SetConflicting(true);
                    answerButtons[i + futoshikiPuzzleData.gridSize].SetConflicting(true);
                }
            }
        }

        //second half are concerned with left/right
        int pointer = 0;
        for (int i = 0; i < futoshikiPuzzleData.gridSize * (futoshikiPuzzleData.gridSize - 1); i++)
        {
            if (i % (futoshikiPuzzleData.gridSize - 1) == 0 && pointer != 0)
                pointer++;

            //Debug.Log("pointer% gridSize-1 =" + (pointer % (futoshikiPuzzleData.gridSize-1)));
            int a = (int)temp[pointer];      //Left
            int b = (int)temp[pointer + 1];  //Right

            if (a != 48 && b != 48)
            {
                int c = futoshikiPuzzleData.snippetSolution[pointer]; //Left
                int d = futoshikiPuzzleData.snippetSolution[pointer + 1]; //Right
                
                //Debug.Log("A = " + (a-48) + ", B = " + (b - 48) + ", C = " + (c - 48) + ", D = " + (d - 48));

                if ((c > d) && !(a > b))
                {
                    AllConditionsSatisfied = false;
                    answerButtons[pointer].SetConflicting(true);
                    answerButtons[pointer + 1].SetConflicting(true);
                }
                else if ((c < d) && !(a < b))
                {
                    AllConditionsSatisfied = false;
                    answerButtons[pointer].SetConflicting(true);
                    answerButtons[pointer + 1].SetConflicting(true);
                }
            }  
            pointer++;
        }

        //Finally, mark all the conflicting buttons in red
        foreach (_FutoshikiAnswerButton b in answerButtons)
        {
            if (b.GetConflicting() && !b.GetLocked())
                b.SetTextColor(Color.red);
            else
                b.SetTextColor(Color.black);
        }

        Debug.Log("AllAnswersFilled = " + AllAnswersFilled);
        Debug.Log("NoDuplicateAnswers = " + NoDuplicateAnswers);
        Debug.Log("AllConditionsSatisfied = " + AllConditionsSatisfied);

        if (AllAnswersFilled && NoDuplicateAnswers && AllConditionsSatisfied)
            EndGame();
    }

    public void EndGame()
    {
        foreach (_FutoshikiAnswerButton b in answerButtons)
            b.transform.GetComponent<Button>().interactable = false;

        OnPuzzleSolved();
    }

    public void OnPuzzleSolved()
    {
        Debug.Log("OnPuzzleSolved ran in FutoshikiSnippetBoard");

        AudioManager.Instance.Play("SnippetSolved");
        SnippetEvents.Instance.SnippetSolved(futoshikiPuzzleData.snippetSlug);
    }

    public void OnPuzzleCompleted()
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
