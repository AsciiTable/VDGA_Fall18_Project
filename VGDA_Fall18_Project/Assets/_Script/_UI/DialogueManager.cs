using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //UI Objects to update the text on the screen
    public Text nameText;
    public Text dialogueText;

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

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);                                             // Lines each up to be in a queue
        }
        DisplayNextSentence();
    }

    /**
     * Displays the next sentence of the dialogue
     */
    public void DisplayNextSentence() {

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

    /**IEnumerator WaitContinue(string sentence) {
        while (!Input.GetButtonDown("TempContinueDialogue")) {
            yield return null;
        }
        StartCoroutine(TypeSentence(sentence));
    }**/

    /**
     * Call when there is no dialogue left to be shown. Dialogue triggered
     * is set to false and the dialogue box is closed.
     */
    public void EndDialogue() {
        FindObjectOfType<DialogueTrigger>().isTriggered = false;                    // sets trigger to false so the player can talk to the NPC again
        FindObjectOfType<DialogueTriggerPlayer>().isTriggered = false;
        animator.SetBool("IsOpen", false);
    }
}
