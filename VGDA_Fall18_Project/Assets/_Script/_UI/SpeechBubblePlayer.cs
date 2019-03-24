using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeechBubblePlayer : MonoBehaviour
{
    // the Person who has multiple speech bubble lines
    public GameObject player;

    // Array of sentences
    [TextArea(3, 10)]
    public string[] sentences;
}
