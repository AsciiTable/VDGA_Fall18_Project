using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //UI Objects to update the text on the screen
    public Text nameText;
    public Text dialogueText;
    public Image expression;

    //Player Movment Freezing
    public GameObject player;
    public GameObject winner;

    //Queues
    private Queue<Sprite> expressionImage;
    private Queue<string> names;
    private Queue<string> sentences;

    public Animator animator;
    private bool inCoro;

    // Start is called before the first frame update
    void Start()
    {
        expressionImage = new Queue<Sprite>();
        sentences = new Queue<string>();
        names = new Queue<string>();
        inCoro = false;
    }

    public void StartDialogue(Dialogue dialogue) {
        if (dialogue.FreezePlayerMovement) {
            player.GetComponent<Animator>().SetBool("PlayerMove", false);
            player.GetComponent<Player>().enabled = false;
        }
        //showDialogue.SetActive(true);

        animator.SetBool("IsOpen", true);
        //string name = "";
        //nameText.text = "Shadow Parker";
        sentences.Clear();
        names.Clear();
        expressionImage.Clear();
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);                                             // Lines each up to be in a queue
        }
        foreach (Sprite express in dialogue.expressions)
        {
            expressionImage.Enqueue(express);
        }
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
            player.GetComponent<Player>().enabled = true;
            winner.active = true;
            return;
        }
        string name = names.Dequeue();
        string sentence =  sentences.Dequeue();                                     // Starts using up each sentence in the queue
        Sprite express = expressionImage.Dequeue();
        expression.sprite = express;
        nameText.text = name;

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
        if (!FindObjectOfType<DialogueTrigger>().dialogue.DialogueIsCheckpoint) {
            FindObjectOfType<DialogueTrigger>().isTriggered = false;                    // sets trigger to false so the player can talk to the NPC again if it is not a checkpoint
            FindObjectOfType<DialogueTriggerPlayer>().isTriggered = false;
        }
        animator.SetBool("IsOpen", false);
    }
}
