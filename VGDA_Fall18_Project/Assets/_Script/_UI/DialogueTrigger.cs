using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject NPC; // the NPC who talks
    public GameObject Player; // the Player who talks to the NPC
    public float InteractDistance = 5f; // The acceptable distance in which the player can start talking to the NPC
    public float CheckPoint = 0f; // If the dialogue is supposed to trigger as a checkpoint
    public Dialogue dialogue; // the Dialogue spoken by the NPC

    public bool isTriggered = false;

    private void FixedUpdate()
    {
        float npcPosition = NPC.transform.position.x;
        float playerPosition = Player.transform.position.x;
        
        if (dialogue.DialogueIsCheckpoint)
        {
            float distanceBetween = Mathf.Abs(CheckPoint - playerPosition);
            if (distanceBetween < InteractDistance)
            {// works if and only if player is close enough
                if (!isTriggered)
                {
                    TriggerDialogue(); // Trigger Dialogue spoken
                    isTriggered = true;
                }
            }
        }
        else {
            float distanceBetween = Mathf.Abs(npcPosition - playerPosition);
            if (Input.GetButtonDown("InteractNPC") && (distanceBetween < InteractDistance))
            {// works if and only if player intends to talk to npc and they're close enough
                if (!isTriggered)
                {
                    TriggerDialogue(); // Trigger Dialogue spoken
                    isTriggered = true;
                }
            }
        }

    }
    // Triggers the dialogue through the Dialogue Manager
    // Passes in the dialogue object for the function to display
    public void TriggerDialogue() {  
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
