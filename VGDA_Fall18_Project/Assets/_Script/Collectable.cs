using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    private Timer_Controller Timer_Script;

	void Start () {
        GameObject gameObject_ScriptHolder = GameObject.Find("ScriptHolder");
        Timer_Script = gameObject_ScriptHolder.GetComponent<Timer_Controller>();
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Timer_Script.CollectTime();
        }
    }
}
