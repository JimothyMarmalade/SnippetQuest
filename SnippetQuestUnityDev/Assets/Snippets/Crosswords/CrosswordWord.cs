/*
 * Created by Logan Edmund, 7/17/21
 * Last Modified by Logan Edmund, 7/17/21
 * 
 * Data container for words in crossword puzzles. Holds the word itself, a clue, and it's position on the board.
 * Also holds whether the word is inked in at the start of the puzzle or not.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CrosswordWord
{
    public string Word;
    public string Clue;
    public Vector2 Location;
    public bool VisibleAtStart;
}
