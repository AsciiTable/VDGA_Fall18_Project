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
    private int save;

    void Start()
    {
        len = speechDict.checkpoints.Length;
        for (int i = 0; i < len; i++) {
            diction.Add(speechDict.checkpoints[i], speechDict.sentences[i]);
        }
        save = -9999;
    }

    void FixedUpdate()
    {
        if (!alreadyOpen) {
            for (int i = 0; i < len; i++) {
                if (Mathf.Abs(speechDict.checkpoints[i] - Player.position.x) < buffer)
                {
                    save = i;
                    TriggerSpeech(diction[speechDict.checkpoints[i]]);
                    break;
                }
            }
        }
        else
        {
            if (Mathf.Abs(speechDict.checkpoints[save] - Player.position.x) > buffer)
            {
                alreadyOpen = false;
                anim.SetBool("isOpen", false);
                save = -9999;
            }
        }
    }

    public void TriggerSpeech(string speech) {
        speechText.text = speech;
        anim.SetBool("isOpen", true);
        alreadyOpen = true;
    }
}
