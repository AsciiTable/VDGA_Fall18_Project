using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject NPC; // the NPC who talks
    public GameObject Player; // the Player who talks to the NPC
    public float InteractDistance = 5f; // The acceptable distance in which the player can start talking to the NPC
    public Dialogue dialogue; // the Dialogue spoken by the NPC

    private bool isTriggered = false;

    private void FixedUpdate()
    {
        float npcPosition = NPC.transform.position.x;
        float playerPosition = Player.transform.position.x;
        float distanceBetween = Mathf.Abs(npcPosition - playerPosition);
        //Debug.Log("Distance Between Player and NPC = " + distanceBetween);
        if (Input.GetButtonDown("InteractNPC") && (distanceBetween < InteractDistance))
        {// works if and only if player intends to talk to npc and they're close enough
            //Debug.Log("Button a is pressed");
            if (!isTriggered) {
                TriggerDialogue(); // Trigger Dialogue spoken
                isTriggered = true;
            }
            //isTriggered = false;
        }
    }
    // Triggers the dialogue through the Dialogue Manager
    // Passes in the dialogue object for the function to display
    public void TriggerDialogue() {  
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
