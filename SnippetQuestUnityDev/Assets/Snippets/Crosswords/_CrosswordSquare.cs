using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Last edited by Logan Edmund, 2/10/21


public class _CrosswordSquare : MonoBehaviour
{
    public _CrosswordPuzzle CrosswordPuzzleReference;
    public TMP_InputField CWInputField;
    public TMP_Text NumDisplay;

    private char CurrentChar;
    private char AnswerChar;
    private string Fallback = "";

    private bool isWordStart;
    public int WordNum;
    public int AcrossWordNum;
    public int DownWordNum;

    private bool DontThinkTwice = false;

    public void Start()
    {
        //Debug Testing
        //SetWordNum(WordNum);
    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void OnValueChanged()
    {
        //Fixes the error where the puzzle checks for a solution twice after each input.

        //[BUGFIX POTENTIAL]
        //Error is caused by the player inputting a lowercase letter. When the script changes it to uppercase,
        //it counts as another input change and runs this method again. If the second check can be bypassed in
        //any way, it may be easier to do that than to keep track of this bool for each object.
        if (DontThinkTwice)
        {
            DontThinkTwice = !DontThinkTwice;
            return;
        }
        else
            DontThinkTwice = true;

        //Runs when a new value is typed into the field. Changes should only apply if the input is a letter.
        string newInput = CWInputField.text;

        if (CWInputField.text.Length == 1)
        {
            //If length of the field text is less than 1, there is only the newly-input char to consider.
            int val = (int)CWInputField.text[0];

            //Check input to ensure it is a letter
            if (CheckIfLetter(val))
                SetCWInputField(newInput);
            else
                //If the character is invalid, revert to what the text was initially
                CWInputField.text = Fallback;
        }
        else if (CWInputField.text.Length > 0)
        {
            //If length of the field text is more than 1, we are only interested in the last char
            int val = (int)CWInputField.text[1];
            
            //Check input to ensure it is a letter
            if (CheckIfLetter(val))
                SetCWInputField(newInput);
            else
                //If the character is invalid, revert to what the text was initially
                CWInputField.text = Fallback;
        }
        Debug.Log("The CurrentChar is " + CurrentChar);

        CrosswordPuzzleReference.CheckForCorrectAnswer();
    }

    private void SetCWInputField(string s)
    {
        //Keep the most recent char
        s = KeepMostRecentInput(s);
        //Capitalize the field
        s = CapitalizeString(s);

        //Set the text field equal to the newInput, set the currentChar and Fallback, and end method
        CWInputField.text = s;
        SetCurrentChar(s[0]);
        Fallback = s;
    }
    private bool CheckIfLetter(int val)
    {
        if ((val >= 65 && val <= 90) || (val >= 97 && val <= 122))
            return true;
        else
            return false;
    }
    private string KeepMostRecentInput(string s)
    {
        //sets the total value of CWInputField.text to be the most-recently input letter of the field
        s = s[s.Length - 1].ToString();
        return s;
    }

    private string CapitalizeString(string s)
    {
        //Capitalizes the entirety of CWInputField.text (which should only be one letter)
        s = s.ToUpper();
        return s;
    }

    public bool CheckCorrectInput()
    {
        if (CurrentChar == AnswerChar)
            return true;
        else
            return false;
    }

    public void OnSelect()
    {
        CrosswordPuzzleReference.DisplayClues(AcrossWordNum, DownWordNum);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public char GetCurrentChar()
    {
        return CurrentChar;
    }
    private void SetCurrentChar(char c)
    {
        CurrentChar = c;
    }

    public int GetWordNum()
    {
        return WordNum;
    }

    public void SetWordNum(int n)
    {
        WordNum = n;
        NumDisplay.text = n.ToString();
    }

    public void NumCheck()
    {
        if (WordNum <= 0)
        {
            NumDisplay.text = "";
        }
    }

    public void SetPuzzleControllerReference(_CrosswordPuzzle c)
    {
        CrosswordPuzzleReference = c;
    }

    public void SetAnswerChar(char c)
    {
        AnswerChar = c;
    }

    public char GetAnswerChar()
    {
        return AnswerChar;
    }

    public void SetIsWordStart(bool b)
    {
        isWordStart = b;
    }

    public bool GetIsWordStart()
    {
        return isWordStart;
    }

    public void SetAcrossWordNum(int i)
    {
        AcrossWordNum = i;
    }

    public int GetAcrossWordNum()
    {
        return AcrossWordNum;
    }

    public void SetDownWordNum(int i)
    {
        DownWordNum = i;
    }

    public int GetDownWordNum()
    {
        return DownWordNum;
    }


}
