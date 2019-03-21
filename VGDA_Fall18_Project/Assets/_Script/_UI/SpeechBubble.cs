using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public Animator animator;
    public int TimeOff = 7;
    public int TimeOn = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpeechBubbleOffTime());
    }

    /**
     * Waits for (7) seconds before the speech box appears again
     **/
    IEnumerator SpeechBubbleOffTime() {
        yield return new WaitForSeconds(7);
        animator.SetBool("isOpen", true);
        StartCoroutine(SpeechBubbleOnTime());
    }
    /**
     * Waits for (10) seconds before the speech box disappears
     **/
    IEnumerator SpeechBubbleOnTime() {
        yield return new WaitForSeconds(10);
        animator.SetBool("isOpen", false);
        StartCoroutine(SpeechBubbleOffTime());
    }
}
