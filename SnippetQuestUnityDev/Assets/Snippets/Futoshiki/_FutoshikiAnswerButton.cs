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
    private FutoshikiSnippetBoard controllerReference;
    private bool conflicting = false;
    private bool locked = false;
    public int maxNum;
    private int currentVal = 0;

    public TMP_FontAsset PrintedFont;
    public TMP_FontAsset HandwrittenFont;


    public void InstAnswerButton(FutoshikiSnippetBoard controller, int m)
    {
        controllerReference = controller;
        maxNum = m;
        buttonText.text = "";
        buttonText.font = HandwrittenFont;
    }

    //Sets the value held to a permanent, developer-defined thing
    public void SetValuePermanent(int value)
    {
        //Disables button so user cannot interact with it
        button.interactable = false;

        //Locks button as one with a preset value
        locked = true;
        buttonText.font = PrintedFont;

        SetCurrentValue(value);
    }
    
    public void SetCurrentValue()
    {
        if (currentVal != maxNum)
        {
            currentVal++;
            buttonText.text = currentVal.ToString();
        }
        else if (currentVal >= maxNum)
        {
            currentVal = 0;
            buttonText.text = "";
        }
        controllerReference.CheckWinCondition();
    }

    public void SetCurrentValue(int i)
    {
        if (i <= maxNum && i > 0)
        {
            currentVal = i;
            buttonText.text = currentVal.ToString();
        }
        else
        {
            Debug.LogError("Warning: value " + i + " is out of bounds for this futoshiki puzzle");
        }
    }

    public int GetCurrentVal()
    {
        return currentVal;
    }

    public void SetConflicting(bool b)
    {
        conflicting = b;
    }

    public bool GetConflicting()
    {
        return conflicting;
    }
    
    public bool GetLocked()
    {
        return locked;
    }

    public void SetTextColor(Color c)
    {
        buttonText.color = c;
    }
}
