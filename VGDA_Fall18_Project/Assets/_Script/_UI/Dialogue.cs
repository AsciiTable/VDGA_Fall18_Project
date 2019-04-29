using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the functions to be edited inside of the engine
[System.Serializable]
public class Dialogue
{
    //Name of the speaker
    public enum Name {
        KnightBoy = 0,
        Parker = 1,
        ShadowParker = 2
    }

    public Name Speaker = Name.KnightBoy;

    [TextArea(3, 10)]
    //Array of sentences
    public string[] sentences;

    public enum Expression {
        Annoyed,
        Happy, 
        Haughty,
        Knightly,
        Moved,
        Afraid,
        Angry,
        Confused,
        Guilty,
        Recoil,
        Relief,
        Sad,
        Standard,
        Pity,
        Creepy,
        Insane,
        Taunt,
        Smug,
        Tsundere
    }
    public Expression[] expressions;
}
