/*
 * Created by Logan Edmund, 6/21/21
 * Last Modified by Logan Edmund, 6/21/21
 * 
 * Gridspace for 3D Tic Tac Toe
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeGridspace : MonoBehaviour
{
    public enum PlacedPiece {None, X, O}
    public PlacedPiece piece;
    public bool locked;

    private TicTacToeController controllerReference;

    private void Start()
    {
        piece = PlacedPiece.None;
        locked = false;
    }

    public void SetTTTControllerReference(TicTacToeController c)
    {
        controllerReference = c;
    }

    public void DebugYell()
    {
        Debug.Log("Mouse hovering over a TTT space");
    }


    public bool TrySetPiece(PlacedPiece p, GameObject g)
    {
        if (!locked)
        {
            piece = p;
            g.transform.position = this.gameObject.transform.position;
            locked = true;
            return true;
        }
        else
        {
            Debug.Log("Cannot place piece there!");
            return false;
        }
    }

    public void ClearGridspace()
    {
        piece = PlacedPiece.None;
        locked = false;
    }

    public PlacedPiece GetPiece()
    {
        return piece;
    }

}
