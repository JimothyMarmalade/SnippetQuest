using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _PicrossClueButton : MonoBehaviour
{
    private int clue;
    private _PicrossPuzzle controller;
    public TMP_Text clueText;

    public void SetButtonText(int s)
    {
        string text = s.ToString();
        clueText.text = text;
    }




    public void SetPuzzleControllerReference(_PicrossPuzzle c)
    {
        controller = c;
    }

}
