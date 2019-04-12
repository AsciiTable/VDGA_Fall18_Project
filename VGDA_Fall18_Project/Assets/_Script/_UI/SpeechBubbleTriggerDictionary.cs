using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleTriggerDictionary : MonoBehaviour
{
    public GameObject Player;
    public float buffer = 5f;
    public SpeechBubbleDictionary speechDict;
    private Dictionary<float, string> dictionary;
    // Start is called before the first frame update
    void Start()
    {
        int len = speechDict.checkpoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
