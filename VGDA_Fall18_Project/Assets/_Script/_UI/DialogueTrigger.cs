using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject NPC;
    public GameObject Player;
    public float InteractDistance = 5f;
    public Dialogue dialogue;


    private void Start()
    {
        NPC.GetComponent<BoxCollider2D>().enabled = true;
    }
    private void FixedUpdate()
    {
        float npcPosition = NPC.transform.position.x;
        float playerPosition = Player.transform.position.x;
        float distanceBetween = Mathf.Abs(npcPosition - playerPosition);
        Debug.Log("Distance Between Player and NPC = " + distanceBetween);
        if (Input.GetButtonDown("InteractNPC") && (distanceBetween < InteractDistance))
        {
            NPC.GetComponent<BoxCollider2D>().enabled = true;
            Debug.Log("Button a is pressed");
            TriggerDialogue();
        }
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
