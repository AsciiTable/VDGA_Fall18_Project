using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleTrigger : MonoBehaviour
{
    public SpeechBubblePlayer speechBubble;

    public void TriggerSpeechBubble() {
        FindObjectOfType<SpeechBubbleManager>().StartSpeechBubbles(speechBubble);
    }

    public string[] getSentences() {
        return speechBubble.sentences;
    }
}
