using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleTrigger : MonoBehaviour
{
    /**public GameObject NPC; // the NPC who talks
    public GameObject Player; // the Player who talks to the NPC
    public float InteractDistance = 5f; // The acceptable distance in which the player can start talking to the NPC
    **/
    public SpeechBubblePlayer speechBubble;
    public bool isTriggered = false;

    public void TriggerSpeechBubble() {
        FindObjectOfType<SpeechBubbleManager>().StartSpeechBubbles(speechBubble);
    }

    public string[] getSentences() {
        return speechBubble.sentences;
    }
}
