/*
 * Created by Logan Edmund, 3/2/21
 * Last Modified by Logan Edmund, 3/8/21
 * 
 * Controls the display of dialogue onscreen when fed a dialogue pack. Handles UI Elements 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; }

    public GameObject DEBUGDialogMenuNametag;

    public delegate void DialogueEvent();
    public static event DialogueEvent OnDialogueOver;


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
    public TMP_Text dialogueText;

    [Header("Animator Reference")]
    public Animator animator;


    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.speakerName);
        animator.SetBool("IsOpen", true);

        DEBUGDialogMenuNametag.SetActive(true);

        nameText.text = dialogue.speakerName;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        //Debug.Log("End of Conversation");
        animator.SetBool("IsOpen", false);
        DEBUGDialogMenuNametag.SetActive(false);

        if (OnDialogueOver != null)
        {
            OnDialogueOver();
        }

    }

}
