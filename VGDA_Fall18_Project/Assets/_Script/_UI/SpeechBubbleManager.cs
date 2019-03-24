using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{
    public GameObject player;
    public Text dialogueText;
    private Queue<string> sentences;

    public Animator animator;

    public float[] checkpoints;
    private float[] checkpointCopy;

    private bool reachedCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        checkpointCopy = new float[checkpoints.Length];
        for (int i = 0; i < checkpoints.Length; i++) {
            checkpointCopy[i] = checkpoints[i];
            Debug.Log(checkpointCopy[i]);
            reachedCheck = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < checkpointCopy.Length; i++) {
            if (player.transform.position.x > checkpointCopy[i]) {
                Debug.Log("Check Reached in Update");
                Debug.Log(checkpointCopy[i]);
                checkpointCopy[i] = 999999999999999999;
                reachedCheck = true;
                break; // kind of allows for duplicate checkpoints
            }
        }
        if (reachedCheck)
        {
            Debug.Log("Check Reached");
            animator.SetBool("isOpen", true);
            DisplayNextSentence();
            StartCoroutine(SpeechBubbleOnTime());
            reachedCheck = false;
            animator.SetBool("isOpen", false);
        }
    }

    public void InititalizeDialogue(SpeechBubblePlayer speech)
    {
        sentences.Clear();

        foreach (string sentence in speech.sentences)
        {
            sentences.Enqueue(sentence);                                             // Lines each up to be in a queue
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();                                     // Starts using up each sentence in the queue

        //If you would like the text to appear immediately, use:
        Debug.Log("Sentence: " + sentence);
        animator.SetBool("isOpen", true);
        dialogueText.text = sentence;
    }

    /**
     * Waits for (10) seconds before the speech box disappears
     **/
    IEnumerator SpeechBubbleOnTime()
    {
        yield return new WaitForSeconds(4);
        animator.SetBool("isOpen", false);
    }

    public void EndDialogue()
    {
        Debug.Log("There are no more speech bubbles to display.");
    }

    public float firstCheck() {
        return checkpoints[0];
    }
}
