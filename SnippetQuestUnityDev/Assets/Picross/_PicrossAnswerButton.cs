using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _PicrossAnswerButton : MonoBehaviour
{
    //Holds information and methods for the Picross Button

    enum ButtonState {Blank, Marked, Crossed}
    ButtonState currentState = ButtonState.Blank;

    private bool toggled = false;
    private bool canBeToggled = true;
    public GameObject XText;


    private _PicrossPuzzle controller;

    public void ToggleState()
    {
        //On successive clicks, button changes from Blank -> Marked -> Crossed
        if (currentState == ButtonState.Blank)
        {
            //Switch to Marked
            toggled = true;
            GetComponent<Image>().color = Color.black;
            XText.SetActive(false);
            currentState = ButtonState.Marked;
        }
        else if (currentState == ButtonState.Marked)
        {
            //Switch to Crossed
            toggled = false;
            GetComponent<Image>().color = Color.white;
            XText.SetActive(true);
            currentState = ButtonState.Crossed;
        }
        else if (currentState == ButtonState.Crossed)
        {
            //Switch to Blank
            toggled = false;
            GetComponent<Image>().color = Color.white;
            XText.SetActive(false);
            currentState = ButtonState.Blank;
        }
        else
        {
            //If somehow no states are active, default to Marked.
            toggled = true;
            GetComponent<Image>().color = Color.black;
            currentState = ButtonState.Marked;
        }

        controller.CheckWinCondition();
    }

    private void SetState(ButtonState b)
    {
        if (b == ButtonState.Blank)
        {
            //Switch to Blank
            toggled = false;
            GetComponent<Image>().color = Color.white;
            XText.SetActive(false);
            currentState = ButtonState.Blank;
        }
        else if (b == ButtonState.Marked)
        {
            //Switch to Marked
            toggled = true;
            GetComponent<Image>().color = Color.black;
            XText.SetActive(false);
            currentState = ButtonState.Marked;
        }
        else if (b == ButtonState.Crossed)
        {
            //Switch to Crossed
            toggled = false;
            GetComponent<Image>().color = Color.white;
            XText.SetActive(true);
            currentState = ButtonState.Crossed;
        }
    }

    //This method also serves as the button's init/start method
    public void SetPuzzleControllerReference(_PicrossPuzzle c)
    {
        controller = c;
        SetState(ButtonState.Blank);
    }

    public bool GetToggle()
    {
        return toggled;
    }
}
