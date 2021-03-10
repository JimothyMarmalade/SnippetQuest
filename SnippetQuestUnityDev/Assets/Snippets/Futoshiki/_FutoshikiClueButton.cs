using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _FutoshikiClueButton : MonoBehaviour
{
    //refers to the self
    public Button button;
    public TMP_Text buttonText;
    private string indicator = ">";
    public char cluePointer = 'n';
    public int gridSpaceX;
    public int gridSpaceY;

    public bool hasClueAssigned;

    //The pointer points right (>) if the value to the right is the lesser of the two, for example
    //n = null, l = left, r = right, u = up, d = down
    public char lesserValuePointer;

    public void setGridSpace(int x, int y)
    {
        gridSpaceX = x;
        gridSpaceY = y;
    }

    //rotates the button to indicate where the lesser value is
    public void setOrientation(char pointer)
    {
        buttonText.text = indicator;

        if (pointer == 'r')
        {
            //Do nothing, it's already in position
        }
        else if (pointer == 'l')
        {
            button.transform.Rotate(0f, 0f, 180f);
        }
        else if (pointer == 'u')
        {
            button.transform.Rotate(0f, 0f, 90f);
        }
        else if (pointer == 'd')
        {
            button.transform.Rotate(0f, 0f, -90f);
        }
        else
        {
            buttonText.text = "!";
            Destroy(this);
        }

        cluePointer = pointer;


    }

    public void DEBUGShowGridLoc()
    {
        Debug.Log("Clicked clue Button located at [" + gridSpaceX + ", " + gridSpaceY + "]");
    }

    public string GetLoc()
    {
        return (gridSpaceX + ", " + gridSpaceY);
    }
}