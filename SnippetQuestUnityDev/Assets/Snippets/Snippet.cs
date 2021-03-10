/*
 * Created by Logan Edmund, 3/4/21
 * Last Modified by Logan Edmund, 3/4/21
 * 
 * Function of Snippet.cs is to hold data for each individual Snippet that can be inserted into puzzle boards. Variables stored here are common
 *  variables and will be held by each individual snippet. Snippet-specific information such as puzzle layout, solutions, etc.. will be held in
 *  their respective classes.
 * This data and inheriting classes are NOT INTENDED for gameplay purposes such as if it has been collected by the player or position in the world.
 * 
 */ 

using UnityEngine;

public class Snippet : ScriptableObject
{
    //Holds the type of puzzle in a string. Assigned in specific classes.
    public string snippetType = "";
    //Holds a name for the puzzle. Can be a direct title or related to theme.
    public string snippetName = "";
    //Master ID refers to the puzzle's ID number in relation to every other Snippet in-game.
    public int masterID;
    //Type ID refers to the puzzle's ID number in relation to every other snippet of a similar type.
    public int typeID;
    //puzzleSolved refers to wether the puzzle has ever been completed by the player
    public bool snippetSolved;
    //numTimesSolved refers to number of times puzzle has been solved.
    public int numTimesSolved;
    //bestTime refers to the shortest amount of time the player has taken to solve the puzzle.
    public float bestTime;

    //----------Method that manually sets type
    public void DefineSnippetType(string s)
    {
        if (s != snippetType)
        {
            snippetType = s;
        }
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


    //----------Other Methods
    public void PrintInfoToConsole()
    {
        Debug.Log("Snippet name: " + snippetName + ", SnippetType: " + snippetType + ", SnippetMasterID: " + masterID);
    }
}
