using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the functions to be edited inside of the engine
[System.Serializable]
public class SpeechBubblePlayer
{
    [TextArea(3, 10)]
    //Array of sentences
    public string[] sentences;
}