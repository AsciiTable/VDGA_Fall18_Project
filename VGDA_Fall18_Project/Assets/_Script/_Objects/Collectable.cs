using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public bool Timer_Activated = true;

    private Timer_Controller Timer_Script;

	void Start () {
        if (Timer_Activated)
        {
            Timer_Script = (Timer_Controller)GameObject.FindObjectOfType(typeof(Timer_Controller));
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            if (Timer_Activated)
            {
                Timer_Script.CollectTime();
            }
        }
    }
}
