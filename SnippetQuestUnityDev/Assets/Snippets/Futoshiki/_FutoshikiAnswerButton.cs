using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _FutoshikiAnswerButton : MonoBehaviour
{
    //refers to the self
    public Button button;
    public TMP_Text buttonText;
    public FutoshikiSnippetBoard controllerReference;
    public bool markedIncorrect = false;
    public int maxNum = 1;
    public int currentVal;


    public void SetPuzzleControllerReference(FutoshikiSnippetBoard controller, int m)
    {
        controllerReference = controller;
        maxNum = m;
        buttonText.text = "1";
        currentVal = 1;
    }

    /*
    //Increments the number in the answerspace
    public void SetNumber()
    {
        if (!hasPresetAnswer)
        {
            if (buttonText.text == "1")
            {
                buttonText.text = "2";
            }
            else if (buttonText.text == "2")
            {
                buttonText.text = "3";
            }
            else if (buttonText.text == "3")
            {
                buttonText.text = "4";
            }
            else if (buttonText.text == "4")
            {
                buttonText.text = "5";
            }
            else if (buttonText.text == "5")
            {
                buttonText.text = "6";
            }
            else if (buttonText.text == "6" || buttonText.text == " ")
            {
                buttonText.text = "1";
            }
            else
                buttonText.text = " ";

            Debug.Log("Clicked button at " + gridSpaceX + ", " + gridSpaceY);

            //Calls the puzzle controller to check if the puzzle has been solved
            controllerReference.checkWinConditions();
        }
    }

    //Sets the value held to a permanent, developer-defined thing
    public void setNumberPermanent(int value)
    {
        //Disables button so user cannot interact with it
        button.interactable = false;

        //Locks button as one with a preset value
        hasPresetAnswer = true;

        //Sets the presetAnswer to the input value and updates buttonText with the value
        presetAnswer = value;
        buttonText.text = value.ToString();
    }

    //Sets the x and y of the button in the grid
    public void setGridSpace(int x, int y)
    {
        gridSpaceX = x;
        gridSpaceY = y;
    }
    */
    //Returns the string in the button as an int
    
    public void SetNumber()
    {
        if (currentVal != maxNum)
        {
            currentVal++;
            buttonText.text = currentVal.ToString();
        }
        else if (currentVal >= maxNum)
        {
            currentVal = 1;
            buttonText.text = currentVal.ToString();
        }
        controllerReference.CheckWinCondition();
    }
    
    public int GetAnswerInt()
    {
        if (buttonText.text == " ")
            return 0;
        else
        {
            char rawchar = buttonText.text[0];
            int answerInt = (int)char.GetNumericValue(rawchar);

            return answerInt;
        }

    }

    public void SetTextColor()
    {
        if (markedIncorrect)
            buttonText.color = Color.red;
        else if (!markedIncorrect)
            buttonText.color = Color.black;
    }
}
