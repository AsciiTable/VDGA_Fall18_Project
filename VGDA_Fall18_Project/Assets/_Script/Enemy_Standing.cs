using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Standing : MonoBehaviour
{

    private Player Player_script;

    private void Awake()
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Enemy Death");
            Player_script.ResetScene();
        }
    }

}
