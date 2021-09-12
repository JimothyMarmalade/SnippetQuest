/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 7/10/21
 * 
 * Controls the display of dialog onscreen when fed a dialog pack. Handles UI Elements 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; set; }

    public GameObject DEBUGDialogMenuNametag;

    public delegate void DialogEvent();
    public static event DialogEvent OnDialogOver;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null && Instance != this)
        {
            Instance = this;
        }
    }

    [Header("Speaker/Text Data")]
    public TMP_Text nameText;
    public TMP_Text dialogText;

    [Header("Option Buttons")]
    public GameObject NextButton;
    public GameObject Op1Button;
    public GameObject Op2Button;

    [Header("Animator Reference")]
    public Animator animator;

    private Dialog nextDialog;
    private Dialog Choice1;
    private Dialog Choice2;
    private ExpressionController focusedCharacterFace;

    private void Start()
    {
        //sentences = new Queue<string>();
        ShowNextButton();
    }

    public void DisplayDialogNormal(Dialog d)
    {
        animator.SetBool("IsOpen", true);
        DEBUGDialogMenuNametag.SetActive(true);

        ShowNextButton();


        nameText.text = d.speakerName;
        if (focusedCharacterFace != null)
        {
            focusedCharacterFace.ChangeExpression(d.eyesExpression, d.mouthExpression);
        }
        StopAllCoroutines(); 
        StartCoroutine(TypeSentence(d.dialogLine));
    }

    public void DisplayDialogChoices(Dialog d, string playerChoice1, string playerChoice2)
    {
        animator.SetBool("IsOpen", true);
        DEBUGDialogMenuNametag.SetActive(true);

        ShowOptionsButtons();

        nameText.text = d.speakerName;
        if (focusedCharacterFace != null)
        {
            focusedCharacterFace.ChangeExpression(d.eyesExpression, d.mouthExpression);
        }

        Op1Button.GetComponentInChildren<TMP_Text>().text = playerChoice1;

        Op2Button.GetComponentInChildren<TMP_Text>().text = playerChoice2;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(d.dialogLine));
    }

    public void SetNextDialog(Dialog d)
    {
        nextDialog = d;
    }

    public void SetPlayerChoices(Dialog d1, Dialog d2)
    {
        Choice1 = d1;
        Choice2 = d2;
    }

    public void SetCharacterFace(ExpressionController exc)
    {
        focusedCharacterFace = exc;
    }

    public void DisplayNextDialogLine()
    {
        if (nextDialog == null)
        {
            EndDialog();
            return;
        }
        else
        {
            nextDialog.DisplayDialog();
        }
    }
    public void MakePlayerDecision(bool choice)
    {
        if (choice)
        {
            Choice1.DisplayDialog();
        }
        //if choice is false, player selected the lower option (option 2)
        else if (!choice)
        {
            Choice2.DisplayDialog();
        }
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        //Debug.Log("End of Conversation");
        animator.SetBool("IsOpen", false);
        DEBUGDialogMenuNametag.SetActive(false);
        nextDialog = null;
        Choice2 = null;
        Choice1 = null;
        if (focusedCharacterFace != null)
            focusedCharacterFace.ClearExpression();
        focusedCharacterFace = null;

        if (OnDialogOver != null)
        {
            OnDialogOver();
        }

    }

    public void ShowNextButton()
    {
        NextButton.SetActive(true);

        Op1Button.SetActive(false);
        Op2Button.SetActive(false);
    }

    public void ShowOptionsButtons()
    {
        NextButton.SetActive(false);

        Op1Button.SetActive(true);
        Op2Button.SetActive(true);
    }


}
