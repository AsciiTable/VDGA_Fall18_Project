using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleManager : MonoBehaviour
{
    private SpeechBubblePlayer playerSpeech;
    private Queue<string> sentences;
    [SerializeField] private SpeechBubblePlayer thoughts;
    public Text speech;
    //public SpeechBubbleTrigger trigger; 
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartSpeechBubbles(SpeechBubblePlayer playerSpeech) {
        Debug.Log("Speech Bubble Starting");
        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach (string sentence in playerSpeech.sentences)
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

        speech.text = sentence;
    }

    public void EndDialogue()
    {
        FindObjectOfType<DialogueTrigger>().isTriggered = false;                    // sets trigger to false so the player can talk to the NPC again
        FindObjectOfType<DialogueTriggerPlayer>().isTriggered = false;
        animator.SetBool("isOpen", false);
    }
}
