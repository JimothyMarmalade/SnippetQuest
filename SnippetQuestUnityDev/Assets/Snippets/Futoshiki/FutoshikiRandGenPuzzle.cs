using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


//Convienience structure for passing information
struct Cell
{
    public int row, col;
    public Cell (int r, int c)
    {
        row = r;
        col = c;
    }
}




public class FutoshikiRandGenPuzzle : _FutoshikiPuzzle
{

    //stores values for every item on the board for random generation purposes
    public class Candidate : IEnumerable
    {
        bool[] m_values;
        int m_count;
        int m_numCandidates;

        public int Count{ get { return m_count; } }

        public Candidate(int numCandidates, bool initialValue)
        {
            m_values = new bool[numCandidates];
            m_count = 0;
            m_numCandidates = numCandidates;

            for (int i = 1; i <= numCandidates; i++)
                this[i] = initialValue;
        }

        public bool this[int key]
        {
            // Allows candidates to be referenced by actual value (i.e. 1-9, rather than 0 - 8)
            get { return m_values[key - 1]; }

            // Automatically tracks the number of candidates
            set
            {
                m_count += (m_values[key - 1] == value) ? 0 : (value == true) ? 1 : -1;
                m_values[key - 1] = value;
            }
        }

        public void SetAll(bool value)
        {
            for (int i = 1; i <= m_numCandidates; i++)
                this[i] = value;
        }

        public override string ToString()
        {
            string values = "";
            foreach (int candidate in this)
                values += candidate;
            return values.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return new CandidateEnumerator(this);
        }

        // Enumerator simplifies iterating over candidates
        private class CandidateEnumerator : IEnumerator
        {
            private int m_position;
            private Candidate m_c;

            public CandidateEnumerator(Candidate c)
            {
                m_c = c;
                m_position = 0;
            }

            // only iterates over valid candidates
            public bool MoveNext()
            {
                ++m_position;
                if (m_position <= m_c.m_numCandidates)
                {
                    if (m_c[m_position] == true)
                        return true;
                    else
                        return MoveNext();
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                m_position = 0;
            }

            public object Current
            {
                get { return m_position; }
            }
        }
    }

    // True values for row, grid, and region constraint matrices
    // mean that they contain that candidate, inversely,
    // True values in the candidate constraint matrix means that it
    // is a possible value for that cell.
    Candidate[,] m_cellConstraintMatrix;
    Candidate[] m_rowConstraintMatrix;
    Candidate[] m_colConstraintMatrix;

    //Convienience structure holding the r and c of grid spaces
    struct Cell
    {
        public int row, col;
        public Cell(int r, int c) { row = r; col = c; }
    }

    // helps avoid iterating over solved squares
    HashSet<Cell> solved;
    HashSet<Cell> unsolved;

    // Tracks the cells changed due to propagation (i.e. the rippled cells)
    Stack<HashSet<Cell>> changed;

    [Header("Size of the generated board")]
    //Size of the grid
    public int gridSizeRandGen;
    //Arrays to eventually hold all values needed to create starting clues
    private char[,] filledClueKeyFutoshikiRandGen;
    private int[,] diminishedAnswerKeyFutoshikiRandGen;
    private char[,] diminishedClueKeyFutoshikiRandGen;

    public enum Difficulty { Easy, Medium, Hard };
    public Difficulty currentDifficulty;

    //The actual generated number grid
    private int[,] generatedNumberGrid;

