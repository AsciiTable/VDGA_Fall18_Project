using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Player Player_script;
    private GameObject platform;

    private void Awake()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Enemy Death");
            Player_script.ResetScene();
        }
    }

}
