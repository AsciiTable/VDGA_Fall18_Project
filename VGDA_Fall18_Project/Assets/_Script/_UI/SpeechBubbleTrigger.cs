﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleTrigger : MonoBehaviour
{
    //public GameObject NPC; // the NPC who talks
    public GameObject Player; // the Player who talks to the NPC
    public float InteractCheckpoint = 0f; // where the player should be when the dialogue box triggers
    public float InteractCheckpointBuffer = 5f; // The acceptable distance in which the player can start talking to the NPC
    public SpeechBubblePlayer speech; // the Dialogue spoken by the Player

    public bool isTriggered = false;

    private void FixedUpdate()
    {
        //float npcPosition = NPC.transform.position.x;
        float check1 = FindObjectOfType<SpeechBubbleManager>().firstCheck();
        float playerPosition = Player.transform.position.x;
        float distanceBetween = Mathf.Abs(InteractCheckpoint - playerPosition);
        if (playerPosition > check1)
        {
            TriggerDialogue(); // Trigger Dialogue spoken
            isTriggered = true;
            Debug.Log("Triggered");
        }
        if (Input.GetButtonDown("InteractNPC") && (distanceBetween < InteractCheckpointBuffer))
        {// works if and only if player intends to talk to npc and they're close enough
            if (!isTriggered)
            {
                TriggerDialogue(); // Trigger Dialogue spoken
                isTriggered = true;
            }
        }
    }
    // Triggers the dialogue through the Dialogue Manager
    // Passes in the dialogue object for the function to display
    public void TriggerDialogue()
    {
        //Time.timeScale = 0f;
        FindObjectOfType<SpeechBubbleManager>().InititalizeDialogue(speech);
    }
}
