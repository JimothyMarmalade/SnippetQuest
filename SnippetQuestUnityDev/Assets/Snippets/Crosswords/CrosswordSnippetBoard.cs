using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Last edited by Logan Edmund, 5/4/21


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
    private GameObject[,] inputs;

    private CrosswordSnippet CurrentSnippetData;


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
        //First, the board defines the size of crosswordGridButtons based on the input data.
        float totalsize = gridPanel.sizeDelta.x;
        Debug.Log("gridPanel TotalSize: " + totalsize);
        float inputSize = totalsize/crosswordPuzzleData.GridLength;
        Debug.Log("inputSize: " + inputSize);

        //With that done, we can now generate prefab buttons to fill the grid.
        inputs = new GameObject[crosswordPuzzleData.GridLength, crosswordPuzzleData.GridLength];

        float startLocX = inputSize/2;
        float startLocY = inputSize/2;
        Debug.Log("startLocX = -" + totalsize + "/2 + " + inputSize + "/2");


        //Build all needed input spaces, place them on the board, and assign them to the array
        for (int row = 0; row < crosswordPuzzleData.GridLength; row++)
        {
            for (int col = 0; col < crosswordPuzzleData.GridLength; col++)
            {
                //Calculate the Y value for the row
                GameObject a = Instantiate(crosswordGridSpacePrefab);
                //Set the button as a child of the grid, NOT the overall board
                a.transform.SetParent(gridPanel.transform, false);
                //Set the size and position of the button
                a.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(inputSize, inputSize);

                a.transform.localPosition = new Vector2((col * inputSize) + (inputSize / 2),
                                                        (totalsize / 2) - (row * inputSize) - (inputSize / 2));

                a.GetComponent<_CrosswordSquare>().SetPuzzleControllerReference(this);
                a.GetComponent<_CrosswordSquare>().NumCheck();

                inputs[col, row] = a;
            }
        }
        //With the buttons made, we can begin setting those used in the puzzle with correct answers.
        //Scrub through the WordsAcross array to get words, location on board, size of word, etc..
        for (int i = 0; i < crosswordPuzzleData.WordsAcross.Length; i++)
        {
            string word = crosswordPuzzleData.WordsAcross[i];
            int locx = crosswordPuzzleData.WordsAcrossLoc[i].x;
            int locy = crosswordPuzzleData.WordsAcrossLoc[i].y;

            int wordLength = word.Length;
            //Starting at the startLoc and going right, the input correct answers are assigned the letters given
            for (int s = 0; s < wordLength; s++)
            {
                _CrosswordSquare iLogic = inputs[locx+s, locy].GetComponent<_CrosswordSquare>();
                iLogic.SetAnswerChar(word[s]);
                iLogic.SetAcrossWordNum(i+1);

                if (s == 0)
                    iLogic.SetIsWordStart(true);

            }
        }
        //All across words are set. Now do a double check with down words to ensure compatibility
        for (int i = 0; i < crosswordPuzzleData.WordsDown.Length; i++)
        {
            string word = crosswordPuzzleData.WordsDown[i];
            int locx = crosswordPuzzleData.WordsDownLoc[i].x;
            int locy = crosswordPuzzleData.WordsDownLoc[i].y;

            int wordLength = word.Length;
            //Starting at the startLoc and going down, the input correct answers are assigned the letters given
            for (int s = 0; s < wordLength; s++)
            {
                _CrosswordSquare iLogic = inputs[locx, locy+s].GetComponent<_CrosswordSquare>();
                if (iLogic.GetAnswerChar() != word[s])
                {
                    Debug.Log("Very bad error! Crossword puzzle has conflicting answers at" + locx + "," + (locy+s));
                }
                else
                {
                    iLogic.SetDownWordNum(i+1);
                    if (s == 0)
                        iLogic.SetIsWordStart(true);
                }
            }
        }
        //With all the buttons set, it's time to tidy up the board by removing inputs with no clues and
        //placing the appropriate clue numbers on inputs that need them.
        int clueNum = 1;
        for (int i = 0; i < crosswordPuzzleData.GridLength; i++)
        {
            for (int j = 0; j < crosswordPuzzleData.GridLength; j++)
            {
                _CrosswordSquare iLogic = inputs[j, i].GetComponent<_CrosswordSquare>();
                if (iLogic.GetAnswerChar() == '\0')
                    Destroy(iLogic.gameObject);
                else
                {
                    if (iLogic.GetIsWordStart())
                    {
                        iLogic.SetWordNum(clueNum);
                        clueNum++;
                    }
                }      
            }
        }
        //After the cleanup, the initial building of the board is complete.
        TitleText.text = crosswordPuzzleData.snippetName;


        //Offload the puzzle's clues into the local arrays
        string[] cap = SnippetDatabase.Instance.GetCrosswordSnippet(crosswordSlug).CluesAcross;
        string[] cdp = SnippetDatabase.Instance.GetCrosswordSnippet(crosswordSlug).CluesDown;
        cluesAcross = new List<string>();
        cluesDown = new List<string>();

        for (int z = 0; z < cap.Length; z++)
            cluesAcross.Add(cap[z]);


        for (int z = 0; z < cdp.Length; z++)
            cluesDown.Add(cdp[z]);
    }

    public void CheckForCorrectAnswer()
    {
        //Run through every space and, if all have the correct char, mark puzzle as complete.
        bool PuzzleSolved = true;
        _CrosswordSquare iLogic = null;
        for (int i = 0; i < crosswordPuzzleData.GridLength; i++)
        {
            for (int j = 0; j < crosswordPuzzleData.GridLength; j++)
            {
                if (inputs[j, i] != null)
                {
                    iLogic = inputs[j, i].GetComponent<_CrosswordSquare>();
                    if (iLogic.GetAnswerChar() != iLogic.GetCurrentChar())
                        PuzzleSolved = false;
                }
            }
        }
        Debug.Log("Puzzle Solved: " + PuzzleSolved);
    }

    public void DisplayClues(int across, int down)
    {
        AcrossClueText.text = "Across: " + cluesAcross[across-1];
        DownClueText.text = "Down: " + cluesDown[down-1];
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
