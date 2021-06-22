/*
 * Created by Logan Edmund, 6/21/21
 * Last Modified by Logan Edmund, 6/21/21
 * 
 * Controller for the Tic-Tac-Toe minigame. Handles all game logic, opponent's moves, etc.
 * Controller assumes the game exists in a 3D space with 3D representaitons of players, pieces, and so on.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeController : MonoBehaviour
{
    public GameObject[] XPieces = new GameObject[5];
    public GameObject[] OPieces = new GameObject[5];

    private Vector3[] XStartLocations = new Vector3[5]; 
    private Vector3[] OStartLocations = new Vector3[5];

    public TicTacToeGridspace[] spaces = new TicTacToeGridspace[9];

    public bool gameOver;
    bool humanWins;
    bool AIWins;
    public int gameTurns = 0;
    public int player1Turns = 0;
    public int player2Turns = 0;
    public bool isHumanPlayerTurn = true;
    public TicTacToeGridspace.PlacedPiece HumanPlayerSide; 
    public TicTacToeGridspace.PlacedPiece AIPlayerSide;

    private enum TicTacToeAI {Random, Blueballer}
    private TicTacToeAI currentAI;


    //Array of locations for the pieces to be placed

    private int XMoves = 0;
    private int OMoves = 0;

    public void Start()
    {
        //Set up transforms
        for (int i = 0; i < 5; i++)
        {
            XStartLocations[i] = XPieces[i].transform.position;
            OStartLocations[i] = OPieces[i].transform.position;
        }
        foreach (TicTacToeGridspace g in spaces)
            g.SetTTTControllerReference(this);

        isHumanPlayerTurn = true;
        HumanPlayerSide = TicTacToeGridspace.PlacedPiece.X;
        AIPlayerSide = TicTacToeGridspace.PlacedPiece.O;

        gameOver = false;
        currentAI = TicTacToeAI.Random;
    }

    public void Update()
    {
        if (!gameOver)
        {
            CheckPlayerMouseClick();

            if (!isHumanPlayerTurn && !gameOver)
                AIPlayerTurn(AIPlayerPlaceDecision());

            if (gameTurns >= 9)
                CheckWinCondition();
        }
    }

    private void CheckPlayerMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clicking Mouse.");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TicTacToeGridspace g = hit.transform.GetComponent<TicTacToeGridspace>();
                if (g != null)
                {
                    HumanPlayerTurn(g);
                    PrintBoardStatus();
                }
            }
        }
    }

    private void HumanPlayerTurn(TicTacToeGridspace g)
    {
        //If the player attempts to place a piece and it can be placed at that location:
        if (isHumanPlayerTurn)
        {
            if (g.TrySetPiece(TicTacToeGridspace.PlacedPiece.X, XPieces[player1Turns]))
            {
                player1Turns++;
                gameTurns++;
                isHumanPlayerTurn = !isHumanPlayerTurn;
            }
            else
            {
                Debug.Log("Can't place piece there!");
            }

            if (gameTurns > 4)
                CheckWinCondition();
        }
        //else do nothing -- wait for Ai to take its turn
    }

    private void AIPlayerTurn(TicTacToeGridspace g)
    {
        if (g.TrySetPiece(TicTacToeGridspace.PlacedPiece.O, OPieces[player2Turns]))
        {
            player2Turns++;
            gameTurns++;
            isHumanPlayerTurn = !isHumanPlayerTurn;
        }

        if (gameTurns > 4)
            CheckWinCondition();
    }

    //Places a piece on the board based on a desicion made by an AI.
    private TicTacToeGridspace AIPlayerPlaceDecision()
    {
        if (currentAI == TicTacToeAI.Random)
        {
            //Random will randomly place a piece down based on the current available spaces.
            List<TicTacToeGridspace> openSpaces = new List<TicTacToeGridspace>();
            foreach(TicTacToeGridspace g in spaces)
                if (g.piece == TicTacToeGridspace.PlacedPiece.None)
                    openSpaces.Add(g);
            if (openSpaces.Count > 0)
            {
                int r = openSpaces.Count;
                r = Random.Range(0, r);
                return openSpaces[r];
            }
            else
                return null;
        }
        //If there is no AI selected, place a piece at the first open spot.
        else
        {
            foreach (TicTacToeGridspace g in spaces)
                if (g.piece == TicTacToeGridspace.PlacedPiece.None)
                    return g;
        }

        return null;
    }



    //Checks all possible win conditions to see if the game is over.
    private void CheckWinCondition()
    {
        if (spaces[0].piece == HumanPlayerSide && spaces[1].piece == HumanPlayerSide && spaces[2].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }
        else if (spaces[3].piece == HumanPlayerSide && spaces[4].piece == HumanPlayerSide && spaces[5].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }
        else if (spaces[6].piece == HumanPlayerSide && spaces[7].piece == HumanPlayerSide && spaces[8].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }
        else if (spaces[0].piece == HumanPlayerSide && spaces[3].piece == HumanPlayerSide && spaces[6].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }
        else if (spaces[1].piece == HumanPlayerSide && spaces[4].piece == HumanPlayerSide && spaces[7].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }
        else if (spaces[2].piece == HumanPlayerSide && spaces[5].piece == HumanPlayerSide && spaces[8].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }
        else if (spaces[0].piece == HumanPlayerSide && spaces[4].piece == HumanPlayerSide && spaces[8].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }
        else if (spaces[2].piece == HumanPlayerSide && spaces[4].piece == HumanPlayerSide && spaces[6].piece == HumanPlayerSide)
        {
            gameOver = true;
            humanWins = true;
        }


        if (spaces[0].piece == AIPlayerSide && spaces[1].piece == AIPlayerSide && spaces[2].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }
        else if (spaces[3].piece == AIPlayerSide && spaces[4].piece == AIPlayerSide && spaces[5].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }
        else if (spaces[6].piece == AIPlayerSide && spaces[7].piece == AIPlayerSide && spaces[8].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }
        else if (spaces[0].piece == AIPlayerSide && spaces[3].piece == AIPlayerSide && spaces[6].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }
        else if (spaces[1].piece == AIPlayerSide && spaces[4].piece == AIPlayerSide && spaces[7].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }
        else if (spaces[2].piece == AIPlayerSide && spaces[5].piece == AIPlayerSide && spaces[8].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }
        else if (spaces[0].piece == AIPlayerSide && spaces[4].piece == AIPlayerSide && spaces[8].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }
        else if (spaces[2].piece == AIPlayerSide && spaces[4].piece == AIPlayerSide && spaces[6].piece == AIPlayerSide)
        {
            gameOver = true;
            AIWins = true;
        }

        if (gameOver || gameTurns >= 9)
            GameOver();
    }

    private void GameOver()
    {
        if (humanWins)
            Debug.Log("Human Player Wins!");
        else if (AIWins)
            Debug.Log("AI Wins!");
        else if (!humanWins && !AIWins)
        {
            gameOver = true;
            Debug.Log("Tie!");
        }

        foreach (TicTacToeGridspace t in spaces)
        {
            t.locked = true;
        }
    }


    public void ResetGame()
    {
        Debug.Log("Resetting Game...");
        foreach (TicTacToeGridspace t in spaces)
        {
            t.ClearGridspace();
        }

        for (int i = 0; i < 5; i++)
        {
            XPieces[i].transform.position = XStartLocations[i];
            OPieces[i].transform.position = OStartLocations[i];
        }

        gameTurns = 0;
        player1Turns = 0;
        player2Turns = 0;
        gameOver = false;
        humanWins = false;
        AIWins = false;
    }

    //Prints the current output of the board to the console.
    private void PrintBoardStatus()
    {
        string boardStatus = "";
        foreach (TicTacToeGridspace t in spaces)
        {
            boardStatus += t.piece.ToString();
        }
        Debug.Log(boardStatus);
    }

    //Returns to main menu
    public void ReturnToMainMenu()
    {
        GameManager.Instance.GoToScene(SceneHandler.Scene.Minigame_LeadParkTicTacToe);
    }
}