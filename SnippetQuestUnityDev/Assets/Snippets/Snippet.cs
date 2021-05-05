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

public class Snippet : MonoBehaviour
{
    //The slug is the "raw" name for the snippet data
    public string snippetSlug { get; set; }
    //Holds the type of puzzle in a string. Assigned in specific classes.
    public string snippetType { get; set; }
    //Holds a name for the puzzle. Can be a direct title or related to theme.
    public string snippetName { get; set; }
    //Master ID refers to the puzzle's ID number in relation to every other Snippet in-game.
    public int masterID { get; set; }
    //Type ID refers to the puzzle's ID number in relation to every other snippet of a similar type.
    public int typeID { get; set; }
    //puzzleSolved refers to wether the puzzle has ever been completed by the player
    public bool snippetSolved { get; set; }
    //numTimesSolved refers to number of times puzzle has been solved.
    public int numTimesSolved { get; set; }
    //bestTime refers to the shortest amount of time the player has taken to solve the puzzle.
    public float bestTime { get; set; }

    /*
    [Newtonsoft.Json.JsonConstructor]
    public Snippet(string snippetSlug, string snippetType, string snippetName, int masterID,
                   int typeID, bool snippetSolved, int numTimesSolved, float bestTime)
    {
        this.snippetSlug = snippetSlug;
        this.snippetType = snippetType;
        this.snippetName = snippetName;
        this.masterID = masterID;
        this.typeID = typeID;
        this.snippetSolved = snippetSolved;
        this.numTimesSolved = numTimesSolved;
        this.bestTime = bestTime;
    }
    */
    
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
