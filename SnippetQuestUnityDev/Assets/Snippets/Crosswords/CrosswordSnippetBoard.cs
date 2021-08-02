using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Last edited by Logan Edmund, 5/7/21


public class CrosswordSnippetBoard : MonoBehaviour
{
    
    [Header("Button/Display Prefabs and References (STATIC)")]
    public GameObject crosswordGridSpacePrefab;
    public RectTransform gridPanel;

    public TMP_Text TitleText;

    public TMP_Text AcrossClueText;
    public TMP_Text DownClueText;
    

    [Header("Board Generation Variables")]
    public CrosswordSnippet crosswordPuzzleData;
    public string crosswordSlug;

    [Header("Puzzle Handling Variables")]
    private List<string> cluesDown;
    private List<string> cluesAcross;
    private GameObject[] inputs;

    private CrosswordSnippet CurrentSnippetData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject g in inputs)
                if (g != null && g.GetComponent<_CrosswordSquare>() != null)
                    g.GetComponent<_CrosswordSquare>().AutoAnswer();

            CheckForCorrectAnswer();
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public bool TryBuildCrosswordBoard(CrosswordSnippet s)
    {
        crosswordPuzzleData = null;
        //Check to ensure the inserted Picross Snippet has all necessary information for the build, then loads the puzzle from data.
        if (s.CheckCriticalInformation())
        {
            crosswordPuzzleData = s;
            crosswordSlug = s.snippetSlug;
            BuildCrosswordBoard();
            CurrentSnippetData = s;
            return true;
        }
        else
        {
            Debug.LogError(s.snippetSlug + " failed CheckCriticalInformation! Terminating build attempt.");
            return false;
        }
    }

    public void BuildCrosswordBoard()
    {

        //First, the input board defines the size of the crosswordGridButtons.
        float boardSizeHor = gridPanel.sizeDelta.x;
        float boardSizeVert = gridPanel.sizeDelta.y;

        float inputSizeHor = boardSizeHor / crosswordPuzzleData.gridSizeHorizontal;
        float inputSizeVert = boardSizeVert / crosswordPuzzleData.gridSizeVertical;


        //With that done, we can now generate prefab buttons to fill the grid.
        inputs = new GameObject[crosswordPuzzleData.gridSizeHorizontal * crosswordPuzzleData.gridSizeVertical];

        float startLocX = inputSizeHor/2;
        float startLocY = inputSizeVert/2;

        int arrayNum = 0;
        //Build all needed input spaces, place them on the board, and assign them to the array
        for (int row = 0; row < crosswordPuzzleData.gridSizeHorizontal; row++)
        {
            for (int col = 0; col < crosswordPuzzleData.gridSizeVertical; col++)
            {
                //Calculate the Y value for the row
                GameObject a = Instantiate(crosswordGridSpacePrefab);
                //Set the button as a child of the grid, NOT the overall board
                a.transform.SetParent(gridPanel.transform, false);
                //Set the size and position of the button
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(inputSizeHor, inputSizeVert);

                a.transform.localPosition = new Vector2((row * inputSizeHor) + (inputSizeHor / 2),
                                                        (boardSizeVert / 2) - (col * inputSizeVert) - (inputSizeVert/ 2));

                a.GetComponent<_CrosswordSquare>().SetPuzzleControllerReference(this);
                a.GetComponent<_CrosswordSquare>().NumCheck();

                inputs[arrayNum] = a;
                arrayNum++;
            }
        }

        //With the buttons made, we can begin setting those used in the puzzle with correct answers.
        //Scrub through the WordsAcross array to get words, location on board, size of word, etc..

        arrayNum = 0;
        cluesAcross = new List<string>();
        foreach (CrosswordWord word in crosswordPuzzleData.HorizontalWords)
        {
            word.Capitalize();
            int locX = word.Location.x;
            int locY = word.Location.y;
            int vert = crosswordPuzzleData.gridSizeVertical;
            for (int s = 0; s < word.Word.Length; s++)
            {
                Debug.Log("Position: " + ((locX * vert) + locY + (s*vert)));
                _CrosswordSquare square = inputs[(locX * vert) + locY + (s * vert)].GetComponent<_CrosswordSquare>();
                square.SetAnswerChar(word.Word[s]);
                square.SetAcrossWordNum(word.Num);
                square.AcrossClueRef = arrayNum;

                if (s == 0)
                    square.SetIsWordStart(true);
            }

            //Add this word's clue into the clue list.
            cluesAcross.Add(word.Clue);
        }
        //All across words are set. Now do down words to ensure compatibility
        arrayNum = 0;

        cluesDown = new List<string>();
        foreach (CrosswordWord word in crosswordPuzzleData.VerticalWords)
        {
            word.Capitalize();
            int locX = word.Location.x;
            int locY = word.Location.y;
            int vert = crosswordPuzzleData.gridSizeVertical;

            for (int s = 0; s < word.Word.Length; s++)
            {
                Debug.Log("Position: " + ((locX * vert) + locY));
                _CrosswordSquare square = inputs[(locX * vert) + locY + s].GetComponent<_CrosswordSquare>();
                square.SetAnswerChar(word.Word[s]);
                square.SetDownWordNum(word.Num);
                square.DownClueRef = arrayNum;


                if (s == 0)
                    square.SetIsWordStart(true);
            }

            //Add this word's clue into the clue list.
            cluesDown.Add(word.Clue);
        }


        //With all the buttons set, it's time to tidy up the board by removing inputs with no clues and
        //placing the appropriate clue numbers on inputs that need them.
        int num = 1;
        foreach (GameObject g in inputs)
        {
            _CrosswordSquare iLogic = g.GetComponent<_CrosswordSquare>();


            if (iLogic.GetAnswerChar() == '\0')
                Destroy(g);
            else
            {
                if (iLogic.GetIsWordStart())
                {
                    iLogic.SetWordNum(num);
                    num++;
                }
            }
        }

        //After the cleanup, the initial building of the board is complete.
        TitleText.text = crosswordPuzzleData.snippetName;
    }

    public void CheckForCorrectAnswer()
    {
        //Run through every space and, if all have the correct char, mark puzzle as complete.
        Debug.Log("Running CheckForCorrectanswer()");
        bool PuzzleSolved = true;
        _CrosswordSquare space = null;
        foreach (GameObject g in inputs)
        {
            if (g != null)
            {
                space = g.GetComponent<_CrosswordSquare>();
                if (space.GetAnswerChar() != space.GetCurrentChar())
                    PuzzleSolved = false;
            }
        }
        if (PuzzleSolved)
            EndGame();
    }

    public void DisplayClues(int across, int down)
    {
        if (across >= 0)
            AcrossClueText.text = "Across: " + cluesAcross[across];
        else
            AcrossClueText.text = "Across: ";


        if (down >= 0)
            DownClueText.text = "Down: " + cluesDown[down];
        else
            DownClueText.text = "Down: ";
    }

    public void EndGame()
    {
        foreach (GameObject g in inputs)
            if (g != null && g.GetComponentInChildren<TMP_InputField>() != null)
                g.GetComponentInChildren<TMP_InputField>().interactable = false;

        OnPuzzleSolved();
    }

    public void OnPuzzleSolved()
    {
        Debug.Log("OnPuzzleSolved ran in CrosswordSnippetBoard with slug: " + crosswordPuzzleData.snippetSlug);

        AudioManager.Instance.PlaySound("SFX_SnippetSolved");
        SnippetEvents.Instance.SnippetSolved(crosswordPuzzleData.snippetSlug);
    }

    public void OnPuzzleCompleted()
    {

    }

    //UnloadSnippet unloads all the buttons from the gameplay panel so they do not use resources. 
    public void UnloadSnippet()
    {
        foreach (GameObject b in inputs)
            Destroy(b);

        cluesDown.Clear();
        cluesAcross.Clear();
        
    }
}
