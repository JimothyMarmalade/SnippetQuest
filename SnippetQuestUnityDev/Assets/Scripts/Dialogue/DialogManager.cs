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

    //private Queue<string> sentences;
    private Dialog currentDialog;
    private DialogChoice currentDialogChoice;
    private ExpressionController focusedCharacterFace;

    private void Start()
    {
        //sentences = new Queue<string>();
        ShowNextButton();
    }

    public void StartDialog(Dialog dialog)
    {
        //Debug.Log("Starting conversation with " + dialog.speakerName);
        animator.SetBool("IsOpen", true);

        DEBUGDialogMenuNametag.SetActive(true);


        currentDialog = dialog;
        nameText.text = currentDialog.speakerName;

        string sentence = currentDialog.dialogLine[0];
        //Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void StartDialog(Dialog dialog, ExpressionController face)
    {
        //Debug.Log("Starting conversation with " + dialog.speakerName);
        animator.SetBool("IsOpen", true);

        DEBUGDialogMenuNametag.SetActive(true);

        ShowNextButton();

        currentDialog = dialog;
        focusedCharacterFace = face;


        nameText.text = currentDialog.speakerName;
        string sentence = currentDialog.dialogLine[0];
        //Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        focusedCharacterFace.ChangeExpression(currentDialog.eyesExpression, currentDialog.mouthExpression);
    }

    public void DisplayNextDialogLine()
    {
        if (currentDialog.NextDialog == null)
        {
            EndDialog();
            return;
        }
        else
        {
            currentDialog = currentDialog.NextDialog;
            nameText.text = currentDialog.speakerName;
            ShowNextButton();

            string sentence = currentDialog.dialogLine[0];
            //Debug.Log(sentence);
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            if (focusedCharacterFace != null)
            {
                focusedCharacterFace.ChangeExpression(currentDialog.eyesExpression, currentDialog.mouthExpression);
            }
        }
    }


    #region Dialog with Player Choices

    public void StartDialogChoice(DialogChoice d, ExpressionController face)
    {
        //Debug.Log("Starting conversation with " + dialog.speakerName);
        animator.SetBool("IsOpen", true);

        DEBUGDialogMenuNametag.SetActive(true);


        currentDialogChoice = d;
        focusedCharacterFace = face;
        nameText.text = currentDialogChoice.speakerName;

        string sentence = currentDialogChoice.dialogLine[0];


        ShowOptionsButtons();
        Op1Button.GetComponentInChildren<TMP_Text>().text = currentDialogChoice.PlayerReaction1;

        Op2Button.GetComponentInChildren<TMP_Text>().text = currentDialogChoice.PlayerReaction2;


        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void MakePlayerDecision(bool choice)
    {
        if (choice)
        {
            DisplayNextDialogLine(currentDialogChoice.DialogChoice1);
        }
        //if choice is false, player selected the lower option (option 2)
        else if (!choice)
        {
            DisplayNextDialogLine(currentDialogChoice.DialogChoice2);
        }
    }

    public void DisplayNextDialogLine(Dialog dialog)
    {
        currentDialog = dialog;
        nameText.text = currentDialog.speakerName;

        string sentence = currentDialog.dialogLine[0];
        //Debug.Log(sentence);
        ShowNextButton();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if (focusedCharacterFace != null)
        {
            focusedCharacterFace.ChangeExpression(currentDialog.eyesExpression, currentDialog.mouthExpression);
        }
    }

    #endregion



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
        currentDialog = null;
        currentDialogChoice = null;
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
