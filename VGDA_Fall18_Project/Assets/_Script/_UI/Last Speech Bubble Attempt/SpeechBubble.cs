using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public Animator animator;
    public int TimeOff = 7;
    public int TimeOn = 10;

    private void Start()
    {
        StartCoroutine(SpeechBubbleOffTime());
    }
    void Update()
    {
        
        /**if (FindObjectOfType<DialogueTrigger>().isTriggered == true)
        {
            //StopAllCoroutines();
            //animator.SetBool("isOpen", false);
        }
        else {
            //StartCoroutine(SpeechBubbleOffTime());
        }**/
    }

    /**
     * Waits for (7) seconds before the speech box appears again
     **/
    IEnumerator SpeechBubbleOffTime() {
        yield return new WaitForSeconds(TimeOff);
        animator.SetBool("isOpen", true);
        StartCoroutine(SpeechBubbleOnTime());
    }
    /**
     * Waits for (10) seconds before the speech box disappears
     **/
    IEnumerator SpeechBubbleOnTime() {
        yield return new WaitForSeconds(TimeOn);
        animator.SetBool("isOpen", false);
        StartCoroutine(SpeechBubbleOffTime());
    }
}