    int stepsTaken;
    
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Start()
    {
        //1. On start, program should build and fill a new board of its own accord
        BuildNewBoard();
        FillBoardRecurse(NextCell());
        DEBUGPrintFilledGrid();

        //2. Now that the board is filled, all possible clues can be generated and placed in the clue array
        BuildClueKey();

        //3. With a default number grid and clueKey built, we can now remove numbers and clues to create the puzzle.
        BuildDiminishedAnswerKey();
        BuildDiminishedClueKey();

        //4. Finally, take all the information and pass it to the master file so it can be displayed in-game
        SetGridSize(gridSizeRandGen);
        SetAnswerButtonsArray(gridSizeRandGen);
        SetClueButtonsArray(gridSizeRandGen);

        SetPresetAnswerKey(diminishedAnswerKeyFutoshikiRandGen);
        SetPresetClueKey(diminishedClueKeyFutoshikiRandGen);

        BuildFutoshikiBoard();
        AssignAllButtonsController(this);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //Takes the newly-built futoshiki board and calculates a clue key based on the filling.
    private void BuildClueKey()
    {
        //Creates cluekey Array based on gridsize. 
        filledClueKeyFutoshikiRandGen = new char[(gridSizeRandGen * 2) - 1, gridSizeRandGen];

        for (int row = 0; row < (gridSizeRandGen * 2) - 1; row++)
        {
            for (int col = 0; col < gridSizeRandGen; col++)
            {
                //If the clue is on an even row in the final column, the clue is automatically N as it cannot exist
                if (row % 2 == 0 && col == gridSizeRandGen - 1)
                    filledClueKeyFutoshikiRandGen[row, col] = 'n';

                //Each clue will be a pointer with the open end directed towards the larger number and the point pointing at the smaller number.
                //Clues on even rows (0, 2, 4) can only point left or right, and there will be gridSize-1 clues
                else if (row % 2 == 0 && col != gridSizeRandGen - 1)
                {
                    //Debug.Log("BuildClueKey(): row, col = " + row + ", " + col);
                    int left = generatedNumberGrid[row / 2, col];
                    int right = generatedNumberGrid[row / 2, col + 1];

                    if (left > right)
                        filledClueKeyFutoshikiRandGen[row, col] = 'r';
                    else
                        filledClueKeyFutoshikiRandGen[row, col] = 'l';
                }
                //Clues on odd rows can only point up or down, and there will be gridSize clues.
                else if (row % 2 != 0)
                {
                    //Debug.Log("BuildClueKey(): row, col = " + row + ", " + col);
                    int top = generatedNumberGrid[(row - 1) / 2, col];
                    int bottom = generatedNumberGrid[(row + 1) / 2, col];

                    if (top > bottom)
                        filledClueKeyFutoshikiRandGen[row, col] = 'd';
                    else
                        filledClueKeyFutoshikiRandGen[row, col] = 'u';
                }
            }
        }
        //And the completed clue sheet -- in this case, a simple representation of greaters and lessers -- is complete.

    }
    
    private void BuildDiminishedAnswerKey()
    {
        //With the passed size, we already know the final size of the answerkey
        diminishedAnswerKeyFutoshikiRandGen = new int[gridSizeRandGen, gridSizeRandGen];
        List<(int, int)> chosenVals = new List<(int, int)>();

        //With the original grid available, we can pick random numbers to add to the Key.
        int remainingNumbers;
        switch (currentDifficulty)
        {
            case Difficulty.Easy: remainingNumbers = gridSizeRandGen * 2;
                break;

            case Difficulty.Medium: remainingNumbers = gridSizeRandGen;
                break;

            case Difficulty.Hard: remainingNumbers = gridSizeRandGen / 2;
                break;

            default: remainingNumbers = gridSizeRandGen * 2;
                break;
        }

        int loops = -1;
        while (remainingNumbers > 0)
        {
            loops++;
            int x = Random.Range(0, gridSizeRandGen);
            int y = Random.Range(0, gridSizeRandGen);
            (int, int) r = (x, y);

            if (!chosenVals.Contains(r))
            {
                diminishedAnswerKeyFutoshikiRandGen[x, y] = generatedNumberGrid[x, y];
                remainingNumbers--;
                chosenVals.Add(r);
            }
        }
        Debug.Log("BuildDiminishedAnswerKey() loops: " + loops);
    }

    private void BuildDiminishedClueKey()
    {
        //With the passed size, we already know the final size of the answerkey
        diminishedClueKeyFutoshikiRandGen = new char[(gridSizeRandGen * 2) - 1, gridSizeRandGen];
        for (int row = 0; row < (gridSizeRandGen * 2)-1; row++)
        {
            for (int col = 0; col < gridSizeRandGen; col++)
            {
                diminishedClueKeyFutoshikiRandGen[row, col] = 'n';
            }
        }

        List<(int, int)> chosenVals = new List<(int, int)>();

        //With the original grid available, we can pick random numbers to add to the Key.
        int remainingClues;
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                remainingClues = gridSizeRandGen * 2;
                break;

            case Difficulty.Medium:
                remainingClues = gridSizeRandGen;
                break;

            case Difficulty.Hard:
                remainingClues = gridSizeRandGen / 2;
                break;

            default:
                remainingClues = gridSizeRandGen * 2;
                break;
        }
        int loops = -1;
        while (remainingClues > 0)
        {
            loops++;
            int x = Random.Range(0, (gridSizeRandGen*2)-1);
            int y = Random.Range(0, gridSizeRandGen);
            (int, int) r = (x, y);

            //This If ensures the "impossible sixth" clue of even rows will never be used
            if (!(x % 2 == 0 && y == gridSizeRandGen-1))
            {
                if (!chosenVals.Contains(r))
                {
                    //First, check to make sure the location isn't between two pre-filled answer key spots. If so,
                    // The clue would be useless and shouldn't be considered.
                    //If the spot is in an even row, it will be pointing left/right
                    if (x % 2 == 0)
                    {
                        //If BOTH answers are not equal to zero, meaing they both have preset answers, there should not be a clue placed there.
                        if (diminishedAnswerKeyFutoshikiRandGen[x/2, y] != 0 && diminishedAnswerKeyFutoshikiRandGen[x/2, y + 1] != 0)
                        {
                            Debug.Log("Found improper placement at " + r.ToString() + ", ignoring value.");
                            chosenVals.Add(r);
                        }
                        else
                        {
                            diminishedClueKeyFutoshikiRandGen[x, y] = filledClueKeyFutoshikiRandGen[x, y];
                            remainingClues--;
                            chosenVals.Add(r);
                        }
                    }
                    else if (x % 2 != 0)
                    {
                        if (diminishedAnswerKeyFutoshikiRandGen[(x-1)/2, y] != 0 && diminishedAnswerKeyFutoshikiRandGen[(x+1)/2, y] != 0)
                        {
                            Debug.Log("Found improper placement at " + r.ToString() + ", ignoring value.");
                            chosenVals.Add(r);
                        }
                        else
                        {
                            diminishedClueKeyFutoshikiRandGen[x, y] = filledClueKeyFutoshikiRandGen[x, y];
                            remainingClues--;
                            chosenVals.Add(r);
                        }
                    }
                }
            }
        }
        Debug.Log("BuildDiminishedClueKey() Loops: " + loops);

    }

