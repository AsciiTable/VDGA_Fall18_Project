using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Collider2D npc;

    private void Start()
    {
        npc.enabled = false;
    }
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("InteractNPC"))
        {
            Debug.Log("Button a is pressed");
            npc.enabled = true;
        }
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            TriggerDialogue();
            npc.enabled = false;
            //time needs to freeze when talking to NPC
        }
    }
}
