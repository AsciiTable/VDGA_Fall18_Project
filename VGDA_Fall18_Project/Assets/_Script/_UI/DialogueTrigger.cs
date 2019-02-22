using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject npc;
    public GameObject player;

    private float distanceBetween;

    /**private float playerPosition;
    private float npcPosition;
    private float distanceBetween;**/

    private void Start()
    {
        npc.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Update()
    {
        float npcPosition = npc.transform.position.x;
        float playerPosition = player.transform.position.x;
        Debug.Log("X Position of Player = " + playerPosition);
        float distanceBetween = Mathf.Abs(npcPosition - playerPosition);
        Debug.Log("Distance Between Player and NPC = " + distanceBetween);
    }
    private void FixedUpdate()
    {
        
        if (Input.GetButtonDown("InteractNPC") && (distanceBetween < 10))
        {
            npc.GetComponent<BoxCollider2D>().enabled = true;
            Debug.Log("Button a is pressed");
            TriggerDialogue();
        }
    }

    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    /**private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Function works");
        
        if (collision.CompareTag("Player"))
        {
            TriggerDialogue();
            //npc.enabled = false;
            //time needs to freeze when talking to NPC
        }
        else {
            //npc.enabled = false;
        }
        
    }**/
}
