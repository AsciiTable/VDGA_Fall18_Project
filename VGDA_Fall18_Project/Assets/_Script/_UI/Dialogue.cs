using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the functions to be edited inside of the engine
[System.Serializable]
public class Dialogue
{
    public bool FreezePlayerMovement;
    public bool DialogueIsCheckpoint;
    //Name of the speaker
    public string[] names;

    [TextArea(3, 10)]
    //Array of sentences
    public string[] sentences;

    /**public enum Expression {
        knightAnnoyed,
        knightHappy, 
        knightHaughty,
        knightKnightly,
        knightMoved,
        knightPuppyEyes,
        knightSad,
        knightSmugPity,
        knightStandard,
        knightTsundere,
        parkerAfraid,
        parkerAngry,
        parkerConfused,
        parkerGuilty,
        parkerHappy,
        parkerRecoil,
        parkerRelief,
        parkerSad,
        parkerStandard,
        shadowConfused,
        shadowCreepy,
        shadowInsane,
        shadowRecoil,
        shadowStandard,
        shadowTaunt
    }**/
    public Sprite[] expressions;
}
