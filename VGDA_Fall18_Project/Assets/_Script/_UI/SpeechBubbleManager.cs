using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleManager : MonoBehaviour
{
    private string[] sentences;
    public SpeechBubbleTrigger trigger; 

    // Start is called before the first frame update
    void Start()
    {
        sentences = trigger.getSentences();
    }

    public void StartSpeechBubbles(sentences) {
        Debug.Log("Speech Bubble Starting");
    }

}
