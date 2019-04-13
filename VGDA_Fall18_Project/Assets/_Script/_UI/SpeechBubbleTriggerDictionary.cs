using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleTriggerDictionary : MonoBehaviour
{
    public Transform Player;
    public float buffer = 5f;
    public SpeechBubbleDictionary speechDict;
    private Dictionary<float, string> diction = new Dictionary<float, string>();
    private int len;
    private bool alreadyOpen = false;
    public Animator anim;
    public Text speechText;

    void Start()
    {
        Debug.Log("Player Position Start: " + Player.position.x);
        len = speechDict.checkpoints.Length;
        for (int i = 0; i < len; i++) {
            diction.Add(speechDict.checkpoints[i], speechDict.sentences[i]);
        }
    }

    void Update()
    {
        Debug.Log("Player Position: " + Player.position.x);
        for (int i = 0; i < len; i++)
        {
            if (!alreadyOpen)
            {
                if (Mathf.Abs(speechDict.checkpoints[i] - Player.position.x) < buffer)
                {
                   
                    TriggerSpeech(diction[speechDict.checkpoints[i]]);
                    alreadyOpen = true;
                }
            }
            else {
                if (Mathf.Abs(speechDict.checkpoints[i] - Player.position.x) > buffer)
                {
                    alreadyOpen = false;
                    anim.SetBool("isOpen", false);
                }
            }
        }
    }

    public void TriggerSpeech(string speech) {
        if (!alreadyOpen) {
            speechText.text = "";
            anim.SetBool("isOpen", true);
        }
        speechText.text = speech;
    }
}
