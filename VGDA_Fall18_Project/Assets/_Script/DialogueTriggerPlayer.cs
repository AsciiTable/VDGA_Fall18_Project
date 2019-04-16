using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerPlayer : MonoBehaviour
{
    //public GameObject NPC; // the NPC who talks
    public GameObject Player; // the Player who talks to the NPC
    public float InteractCheckpoint = 0f; // where the player should be when the dialogue box triggers
    public float InteractCheckpointBuffer = 5f; // The acceptable distance in which the player can start talking to the NPC
    [HideInInspector] public Dialogue dialogue; // the Dialogue spoken by the Player

    public bool isTriggered = false;

    // Triggers the dialogue through the Dialogue Manager
    // Passes in the dialogue object for the function to display
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
