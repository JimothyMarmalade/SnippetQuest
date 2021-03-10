/*
 * Created by Logan Edmund, 3/9/21
 * Last Modified by Logan Edmund, 3/9/21
 * 
 * Function of PicrossAnswerButton is to relay data from picross grid squares to the PicrossSnippetBoard controller. Stores/Relays it's current state
 * (Blank, Filled, Marked, Crossed).
 * 
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _PicrossAnswerButton : MonoBehaviour
{
    //Holds information and methods for the Picross Button

    enum ButtonState {Blank, Filled, Marked, Crossed}
    ButtonState currentState = ButtonState.Blank;

    private bool canBeModified = true;
    public TMP_Text markerText;

    private PicrossSnippetBoard controller;

    public char currentValue;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void ToggleState()
    {
        if (canBeModified)
        {
            switch (currentState)
            {
                case (ButtonState.Blank):
                    //Switch to Filled
                    GetComponent<Image>().color = Color.black;
                    markerText.text = "";
                    currentValue = '1';
                    currentState = ButtonState.Filled;
                    break;
                case (ButtonState.Filled):
                    //Switch to Marked
                    GetComponent<Image>().color = Color.white;
                    markerText.text = "•";
                    currentValue = '•';
                    currentState = ButtonState.Marked;
                    break;
                case (ButtonState.Marked):
                    //Switch to Crossed
                    markerText.text = "X";
                    currentValue = 'X';
                    currentState = ButtonState.Crossed;
                    break;
                case (ButtonState.Crossed):
                    //Switch to Blank
                    markerText.text = "";
                    currentValue = '0';
                    currentState = ButtonState.Blank;
                    break;
            }
            controller.CheckWinCondition();
        }
        
    }

    private void SetState(ButtonState b)
    {
        switch (b)
        {
            case (ButtonState.Blank):
                //Switch to Blank
                GetComponent<Image>().color = Color.white;
                markerText.text = "";
                currentValue = '0';
                currentState = ButtonState.Blank;
                break;
            case (ButtonState.Filled):
                //Switch to Filled
                GetComponent<Image>().color = Color.black;
                markerText.text = "";
                currentValue = '1';
                currentState = ButtonState.Filled;
                break;
            case (ButtonState.Marked):
                //Switch to Marked
                GetComponent<Image>().color = Color.white;
                markerText.text = "•";
                currentValue = '•';
                currentState = ButtonState.Marked;
                break;
            case (ButtonState.Crossed):
                //Switch to Crossed
                GetComponent<Image>().color = Color.white;
                markerText.text = "X";
                currentValue = 'X';
                currentState = ButtonState.Crossed;
                break;
        }
        //controller.CheckWinCondition();

    }

    public void SetState(string state)
    {
        switch (state)
        {
            case ("Blank"):
                //Switch to Blank
                GetComponent<Image>().color = Color.white;
                markerText.text = "";
                currentValue = '0';
                currentState = ButtonState.Blank;
                break;
            case ("Filled"):
                //Switch to Filled
                GetComponent<Image>().color = Color.black;
                markerText.text = "";
                currentValue = '1';
                currentState = ButtonState.Filled;
                break;
            case ("Marked"):
                //Switch to Marked
                GetComponent<Image>().color = Color.white;
                markerText.text = "•";
                currentValue = '•';
                currentState = ButtonState.Marked;
                break;
            case ("Crossed"):
                //Switch to Crossed
                GetComponent<Image>().color = Color.white;
                markerText.text = "X";
                currentValue = 'X';
                currentState = ButtonState.Crossed;
                break;
        }
        //controller.CheckWinCondition();

    }

    //This method also serves as the button's init/start method
    public void SetPuzzleControllerReference(PicrossSnippetBoard c)
    {
        controller = c;
        SetState(ButtonState.Blank);
    }
}
