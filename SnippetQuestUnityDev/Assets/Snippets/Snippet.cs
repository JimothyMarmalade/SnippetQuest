/*
 * Created by Logan Edmund, 3/4/21
 * Last Modified by Logan Edmund, 5/20/21
 * 
 * Function of Snippet.cs is to hold data for each individual Snippet that can be inserted into puzzle boards. Variables stored here are common
 *  variables and will be held by each individual snippet. Snippet-specific information such as puzzle layout, solutions, etc.. will be held in
 *  their respective classes.
 * This data and inheriting classes are NOT INTENDED for gameplay purposes such as if it has been collected by the player or position in the world.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty Snippet", menuName = "Snippets/Empty")]
public class Snippet : ScriptableObject
{
    public enum SnippetType
    {
        NULL,
        Picross,
        Futoshiki,
        Crossword
    }


    [Header("Identification")]
    //The slug is the "raw" name for the snippet data
    public string snippetSlug;
    //Holds the type of puzzle in a string. Assigned in specific classes.
    public SnippetType snippetType;
    //Holds a name for the puzzle. Can be a direct title or related to theme.
    public string snippetName;
    //Master ID refers to the puzzle's ID number in relation to every other Snippet in-game.
    public int masterID;
    //Type ID refers to the puzzle's ID number in relation to every other snippet of a similar type.
    public int typeID;
    [Header("Player Impact")]
    //puzzleSolved refers to wether the puzzle has ever been completed by the player
    public bool snippetSolved;
    //numTimesSolved refers to number of times puzzle has been solved.
    public int numTimesSolved;
    //bestTime refers to the shortest amount of time the player has taken to solve the puzzle.
    public float bestTime;

    public Snippet()
    {
        this.snippetSlug = "NULLSLUG";
        this.snippetType = SnippetType.NULL;
        this.snippetName = "NO NAME";
        this.masterID = 0000;
        this.typeID = 000;
        this.snippetSolved = false;
        this.numTimesSolved = 0;
        this.bestTime = 0;
    }



    //----------Setters for information that can be modified
    public void SetSnippetSolved(bool b)
    {
        snippetSolved = b;
    }

    public void IncrementNumTimesSolved()
    {
        numTimesSolved++;
    }

    public void SetBestTime(float f)
    {
        bestTime = f;
    }

    //Clears all player impact data
    public void ResetPlayerInformation()
    {
        this.snippetSolved = false;
        this.numTimesSolved = 0;
        this.bestTime = 0;
    }

    public virtual bool CheckCriticalInformation()
    {
        Debug.LogWarning("Running CheckCriticalInformation on an empty snippet");
        return false;
    }

    //----------Other Methods
    public void PrintInfoToConsole()
    {
        Debug.Log("Snippet name: " + snippetName + ", SnippetType: " + snippetType + ", SnippetMasterID: " + masterID);
    }
}
