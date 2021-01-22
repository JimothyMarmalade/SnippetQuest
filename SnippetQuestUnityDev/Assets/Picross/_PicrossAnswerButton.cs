using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _PicrossAnswerButton : MonoBehaviour
{
    //Holds information and methods for the Picross Button

    private bool toggled = false;
    private bool canBeToggled = true;
    private _PicrossPuzzle controller;

    public void ToggleState()
    {
        //if the button is black, change it to white
        if (toggled)
        {
            toggled = false;
            GetComponent<Image>().color = Color.white;
        }
        //If the button is white, change it to black
        else if (!toggled)
        {
            toggled = true;
            GetComponent<Image>().color = Color.black;
        }

        controller.CheckWinCondition();

    }

    public void SetPuzzleControllerReference(_PicrossPuzzle c)
    {
        controller = c;
    }

    public bool GetToggle()
    {
        return toggled;
    }
}
