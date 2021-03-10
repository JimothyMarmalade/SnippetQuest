using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePickup : MonoBehaviour
{
    //PuzzleID instantiates to -1, which will result in no puzzles being added
    private GameController ControllerReference;
    public int PuzzleID = -1;

    //Called when the Player interacts with the Puzzle Pickup
    public void Collect()
    {
        //Do the required actions needed to inform game that it has been collected
        ControllerReference.ActivateSnippetButton(PuzzleID);
        ControllerReference.AdventureSFX.PlayOneShot(ControllerReference.PuzzleCollected);

        //Finally, Destroy gameObject
        Destroy(gameObject);
    }

    public void SetControllerReference(GameController c)
    {
        ControllerReference = c;
    }

}
