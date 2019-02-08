using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the functions to be edited inside of the engine
[System.Serializable]
public class Dialogue
{
    //Name of the speaker
    public string name;

    [TextArea(3, 10)]
    //Array of sentences
    public string[] sentences;
}
