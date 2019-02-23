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

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        //showDialogue.SetActive(true);
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();


        //If you would like the text to appear immediately, use:
        //dialogueText.text = sentence;

        //If you want to show it letter by letter, use:
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //The text appears one letter at a time (Coroutine)
    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            //Add one letter onto the scrreen at a time
            dialogueText.text += letter;
            if (Input.GetButtonDown("FasterInteractNPC")){
                StopAllCoroutines();
                dialogueText.text = sentence;
                yield return null;
            }
            yield return null;
        }
    }

    public void EndDialogue() {
        animator.SetBool("IsOpen", false);
    }
}