    //Destroys the current puzzle and generates a new one.
    public void GenerateNewPuzzle()
    {
        //Destroy old Puzzle
        DestroyPuzzle();

        //Repeat all the steps from the start method.
        BuildNewBoard();
        FillBoardRecurse(NextCell());
        DEBUGPrintFilledGrid();

        BuildClueKey();

        BuildDiminishedAnswerKey();
        BuildDiminishedClueKey();

        SetGridSize(gridSizeRandGen);
        SetAnswerButtonsArray(gridSizeRandGen);
        SetClueButtonsArray(gridSizeRandGen);

        SetPresetAnswerKey(diminishedAnswerKeyFutoshikiRandGen);
        SetPresetClueKey(diminishedClueKeyFutoshikiRandGen);

        BuildFutoshikiBoard();
        AssignAllButtonsController(this);
    }



    //Outputs the board filling to the console after successful completion
    private void DEBUGPrintFilledGrid()
    {
        string sol = "";
        int i = 0;
        foreach (int num in generatedNumberGrid)
        {
            if (i > gridSizeRandGen - 1)
            {
                sol += ", ";
                i = 0;
            }
            sol += num;
            i++;
        }
        Debug.Log("Steps to build grid: " + stepsTaken);
        Debug.Log("Filled Grid Output: " + sol);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Sudoku Solver template code below -- may have potential as an automatic completer, but needs to be modified to take into account
    //Lesser/Greater constraints. Currently used to generate filled boards from empty ones. -- 9/24/20
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void BuildNewBoard()
    {
        //Next, the board itself must be generated and instantiated to zero.
        generatedNumberGrid = new int[gridSizeRandGen, gridSizeRandGen];

        //As should all the cell constraint matricies.
        m_cellConstraintMatrix = new Candidate[gridSizeRandGen, gridSizeRandGen];
        m_rowConstraintMatrix = new Candidate[gridSizeRandGen];
        m_colConstraintMatrix = new Candidate[gridSizeRandGen];

        solved = new HashSet<Cell>();
        unsolved = new HashSet<Cell>();
        changed = new Stack<HashSet<Cell>>();
        stepsTaken = 0;

        // initialize constraints -- give each space on the board the ability to choose any free number
        for (int row = 0; row < gridSizeRandGen; row++)
        {
            for (int col = 0; col < gridSizeRandGen; col++)
            {
                // turn on all Candidates for every cell
                m_cellConstraintMatrix[row, col] = new Candidate(gridSizeRandGen, true);
            }
        }

        for (int i = 0; i < gridSizeRandGen; i++)
        {
            m_rowConstraintMatrix[i] = new Candidate(gridSizeRandGen, false);
            m_colConstraintMatrix[i] = new Candidate(gridSizeRandGen, false);
        }

        InitializeMatrices();
        PopulateCandidates();
    }

    //Initializes matrices to determine what numbers are placed on the starting board. This shouldn't be needed for random board generation,
    //but may be useful for auto-solving further down the line.
    private void InitializeMatrices()
    {
        for (int row = 0; row < gridSizeRandGen; row++)
        {
            for (int col = 0; col < gridSizeRandGen; col++)
            {
                // if the square is solved update the candidate list
                // for the row, column, and region
                if (generatedNumberGrid[row, col] > 0)
                {
                    int candidate = generatedNumberGrid[row, col];
                    m_rowConstraintMatrix[row][candidate] = true;
                    m_colConstraintMatrix[col][candidate] = true;
                }
            }
        }
    }

    private void PopulateCandidates()
    {
        //Add possible candidates by checking
        //the rows, columns and grid
        for (int row = 0; row < gridSizeRandGen; row++)
        {
            for (int col = 0; col < gridSizeRandGen; col++)
            {
                //if solved, then there are no possible candidates
                if (generatedNumberGrid[row, col] > 0)
                {
                    m_cellConstraintMatrix[row, col].SetAll(false);
                    solved.Add(new Cell(row, col));
                }
                else
                {
                    // populate each cell with possible candidates
                    // by checking the row and col associated 
                    // with that cell
                    foreach (int candidate in m_rowConstraintMatrix[row])
                        m_cellConstraintMatrix[row, col][candidate] = false;
                    foreach (int candidate in m_colConstraintMatrix[col])
                        m_cellConstraintMatrix[row, col][candidate] = false;

                    Cell c = new Cell(row, col);
                    unsolved.Add(c);
                }
            }
        }
    }

    //determines the next cell to fill by finding one with the least number of potential inputs
    private Cell NextCell()
    {
        if (unsolved.Count == 0)
            return new Cell(-1, -1); // signals that there are no next cells and the puzzle is solved

        Cell min = unsolved.ElementAt(Random.Range(0, unsolved.Count));
        foreach (Cell cell in unsolved)
        {
            if (Random.Range(0, 2) == 1)
                min = (m_cellConstraintMatrix[cell.row, cell.col].Count < m_cellConstraintMatrix[min.row, min.col].Count) ? cell : min;
        }

        return min;
    }

    private void SelectCandidate(Cell aCell, int candidate)
    {
        HashSet<Cell> changedCells = new HashSet<Cell>();

        // place candidate on grid
        generatedNumberGrid[aCell.row, aCell.col] = candidate;

        // remove candidate from cell constraint matrix
        m_cellConstraintMatrix[aCell.row, aCell.col][candidate] = false;

        // add the candidate to the cell, row, col, region constraint matrices
        m_colConstraintMatrix[aCell.col][candidate] = true;
        m_rowConstraintMatrix[aCell.row][candidate] = true;

        /**** RIPPLE ACROSS COL, ROW ****/

        // (propagation)
        // remove candidates across unsolved cells in the same
        // row and col.
        for (int i = 0; i < gridSizeRandGen; i++)
        {
            // only change unsolved cells containing the candidate
            if (generatedNumberGrid[aCell.row, i] == 0)
            {
                if (m_cellConstraintMatrix[aCell.row, i][candidate] == true)
                {
                    // remove the candidate
                    m_cellConstraintMatrix[aCell.row, i][candidate] = false;

                    //update changed cells (for backtracking)
                    changedCells.Add(new Cell(aCell.row, i));
                }
            }
            // only change unsolved cells containing the candidate
            if (generatedNumberGrid[i, aCell.col] == 0)
            {
                if (m_cellConstraintMatrix[i, aCell.col][candidate] == true)
                {
                    // remove the candidate
                    m_cellConstraintMatrix[i, aCell.col][candidate] = false;

                    //update changed cells (for backtracking)
                    changedCells.Add(new Cell(i, aCell.col));
                }
            }
        }

        // add cell to solved list
        unsolved.Remove(aCell);
        solved.Add(aCell);
        changed.Push(changedCells);
    }

    private void UnselectCandidate(Cell aCell, int candidate)
    {
        // 1) Remove selected candidate from grid
        generatedNumberGrid[aCell.row, aCell.col] = 0;

        // 2) Add that candidate back to the cell constraint matrix.
        //    Since it wasn't selected, it can still be selected in the 
        //    future
        m_cellConstraintMatrix[aCell.row, aCell.col][candidate] = true;

        // 3) Remove the candidate from the row and col constraint matrices
        m_rowConstraintMatrix[aCell.row][candidate] = false;
        m_colConstraintMatrix[aCell.col][candidate] = false;

        // 4) Add the candidate back to any cells that changed from
        //    its selection (propagation).
        foreach (Cell c in changed.Pop())
        {
            m_cellConstraintMatrix[c.row, c.col][candidate] = true;
        }

        // 5) Add the cell back to the list of unsolved
        solved.Remove(aCell);
        unsolved.Add(aCell);
    }

    //Uses recursion to fill the board.
    private bool FillBoardRecurse(Cell nextCell)
    {
        // Our base case: No more unsolved cells to select, 
        // thus puzzle solved
        if (nextCell.row == -1)
            return true;

        // Loop through all candidates in the cell
        foreach (int candidate in m_cellConstraintMatrix[nextCell.row, nextCell.col])
        {
            stepsTaken++;
            /*
            string stepDetail = "";
            stepDetail = stepDetail + "{" + stepsTaken.ToString() + "} -> ({";
            stepDetail = stepDetail + nextCell.row + "}, {" + nextCell.col + "}) : {";
            stepDetail = stepDetail + m_cellConstraintMatrix[nextCell.row, nextCell.col] + "} ({";
            stepDetail = stepDetail + m_cellConstraintMatrix[nextCell.row, nextCell.col].Count + "})";
            Debug.Log(stepDetail);
            */

            SelectCandidate(nextCell, candidate);

            // Move to the next cell.
            // if it returns false backtrack
            if (FillBoardRecurse(NextCell()) == false)
            {
                ++stepsTaken;
                //Debug.Log("{" + stepsTaken + "} -> BACK");
                UnselectCandidate(nextCell, candidate);
                continue;
            }
            else // if we recieve true here this means the puzzle was solved earlier
                return true;
        }

        // return false if path is unsolvable
        return false;

    }

}