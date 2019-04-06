    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThoughts : MonoBehaviour
{

    [SerializeField] private Dialogue thoughts;
    [SerializeField] private GameObject Player;
    public float InteractDistance = 5f;

    private Transform xyz;
    private DialogueTriggerPlayer script;
    

    private void Awake()
    {
        xyz = gameObject.GetComponent<Transform>();
        script = Player.GetComponent<DialogueTriggerPlayer>();   
    }
    private void FixedUpdate()
    {
        float npcPosition = xyz.position.x;
        float playerPosition = Player.transform.position.x;
        float distanceBetween = Mathf.Abs(npcPosition - playerPosition);
        if (Input.GetButtonDown("InteractNPC") && (distanceBetween < InteractDistance))
        {// works if and only if player intends to talk to npc and they're close enough
            if (!script.isTriggered)
            {
                script.dialogue = thoughts;
                script.TriggerDialogue(); // Trigger Dialogue spoken
                script.isTriggered = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
