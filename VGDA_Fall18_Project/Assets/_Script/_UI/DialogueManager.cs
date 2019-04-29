﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //UI Objects to update the text on the screen
    public Text nameText;
    public Text dialogueText;

    public Queue<Dialogue.Expression> expressionImage;
    public Image expression;

    private Queue<string> sentences;

    public Animator animator;
    private bool inCoro;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        inCoro = false;
    }

    public void StartDialogue(Dialogue dialogue) {
        //showDialogue.SetActive(true);
        animator.SetBool("IsOpen", true);
        string name = "";
        if (dialogue.Speaker == Dialogue.Name.KnightBoy)
        {
            nameText.text = "Knight Boy";
        }
        else if (dialogue.Speaker == Dialogue.Name.Parker)
        {
            nameText.text = "Parker";
        }
        else if (dialogue.Speaker == Dialogue.Name.ShadowParker) {
            nameText.text = "Shadow Parker";
        }
        else
        {
            nameText.text = "NPC";
        }
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);                                             // Lines each up to be in a queue
        }
        foreach (Dialogue.Expression express in dialogue.expressions) {
            expressionImage.Enqueue(express);
        }
        DisplayNextSentence(dialogue);
    }

    /**
     * Displays the next sentence of the dialogue
     */
    public void DisplayNextSentence(Dialogue dialogue) {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence =  sentences.Dequeue();                                     // Starts using up each sentence in the queue

        //If you would like the text to appear immediately, use:
        //dialogueText.text = sentence;

        //If you want to show it letter by letter, use:
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /**
     * The text appears one letter at a time (Coroutine)
     * @param sentence to be "typed out"
     * @return null - getting the display of the sentence is all we need
     */
    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            
            dialogueText.text += letter;                                            // Add one letter onto the scrreen at a time
            if (Input.GetButtonDown("FasterInteractNPC")){
                StopAllCoroutines();
                dialogueText.text = sentence;
                inCoro = false;
            }
            yield return null;
        }
    }

    /**
     * Call when there is no dialogue left to be shown. Dialogue triggered
     * is set to false and the dialogue box is closed.
     */
    public void EndDialogue() {
        FindObjectOfType<DialogueTrigger>().isTriggered = false;                    // sets trigger to false so the player can talk to the NPC again
        FindObjectOfType<DialogueTriggerPlayer>().isTriggered = false;
        animator.SetBool("IsOpen", false);
    }

    public Image SetExpression(Dialogue dialogue) {
        Image expression = null;
        if (dialogue.Speaker == Dialogue.Name.KnightBoy) {
        }
        return expression;
    }
}
